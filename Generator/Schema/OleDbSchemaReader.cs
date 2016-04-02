using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    class OleDbSchemaReader : SchemaReader
    {
        // SchemaReader.ReadSchema
        public override Tables ReadSchema(DbConnection connection, DbProviderFactory factory)
        {
            var result = new Tables();

            _connection = connection;
            _factory = factory;


            var cnn = _connection as OleDbConnection;
            if (cnn != null)
            {
                var dt = cnn.GetOleDbSchemaTable(
                    OleDbSchemaGuid.Tables,
                    new object[] { null, null, null, "TABLE" });

                foreach (DataRow row in dt.Rows)
                {
                    Table tbl = new Table();
                    tbl.Name = row["TABLE_NAME"].ToString();
                    tbl.Schema = row["TABLE_SCHEMA"].ToString();
                    // todo have not deal with the 'view' object
                    tbl.IsView = string.Compare(row["TABLE_TYPE"].ToString().ToUpper(), "VIEW", true) == 0;
                    tbl.CleanName = CleanUp(tbl.Name);
                    tbl.ClassName = Inflector.MakeSingular(tbl.CleanName);

                    result.Add(tbl);
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
            var result = new List<Column>();
            var cnn = _connection as OleDbConnection;
            if (cnn != null)
            {
                var dt = cnn.GetOleDbSchemaTable(
                    OleDbSchemaGuid.Columns,
                    new object[] { null, null, tbl.Name, null });

                foreach (DataColumn col in dt.Columns)
                {
                    Console.WriteLine(col.ColumnName);
                }

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row["COLUMN_NAME"] + "::" + row["DATA_TYPE"]);

                    Column col = new Column();
                    col.Name = row["COLUMN_NAME"].ToString();
                    col.PropertyName = CleanUp(col.Name);
                    col.PropertyType = GetPropertyType(row["DATA_TYPE"].ToString());
                    col.IsNullable = row["IS_NULLABLE"].ToString() == "YES";
                    col.IsAutoIncrement = ((int)row["IsIdentity"]) == 1;
                    result.Add(col);
                }
            }

            return result;
        }

        string GetPK(string table)
        {

            string sql = @"SELECT c.name AS ColumnName
                FROM sys.indexes AS i 
                INNER JOIN sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id 
                INNER JOIN sys.objects AS o ON i.object_id = o.object_id 
                LEFT OUTER JOIN sys.columns AS c ON ic.object_id = c.object_id AND c.column_id = ic.column_id
                WHERE (i.is_primary_key = 1) AND (o.name = @tableName)";

            using (var cmd = _factory.CreateCommand())
            {
                cmd.Connection = _connection;
                cmd.CommandText = sql;

                var p = cmd.CreateParameter();
                p.ParameterName = "@tableName";
                p.Value = table;
                cmd.Parameters.Add(p);

                var result = cmd.ExecuteScalar();

                if (result != null)
                    return result.ToString();
            }

            return "";
        }

        string GetPropertyType(string sqlType)
        {
            string sysType = "string";
            switch (sqlType)
            {
                case "bigint":
                    sysType = "long";
                    break;
                case "smallint":
                    sysType = "short";
                    break;
                case "int":
                    sysType = "int";
                    break;
                case "uniqueidentifier":
                    sysType = "Guid";
                    break;
                case "smalldatetime":
                case "datetime":
                case "datetime2":
                case "date":
                case "time":
                    sysType = "DateTime";
                    break;
                case "datetimeoffset":
                    sysType = "DateTimeOffset";
                    break;
                case "float":
                    sysType = "double";
                    break;
                case "real":
                    sysType = "float";
                    break;
                case "numeric":
                case "smallmoney":
                case "decimal":
                case "money":
                    sysType = "decimal";
                    break;
                case "tinyint":
                    sysType = "byte";
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
                case "geography":
                    sysType = "Microsoft.SqlServer.Types.SqlGeography";
                    break;
                case "geometry":
                    sysType = "Microsoft.SqlServer.Types.SqlGeometry";
                    break;
            }
            return sysType;
        }
    }
}
