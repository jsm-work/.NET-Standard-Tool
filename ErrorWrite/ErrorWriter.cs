using System;

namespace ErrorWrite
{
    public class ErrorWriter
    {       
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="functionName">System.Reflection.MethodBase.GetCurrentMethod().Name</param>
        public static void Write(Exception e, string functionName, string filePath = "")
        {
            try
            {
                string splitChar = "=";
                int splitChatCount = 60;

                //폴더 경로 지정
                string folderPath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory) + @"\ErrorLog\" + DateTime.Now.ToString("yyyy-MM");
                //폴더 존재 유무 확인
                System.IO.DirectoryInfo DI = new System.IO.DirectoryInfo(folderPath);
                //폴더 생성
                if (DI.Exists == false)
                    DI.Create();

                //파일 경로 지정
                if(filePath.Length == 0)
                    filePath = folderPath + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                
                using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Append))
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);


                    sw.WriteLine(DateTime.Now.ToString(" yyyy-MM-dd hh:mm:ss.ffff " + "[ " + functionName + " ]"));

                    sw.WriteLine(e.ToString());

                    for (int i = 0; i < splitChatCount; i++)
                        sw.Write(splitChar);
                    sw.WriteLine("");

                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
