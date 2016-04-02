using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Column
    {
        public string Name;
        public string PropertyName;
        public string PropertyType;
        public bool IsPK;
        public bool IsNullable;
        public bool IsAutoIncrement;
        public bool Ignore;
    }
}
