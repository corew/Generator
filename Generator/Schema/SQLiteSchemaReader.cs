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
                        if (col.IsPK)
                            col.IsAutoIncrement = rdr["sql"].ToString().ToUpper().Contains("AUTOINCREMENT");
                        else
                            col.IsAutoIncrement = false;

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
            switch (sqlType.ToLower())
            {
                case "integer":
                case "int":
                case "tinyint":
                case "smallint":
                case "mediumint":
                case "int2":
                case "int8":
                    sysType = "int";
                    break;
                case "bigint":
                case "unsigned big int":
                    sysType = "long";
                    break;
                case "uniqueidentifier":
                    sysType = "Guid";
                    break;
                case "smalldatetime":
                case "datetime":
                case "date":
                    sysType = "DateTime";
                    break;
                case "float":
                case "double precision":
                case "double":
                    sysType = "double";
                    break;
                case "real":
                case "numeric":
                case "smallmoney":
                case "decimal":
                case "money":
                case "number":
                    sysType = "decimal";
                    break;
                case "bit":
                    sysType = "bool";
                    break;
                case "image":
                case "binary":
                case "varbinary":
                case "timestamp":
                    sysType = "byte[]";
                    break;
            }

            return sysType;
        }




        const string TABLE_SQL =
            @"SELECT name, type , sql FROM sqlite_master WHERE type IN ('table','view') and name not LIKE 'sqlite_sequence%'";

        const string COLUMN_SQL = @"PRAGMA table_info({0})";

    }
}
