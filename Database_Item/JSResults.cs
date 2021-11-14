using System;
using System.Collections.Generic;
using System.Linq;

namespace Database_Item
{

    /// <summary>
    /// JSResults 데이터를 구조에 맞는 클래스에 변수 이름을 비교하여 자동으로 넣어준다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JSResults_Carrier <T>
    {
        /// <summary>
        /// JSResults 데이터를 구조에 맞는 클래스에 변수 이름을 비교하여 자동으로 넣어준다.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="jsr"></param>
        /// <returns></returns>
        public static List<T> Carrier(JSResults jsr)
        {
            Type type = typeof(T);
            System.Reflection.FieldInfo[] fields = type.GetFields(System.Reflection.BindingFlags.Public |
                                                             System.Reflection.BindingFlags.NonPublic |
                                                             System.Reflection.BindingFlags.Instance);
            List<T> result = new List<T>();

            foreach (var item in jsr)
            {
                //Type을 사용하여 동적으로 변수 생성
                T result_item = Activator.CreateInstance<T>();

                foreach (var item_field in fields)
                {
                    if (item.Keys.Contains(item_field.Name))
                    {
                        switch (item_field.FieldType.Name.ToString())
                        {
                            case "String":
                                item_field.SetValue(result_item, item[item_field.Name]);
                                break;
                            case "Int32":
                                item_field.SetValue(result_item, int.Parse(item[item_field.Name].ToString()));
                                break;
                            case "Boolean":
                                bool data = false;
                                if (item[item_field.Name].ToString() == "1" || item[item_field.Name].ToString().ToUpper() == "TRUE")
                                    data = true;

                                item_field.SetValue(result_item, data);
                                break;
                            case "Double":
                                item_field.SetValue(result_item, double.Parse(item[item_field.Name].ToString()));
                                break;
                            case "Float":
                                item_field.SetValue(result_item, float.Parse(item[item_field.Name].ToString()));
                                break;
                        }
                    }

                    //foreach (var data in item)
                    //{
                    //    if (item_field.Name == data.Key)
                    //        item_field.SetValue(result_item, data.Value);
                    //}
                }
                result.Add(result_item);
            }

            return result;
        }
    }

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
