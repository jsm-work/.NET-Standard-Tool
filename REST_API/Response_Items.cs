using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class Response_Item<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Log { get; set; }
        public T Response { get; set; }
    }
    public class Response_Items<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Log { get; set; }
        public List<T> Response { get; set; }
    }
}
