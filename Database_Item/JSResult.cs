using System;
using System.Collections.Generic;
using System.Text;

namespace Database_Item
{
    public class JSResult : Dictionary<string, object>
    {
        public int? GetIntValue(string fieldName)
        {
            if (true == this.ContainsKey(fieldName))
            {
                return int.Parse((string)this[fieldName]);
            }
            return null;
        }
        public string GetStringValue(string fieldName)
        {
            if (true == this.ContainsKey(fieldName))
            {
                return (string)this[fieldName];
            }
            return null;
        }

        public byte[] GetBytesValue(string fieldName)
        {
            if (true == this.ContainsKey(fieldName))
            {
                return this[fieldName] as byte[];
            }
            return null;
        }
    }
}
