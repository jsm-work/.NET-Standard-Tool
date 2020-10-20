using System;
using System.Collections.Generic;
using System.Linq;

namespace Database_Item
{
    public class JSResults : List<JSResult>
    {
        public string GetStringValue(int index, string fieldName)
        {
            if (index < this.Count())
            {
                if (true == this[index].ContainsKey(fieldName))
                {
                    return this[index][fieldName] as string;
                }
            }
            return null;
        }

        public byte[] GetBytesValue(int index, string fieldName)
        {
            if (index < this.Count())
            {
                if (true == this[index].ContainsKey(fieldName))
                {
                    return this[index][fieldName] as byte[];
                }
            }
            return null;
        }
    }
}
