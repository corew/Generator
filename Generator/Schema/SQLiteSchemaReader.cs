using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    class SQLiteSchemaReader : SchemaReader
    {
        // SchemaReader.ReadSchema
        public override Tables ReadSchema(DbConnection connection, DbProviderFactory factory)
        {
            var result = new Tables();

            _connection = connection;
            _factory = factory;

            var cmd = _factory.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = TABLE_SQL;

            //pull the tables in a reader
            using (cmd)
            {

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Table tbl = new Table();
                        tbl.Name = rdr["name"].ToString();
                        tbl.Schema = null;
                        tbl.IsView = String.Compare(rdr["type"].ToString(), "view", StringComparison.OrdinalIgnoreCase) == 0;
                        tbl.CleanName = CleanUp(tbl.Name);
                        tbl.ClassName = Inflector.MakeSingular(tbl.CleanName);

                        result.Add(tbl);
                    }
                }
            }

            foreach (var tbl in result)
            {
                tbl.Columns = LoadColumns(tbl);

                // Mark the primary key
                string PrimaryKey = GetPK(tbl.Name);
                var pkColumn = tbl.Columns.SingleOrDefault(x => x.Name.ToLower().Trim() == PrimaryKey.ToLower().Trim());
                if (pkColumn != null)
                {
                    pkColumn.IsPK = true;
                }
            }


            return result;
        }

        DbConnection _connection;
        DbProviderFactory _factory;


        List<Column> LoadColumns(Table tbl)
        {

            using (var cmd = _factory.CreateCommand())
            {
                cmd.Connection = _connection;
                cmd.CommandText = string.Format(COLUMN_SQL, tbl.Name);

                var result = new List<Column>();
                using (IDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Column col = new Column();
                        col.Name = rdr["name"].ToString();
                        col.PropertyName = CleanUp(col.Name);
                        col.PropertyType = GetPropertyType(rdr["type"].ToString());
                        col.IsNullable = rdr["notnull"].ToString() == "0";
                        col.IsAutoIncrement = false;

                        if (rdr["pk"].ToString() == "1")
                        {
                            var sql = "select * from sqlite_sequence where name = @tableName";
                            try
                            {
                                using (var _cmd = _factory.CreateCommand())
                                {
                                    _cmd.Connection = _connection;
                                    _cmd.CommandText = sql;

                                    var _p = _cmd.CreateParameter();
                                    _p.ParameterName = "@tableName";
                                    _p.Value = tbl.Name;
                                    _cmd.Parameters.Add(_p);

                                    using (IDataReader _rdr = _cmd.ExecuteReader())
                                    {
                                        if (_rdr.Read())
                                        {
                                            col.IsAutoIncrement = true;
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                // ignore
                            }
                        }

                        result.Add(col);
                    }
                }

                return result;
            }
        }

        string GetPK(string table)
        {
            using (var cmd = _factory.CreateCommand())
            {
                cmd.Connection = _connection;
                cmd.CommandText = string.Format(COLUMN_SQL, table);

                using (IDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        if (rdr["pk"].ToString() == "1")
                            return rdr["name"].ToString();
                    }
                }
            }

            return "";
        }

        string GetPropertyType(string sqlType)
        {
            string sysType = "string";
            switch (sqlType.ToUpper())
            {
                case "INTEGER":
                    sysType = "int";
                    break;
                case "REAL":
                    sysType = "double";
                    break;
                case "TEXT":
                    sysType = "string";
                    break;
                case "BLOB":
                    sysType = "byte[]";
                    break;
            }
            return sysType;
        }



        const string TABLE_SQL = @"select * from sqlite_master 
            where (type = 'table' or type = 'view')
            and name != 'sqlite_sequence' 
            and name not like 'sqlite_autoindex_TABLE_%' 
            and name not like 'sqlite_stat%'
            and name not like '_%_old_%'";

        const string COLUMN_SQL = @"PRAGMA table_info({0})";

    }
}
