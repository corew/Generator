using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Tables : List<Table>
    {
        public Tables()
        {
        }

        public Table GetTable(string tableName)
        {
            return this.Single(x => string.Compare(x.Name, tableName, true) == 0);
        }

        public Table this[string tableName]
        {
            get
            {
                return GetTable(tableName);
            }
        }

    }
}
