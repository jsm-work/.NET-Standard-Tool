# .NET-Standard-Tool
Mysql, Oracle, AES128, REST_API 호출, MD5, Convertor, FTP 등의 기능의 편의 기능 제공

API로 일부 기능 지원 http://tool.jsmun.com/swagger/index.html

ㆍMySQL, Oracle, REST_API의 결과값을 일부를 JSResult Class로 통일화하여 사용 편의를 높임.
ㆍConvertor에서 자주 사용되는 데이터 변환 지원
ㆍAES128 암호화, 복호화 지원
ㆍ





API-GET

            JSResults b = REST_API.Get_JSResults("http://localhost/api/s100toolkit/program/last/all?programType_idx=-1&rankMin=-1&rankMax=-1");
            MessageBox.Show(a.GetStringValue(15, "encrytedData"));


API-POST

            JSResult result_encryted  = REST_API.Post_JSResult(   "http://localhost:7553/api/aes/aes128-encrypt", 
                                        REST_API.DictionaryToJson(new Dictionary<string, object>
                                        {
                                            { "Data", "40384B45B54596201114FE99042201" },
                                            { "Key", "4D5A79677065774A7343705272664F72" }
                                        })
            );
            MessageBox.Show(a.GetStringValue("encrytedData"));
            
Database-MySQL

            myMySql mysql = new myMySql("localhost", "root", "18932", "program", 5332);
            foreach (JSResult item in mysql.Select("SELECT * FROM groups"))
            {
                int? idx = item.GetIntValue("idx");                
                string groupName = item.GetStringValue("groupName");
            }
						
Database-Oracle

            myOracle oracle = new myOracle("localhost", "root", "18932", 5333);            
            foreach (JSResult item in oracle.Select("SELECT * FROM groups"))
            {
                int? idx = item.GetIntValue("idx");
                string groupName = item.GetStringValue("groupName");
            }

AES128

            string input = "jsmun";
            string Encrypt_String = AES128.Encrypt_String(input, "00000000000000000000000000000000", "00000000000000000000000000000000");
            string Decrypt_String = AES128.Decrypt_String(Encrypt_String, "00000000000000000000000000000000", "00000000000000000000000000000000");
            string output = Decrypt_String;
            if (input == output)
                MessageBox.Show("성공");





