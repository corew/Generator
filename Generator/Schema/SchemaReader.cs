using System;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace Generator
{
    abstract class SchemaReader
    {
        public abstract Tables ReadSchema(DbConnection connection, DbProviderFactory factory);

        public void WriteLine(string o)
        {
            //outer.WriteLine(o);
        }

        static Regex rxCleanUp = new Regex(@"[^\w\d_]", RegexOptions.Compiled);

        static string[] cs_keywords =
        {
            "abstract", "event", "new", "struct", "as", "explicit", "null",
            "switch", "base", "extern", "object", "this", "bool", "false", "operator", "throw",
            "break", "finally", "out", "true", "byte", "fixed", "override", "try", "case", "float",
            "params", "typeof", "catch", "for", "private", "uint", "char", "foreach", "protected",
            "ulong", "checked", "goto", "public", "unchecked", "class", "if", "readonly", "unsafe",
            "const", "implicit", "ref", "ushort", "continue", "in", "return", "using", "decimal",
            "int", "sbyte", "virtual", "default", "interface", "sealed", "volatile", "delegate",
            "internal", "short", "void", "do", "is", "sizeof", "while", "double", "lock",
            "stackalloc", "else", "long", "static", "enum", "namespace", "string"
        };

        public static Func<string, string> CleanUp = (str) =>
        {
            str = rxCleanUp.Replace(str, "_");

            if (char.IsDigit(str[0]) || cs_keywords.Contains(str))
                str = "@" + str;

            return str;
        };

    }
}
