using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Table
    {
        public List<Column> Columns;
        public string Name;
        public string Schema;
        public bool IsView;
        public string CleanName;
        public string ClassName;
        public string SequenceName;
        public bool Ignore;

        public Column PK
        {
            get
            {
                return this.Columns.SingleOrDefault(x => x.IsPK);
            }
        }

        public Column GetColumn(string columnName)
        {
            return Columns.Single(x => string.Compare(x.Name, columnName, true) == 0);
        }

        public Column this[string columnName]
        {
            get
            {
                return GetColumn(columnName);
            }
        }

    }
}
