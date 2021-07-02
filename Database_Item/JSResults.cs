using System;
using System.Collections.Generic;
using System.Linq;

namespace Database_Item
{
    public class JSResults : List<JSResult>
    {
        public int GetIntValue(int index, string fieldName)
        {
            if (index < this.Count())
            {
                if (true == this[index].ContainsKey(fieldName))
                {
                    return int.Parse(this[index][fieldName] as string);
                }
            }
            throw new IndexOutOfRangeException();
        }

        public string GetStringValue(int index, string fieldName)
        {
            if (index < this.Count())
            {
                if (true == this[index].ContainsKey(fieldName))
                {
                    return this[index][fieldName] as string;
                }
            }
            throw new IndexOutOfRangeException();
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
            throw new IndexOutOfRangeException();
        }
    }
}
