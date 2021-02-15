using System;
using System.Collections.Generic;
using System.Text;

namespace API
{
    public class PostItem
    {
        public string Name { get; set; }
        public interface Value { }
    }

    public class PostItem_File : PostItem
    {
        // 파일 경로 : FilePath
        public string Value { get; set; }

        public PostItem_File() { }
        public PostItem_File(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    public class PostItem_String : PostItem
    {
        /// <summary>
        /// 값
        /// </summary>
        public string Value { get; set; }

        public PostItem_String() { }
        public PostItem_String(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    public class PostItems : List<PostItem>
    {
    }
}
