using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Generator
{
    class Program
    {
        internal static string ConnectionString
            => ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        internal static string ProviderName
            => ConfigurationManager.ConnectionStrings["connectionString"].ProviderName;

        static string ClassPrefix = "";
        static string ClassSuffix = "";
        static string SchemaName = null;
        static bool IncludeViews = false;

        static void Main(string[] args)
        {
            DbProviderFactory _factory;
            try
            {
                _factory = DbProviderFactories.GetFactory(ProviderName);
            }
            catch (Exception x)
            {
                var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
                return;
            }

            try
            {
                Tables result;
                using (var conn = _factory.CreateConnection())
                {
                    conn.ConnectionString = ConnectionString;
                    conn.Open();

                    SchemaReader reader = null;

                    if (_factory.GetType().Name == "MySqlClientFactory")
                    {
                        // MySql
                        reader = new MySqlSchemaReader();
                    }
                    else if (_factory.GetType().Name == "SqlCeProviderFactory")
                    {
                        // SQL CE
                        reader = new SqlServerCeSchemaReader();
                    }
                    else if (_factory.GetType().Name == "NpgsqlFactory")
                    {
                        // PostgreSQL
                        reader = new PostGreSqlSchemaReader();
                    }
                    else if (_factory.GetType().Name == "OracleClientFactory")
                    {
                        // Oracle
                        reader = new OracleSchemaReader();
                    }
                    else if (_factory.GetType().Name == "OleDbFactory")
                    {
                        // OleDb for Access
                        reader = new OleDbSchemaReader();
                    }
                    else if (_factory.GetType().Name == "SQLiteFactory")
                    {
                        // SQLite
                        reader = new SQLiteSchemaReader();
                    }
                    else
                    {
                        // Assume SQL Server
                        reader = new SqlServerSchemaReader();
                    }

                    //reader.outer = this;
                    result = reader.ReadSchema(conn, _factory);

                    // Remove unrequired tables/views
                    for (int i = result.Count - 1; i >= 0; i--)
                    {
                        if (SchemaName != null && string.Compare(result[i].Schema, SchemaName, true) != 0)
                        {
                            result.RemoveAt(i);
                            continue;
                        }
                        if (!IncludeViews && result[i].IsView)
                        {
                            result.RemoveAt(i);
                            continue;
                        }
                    }

                    conn.Close();


                    var rxClean =
                        new Regex(
                            "^(Equals|GetHashCode|GetType|ToString|repo|Save|IsNew|Insert|Update|Delete|Exists|SingleOrDefault|Single|First|FirstOrDefault|Fetch|Page|Query)$");
                    foreach (var t in result)
                    {
                        t.ClassName = ClassPrefix + t.ClassName + ClassSuffix;
                        foreach (var c in t.Columns)
                        {
                            c.PropertyName = rxClean.Replace(c.PropertyName, "_$1");

                            // Make sure property name doesn't clash with class name
                            if (c.PropertyName == t.ClassName)
                                c.PropertyName = "_" + c.PropertyName;
                        }
                    }

                    // result here

                    return;
                }
            }
            catch (Exception x)
            {
                var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
                //Warning(string.Format("Failed to read database schema - {0}", error));
                //WriteLine("");
                //WriteLine("// -----------------------------------------------------------------------------------------");
                //WriteLine("// Failed to read database schema - {0}", error);
                //WriteLine("// -----------------------------------------------------------------------------------------");
                //WriteLine("");
                return;
            }
        }
    }
}
