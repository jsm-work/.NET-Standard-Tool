using Database_Item;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace API
{
    public class REST_API
    {
        private const int delaySec = 50;
        private static Dictionary<string, string> dicResult = new Dictionary<string, string>();


        private static void Request_GET(string url, string timeKey)
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(async () =>
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    using (System.Net.Http.HttpResponseMessage response = await client.GetAsync(url))
                    {
                        using (System.Net.Http.HttpContent content = response.Content)
                        {
                            string myContent = await content.ReadAsStringAsync();
                            dicResult.Add(timeKey, myContent);
                        }
                    }
                }
            }))
            { IsBackground = true };
            th.Start();
        }
        private static void Request_POST(string uri, string raw, string timeKey)
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(async () =>
            {
                using (var client = new System.Net.Http.HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
                {
                    request.Content = new System.Net.Http.StringContent(raw,
                                            Encoding.UTF8,
                                            "application/json");//CONTENT-TYPE header
                    await client.SendAsync(request).ContinueWith(responseTask =>
                    {
                        dicResult.Add(timeKey, responseTask.Result.Content.ReadAsStringAsync().Result);
                    });
                }
            }))
            { IsBackground = true };
            th.Start();
        }

        private static async void Request_POST_MultipartContent(string uri, string raw, string timeKey)
        {
            using (var client = new System.Net.Http.HttpClient())
            using (var batchRequest = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, uri))
            {
                System.Net.Http.MultipartContent batchContent = new System.Net.Http.MultipartContent("batch");
                batchRequest.Content = batchContent;

                batchContent.Add(new System.Net.Http.StringContent(raw, System.Text.Encoding.UTF8, "application/json"));


                await client.SendAsync(batchRequest).ContinueWith(responseTask =>
                {
                    dicResult.Add(timeKey, responseTask.Result.Content.ReadAsStringAsync().Result);
                });
            }
        }

        private static void Request_PUT(string uri, string raw, string timeKey)
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(async () =>
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Put, uri);
                    request.Content = new System.Net.Http.StringContent(raw,
                                            Encoding.UTF8,
                                            "application/json");//CONTENT-TYPE header
                    await client.SendAsync(request).ContinueWith(responseTask =>
                    {
                        dicResult.Add(timeKey, responseTask.Result.Content.ReadAsStringAsync().Result);
                    });
                }
            }))
            { IsBackground = true };
            th.Start();
        }
        private static void Request_DELETE(string uri, string raw, string timeKey)
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(async () =>
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Delete, uri);
                    request.Content = new System.Net.Http.StringContent(raw,
                                            Encoding.UTF8,
                                            "application/json");//CONTENT-TYPE header
                    await client.SendAsync(request).ContinueWith(responseTask =>
                    {
                        dicResult.Add(timeKey, responseTask.Result.Content.ReadAsStringAsync().Result);
                    });
                }
            }))
            { IsBackground = true };
            th.Start();
        }
        private static void Request_DELETE(string url, string timeKey)
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(async () =>
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    using (System.Net.Http.HttpResponseMessage response = await client.DeleteAsync(url))
                    {
                        using (System.Net.Http.HttpContent content = response.Content)
                        {
                            string myContent = await content.ReadAsStringAsync();
                            dicResult.Add(timeKey, myContent);
                        }
                    }
                }
            }))
            { IsBackground = true };
            th.Start();
        }
        private static void Request_POST<T>(string url, string timeKey, T data)
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(async () =>
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    //특정 'Accept' 헤더를 BSON으로 지정: 서버에서 BSON 형식으로 데이터를 반환하도록 요청
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/bson"));

                    //'Content-Type' 헤더 지정: 서버에서 게시할 데이터 형식 지정
                    //Post 데이터는 Bson 형식으로 표시됨          
                    var bSonData = SerializeBson<T>(data);
                    var byteArrayContent = new System.Net.Http.ByteArrayContent(bSonData);
                    byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/bson");

                    await client.PostAsync(url, byteArrayContent).ContinueWith(responseTask =>
                    {
                        dicResult.Add(timeKey, responseTask.Result.ToString());
                    });
                }
            }))
            { IsBackground = true };
            th.Start();
        }

        public static byte[] SerializeBson<T>(T obj)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (Newtonsoft.Json.Bson.BsonWriter writer = new Newtonsoft.Json.Bson.BsonWriter(ms))
                {
                    Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                    serializer.Serialize(writer, obj);
                }

                return ms.ToArray();
            }
        }

        /// <summary>
        /// GET 메서드 호출
        /// </summary>
        /// <param name="uri">호출 경로</param>
        /// <param name="timeKey">입력 X</param>
        /// <param name="sec">최대 지연 시간</param>
        /// <returns></returns>
        public static string Get(string uri, int sec = delaySec, string timeKey = "")
        {
            timeKey = timeKey.Length == 0 ? System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff") : timeKey;
            Request_GET(uri, timeKey);
            System.Console.WriteLine("GET\t\t" + timeKey.Split(' ')[1] + "\t" + uri);

            for (int i = 0; i < sec * 1000 && dicResult.ContainsKey(timeKey) == false; i += 100)
            {
                System.Threading.Thread.Sleep(100);
                System.Console.WriteLine((i / 1000.0f) + "초 대기중");
            }
            System.Console.WriteLine("");
            if (dicResult.ContainsKey(timeKey) == false)
            {
                System.Console.WriteLine("Json 공백 반환.");
                dicResult.Remove(timeKey);
                return null;
            }
            else
            {
                string result = dicResult[timeKey];
                dicResult.Remove(timeKey);
                return result;
            }
        }
        public static JSResult Get_JSResult(string uri, string timeKey = "", int sec = delaySec)
        {
            timeKey = timeKey.Length == 0 ? System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff") : timeKey;
            string json = Get(uri, sec, timeKey);
            if (json.Length == 0)
                return new JSResult();
            else
                return JsonToJSResult(json);
        }
        public static JSResults Get_JSResults(string uri, string timeKey = "", int sec = delaySec)
        {
            timeKey = timeKey.Length == 0 ? System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff") : timeKey;
            string json = Get(uri, sec, timeKey);
            if (json.Length == 0)
                return new JSResults();
            else
                return JsonToJSResults(json);
        }

        public static string Post(string uri, string raw, string timeKey = "", int sec = delaySec)
        {
            timeKey = timeKey.Length == 0 ? System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff") : timeKey;
            Request_POST(uri, raw, timeKey);
            System.Console.WriteLine("POST\t" + timeKey.Split(' ')[1] + "\t" + uri);

            for (int i = 0; i < sec * 1000 && dicResult.ContainsKey(timeKey) == false; i += 100)
            {
                System.Threading.Thread.Sleep(100);
                System.Console.WriteLine((i / 1000.0f) + "초 대기중");
            }
            System.Console.WriteLine("");
            if (dicResult.ContainsKey(timeKey) == false)
            {
                System.Console.WriteLine("Json 공백 반환.");
                return null;
            }
            string json = dicResult[timeKey];
            dicResult.Remove(timeKey);
            return json;
        }
        public static JSResult Post_JSResult(string uri, string raw, string timeKey = "", int sec = delaySec)
        {
            timeKey = timeKey.Length == 0 ? System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff") : timeKey;
            string json = Post(uri, raw, timeKey, sec);
            if (json.Length == 0)
                return new JSResult();
            else
                return JsonToJSResult(json);
        }
        public static JSResults Post_JSResults(string uri, string raw, string timeKey = "", int sec = delaySec)
        {
            timeKey = timeKey.Length == 0 ? System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff") : timeKey;
            string json = Post(uri, raw, timeKey, sec);
            if (json.Length == 0)
                return new JSResults();
            else
                return JsonToJSResults(json);
        }


        public static string Put(string uri, string raw, int sec = delaySec)
        {
            string timeKey = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
            Request_PUT(uri, raw, timeKey);
            System.Console.WriteLine("PUT\t" + timeKey.Split(' ')[1] + "\t" + uri);

            for (int i = 0; i < sec * 1000 && dicResult.ContainsKey(timeKey) == false; i += 100)
            {
                System.Threading.Thread.Sleep(100);
                System.Console.WriteLine((i / 1000.0f) + "초 대기중");
            }
            System.Console.WriteLine("");
            if (dicResult.ContainsKey(timeKey) == false)
            {
                System.Console.WriteLine("Json 공백 반환.");
                return null;
            }
            return dicResult[timeKey];
        }

        public static string Delete(string uri, string raw, int sec = delaySec)
        {
            string timeKey = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
            Request_DELETE(uri, raw, timeKey);
            System.Console.WriteLine("DELETE\t" + timeKey.Split(' ')[1] + "\t" + uri);

            for (int i = 0; i < sec * 1000 && dicResult.ContainsKey(timeKey) == false; i += 100)
            {
                System.Threading.Thread.Sleep(100);
                System.Console.WriteLine((i / 1000.0f) + "초 대기중");
            }
            System.Console.WriteLine("");
            if (dicResult.ContainsKey(timeKey) == false)
            {
                System.Console.WriteLine("Json 공백 반환.");
                return null;
            }
            return dicResult[timeKey];
        }

        public static string Delete(string uri, int sec = delaySec)
        {
            string timeKey = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
            Request_DELETE(uri, timeKey);
            System.Console.WriteLine("DELETE\t" + timeKey.Split(' ')[1] + "\t" + uri);

            for (int i = 0; i < sec * 1000 && dicResult.ContainsKey(timeKey) == false; i += 100)
            {
                System.Threading.Thread.Sleep(100);
                System.Console.WriteLine((i / 1000.0f) + "초 대기중");
            }
            System.Console.WriteLine("");
            if (dicResult.ContainsKey(timeKey) == false)
            {
                System.Console.WriteLine("Json 공백 반환.");
                return null;
            }
            return dicResult[timeKey];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="s3Bucket"></param>
        /// <param name="formData"></param>
        /// <param name="mediaType">text/plain, application/json</param>
        /// <param name="compressedFile"></param>
        /// <returns></returns>
        public static string PostFile(string host, string uri, PostItems items, int Timeout = 10000)
        {














            var client = new RestClient(host);
            RestRequest request = new RestRequest(uri, Method.POST);
            //request.AddHeader("FileName", "mytest.txt");
            request.AddHeader("Content-Type", "multipart/form-data");
            foreach (PostItem item in items)
            {
                if (item is PostItem_String)
                    request.AddParameter(item.Name, (item as PostItem_String).Value);
                else if (item is PostItem_File)
                    request.AddFile(item.Name, (item as PostItem_File).Value);
            }

            request.ReadWriteTimeout = Timeout;
            request.Timeout = Timeout;
            return client.Execute(request).ToString();












            //using (var client = new HttpClient())
            //{
            //    MultipartFormDataContent content = new MultipartFormDataContent();

            //    foreach (KeyValuePair<string, object> item in Contents)
            //    {
            //        if (item.Value is string)
            //        {
            //            content.Add(new StringContent(item.Value.ToString()), item.Key);
            //        }
            //        else if (item.Value is byte[])
            //        {
            //            content.Add(new ByteArrayContent((byte[])item.Value), item.Key);
            //        }
            //        else if (item.Value is System.IO.FileStream)
            //        {
            //            using (System.IO.FileStream stream = (System.IO.FileStream)item.Value)
            //            {

            //                ByteArrayContent file_content = new ByteArrayContent(new StreamContent(stream).ReadAsByteArrayAsync().Result);
            //                file_content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
            //                content.Add(file_content, "file");

            //                //file_content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            //                //file_content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            //                //file_content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")

            //                //{
            //                //    FileName = "screenshot.png",
            //                //    Name = "foo",
            //                //};

            //            }
            //        }
            //    }
            //    HttpResponseMessage response = client.PostAsync(uri, content).Result;
            //        return response.Content.ReadAsStringAsync().Result;
            //    //HttpResponseMessage response = await client.PostAsync(uri, content);
            //    //response.EnsureSuccessStatusCode();
            //}



            //HttpResponseMessage response;

            //using (var client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
            //    var requestContent = new MultipartFormDataContent();
            //    foreach (KeyValuePair<string, object> item in Contents)
            //    {
            //        if (item.Value is string)
            //        {
            //            requestContent.Add(new StringContent(item.Value.ToString()), item.Key);
            //        }
            //        else if (item.Value is byte[])
            //        {
            //            requestContent.Add(new ByteArrayContent((byte[])item.Value), item.Key);
            //        }
            //        else if (item.Value is System.IO.Stream)
            //        {
            //            requestContent.Add(new StreamContent((System.IO.Stream)item.Value), item.Key);
            //        }
            //    }

            //    response = client.PostAsync(uri, requestContent).Result;
            //    return response.Content.ReadAsStringAsync().Result;
            //}
        }

        /// <summary>
        /// 미검증
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="requestClass"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        //public static string Post<T>(string uri, T requestClass, int sec = 3)
        //{
        //    string timeKey = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");
        //    System.Console.WriteLine("POST  " + timeKey.Split(' ')[1] + "/  " + uri);
        //    for (int i = 0; i < sec * 1000 && dicResult.ContainsKey(timeKey) == false; i += 100)
        //    {
        //        System.Threading.Thread.Sleep(100);
        //        System.Console.WriteLine((i / 1000.0f) + "초 대기중");
        //    }
        //    System.Console.WriteLine("");
        //    if (dicResult.ContainsKey(timeKey) == false)
        //    {
        //        System.Console.WriteLine("Json 공백 반환.");
        //        return null;
        //    }
        //    return dicResult[timeKey];
        //}


        public static JSResult JsonToJSResult(string json)
        {
            return JsonConvert.DeserializeObject<JSResult>(json);
        }
        public static JSResults JsonToJSResults(string json)
        {
            return JsonConvert.DeserializeObject<JSResults>(json);
        }

        #region Dictionary<string, string> ↔ JSON
        public static string DictionaryToJson(Dictionary<string, object> values)
        {
            string result = "{";
            foreach (var item in values)
            {
                result += "\"" + item.Key + "\":\"" + item.Value + "\",";
            }
            result = result.Remove(result.Length - 1, 1);
            return result + "}";
        }
        public static Dictionary<string, string> JsonToDictionary_string_string(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
        #endregion
    }
}
