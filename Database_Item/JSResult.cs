using System;
using System.Collections.Generic;
using System.Text;

namespace Database_Item
{
    public class JSResult : Dictionary<string, object>
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
