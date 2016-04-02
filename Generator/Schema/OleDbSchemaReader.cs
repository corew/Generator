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

                foreach (DataRow row in dt.Select("", "ORDINAL_POSITION ASC"))
                {
                    Console.WriteLine(row["COLUMN_NAME"] + "::" + row["DATA_TYPE"]);

                    Column col = new Column();
                    col.Name = row["COLUMN_NAME"].ToString();
                    col.PropertyName = CleanUp(col.Name);
                    col.PropertyType = GetPropertyType(row["DATA_TYPE"].ToString());
                    col.IsNullable = Convert.ToBoolean(row["IS_NULLABLE"]);
                    // oledb does not support an identity column
                    // so just see if there is an 'ID' column instead
                    col.IsAutoIncrement = col.Name == "ID" && !col.IsNullable;
                    result.Add(col);
                }
            }

            return result;
        }

        string GetPK(string table)
        {
            var cnn = _connection as OleDbConnection;
            if (cnn != null)
            {
                var dt = cnn.GetOleDbSchemaTable(
                    OleDbSchemaGuid.Primary_Keys,
                    new object[] { null, null, table });

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["COLUMN_NAME"].ToString();
                }
            }

            return "";
        }

        // see https://msdn.microsoft.com/zh-cn/dynamics/microsoft.sqlserver.dts.runtime.wrapper.datatype
        string GetPropertyType(string sqlType)
        {
            string sysType = "string";
            switch (sqlType)
            {
                case "20":
                case "21":
                    sysType = "long";
                    break;
                case "2":
                case "18":
                    sysType = "short";
                    break;
                case "3":
                case "19":
                    sysType = "int";
                    break;
                case "72":
                    sysType = "Guid";
                    break;
                case "7":
                case "133":
                case "134":
                case "135":
                case "145":
                case "304":
                    sysType = "DateTime";
                    break;
                case "5":
                    sysType = "double";
                    break;
                case "4":
                    sysType = "float";
                    break;
                case "6":
                case "14":
                case "131":
                    sysType = "decimal";
                    break;
                case "16":
                case "17":
                case "128":
                    sysType = "byte";
                    break;
                case "11":
                    sysType = "bool";
                    break;
                case "301":
                    sysType = "byte[]";
                    break;
            }
            return sysType;
        }
    }
}
