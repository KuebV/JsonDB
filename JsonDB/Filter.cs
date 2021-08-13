using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDB
{
    public class Filter
    {
        public string Field;
        public string FieldValue;
        public Filter(string ItemValue, string ItemField = null)
        {
            Field = ItemField;
            FieldValue = ItemValue;
        }
    }
}
