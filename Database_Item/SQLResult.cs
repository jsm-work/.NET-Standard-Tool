using System;
using System.Collections.Generic;
using System.Text;

namespace Database_Item
{
    public class SQLResult : Dictionary<string, object>
    {
        public string GetValue(string fieldName)
        {
            if (true == this.ContainsKey(fieldName))
            {
                return (string)this[fieldName];
            }
            return null;
        }
    }
}
