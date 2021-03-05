using System;
using System.Collections.Generic;

namespace Email
{
    public class Email
    {
        /// <summary>
        /// 이메일 전송
        /// </summary>
        /// <param name="SMTP_host" > SMTP주소 구글(smtp.gmail.com)_네이버(smtp.naver.com)_메일플러그(smtp.mailplug.co.kr)</param>
        /// <param name="SMTP_mail" > 메일 접속 아이디 (예-TEST@naver.com)</param>
        /// <param name="SMTP_pw"   > 메일 접속 암호                </param>
        /// <param name="SMTP_port" > SMTP주소 구글(587)_네이버(587)_메일플러그(465)</param>
        /// <param name="from_name" > 송신자 (예-신짱구)            </param>
        /// <param name="to_mail"   > 수신 메일 (예-get@naver.com)  </param>
        /// <param name="title"     > 메일 제목                     </param>
        /// <param name="content"   > 메일 내용                     </param>
        /// <param name="files"     > 첨부 파일 경로                </param>
        /// <param name="isHtmlBody"> HTML로 작성된 메일 내용       </param>
        public static async System.Threading.Tasks.Task SendMail(string SMTP_host,
                                                                 string SMTP_mail,
                                                                 string SMTP_pw,
                                                                 int SMTP_port,
                                                                 string from_name,
                                                                 List<string> to_mail,
                                                                 string subject,
                                                                 string content,
                                                                 string[] files,
                                                                 bool isHtmlBody = false)
        {
            try
            {
                int MAX_MAIL_COUNT = 100;
                int loop = to_mail.Count / MAX_MAIL_COUNT;
                for (int i = 0; loop >= i; i++)
                {
                    MimeKit.MimeMessage message = new MimeKit.MimeMessage();
                    MimeKit.BodyBuilder body = new MimeKit.BodyBuilder();


                    message.From.Add(new MimeKit.MailboxAddress(from_name, SMTP_mail));             //----- 메일 송신자
                    for (int mail_i = i * MAX_MAIL_COUNT; mail_i < (i * MAX_MAIL_COUNT) + MAX_MAIL_COUNT && mail_i < to_mail.Count; mail_i++)
                        message.To.Add(new MimeKit.MailboxAddress("", to_mail[mail_i]));            //----- 메일 수신자
                    message.Subject = subject;                                                      //----- 메일 제목
                    if (isHtmlBody == true)
                        body.HtmlBody = content;
                    else
                        body.TextBody = content;

                    #region 첨부파일
                    foreach (string filePath in files)
                    {
                        if (filePath.Length < 1) continue;

                        try
                        {
                            System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
                            if (fi.Exists == true)
                            {
                                //파일 불러오기
                                byte[] fileBytes = StreamToBytes(System.IO.File.OpenRead(filePath));

                                //파일 첨부
                                body.Attachments.Add(fi.Name, fileBytes);
                            }
                        }
                        catch (System.ArgumentException e)
                        {
                            System.Console.WriteLine("");
                            System.Console.WriteLine("===== 잘못된 파일 경로 : " + filePath + " =====");
                            System.Console.WriteLine("");
                        }
                    }
                    #endregion
                    message.Body = body.ToMessageBody();                                            //----- 메일 내용


                    using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        await client.ConnectAsync(SMTP_host, SMTP_port, true);                      //----- SMTP 호스트 주소 / 포트 / SSL 사용
                        await client.AuthenticateAsync(SMTP_mail, SMTP_pw);                         //----- SMTP 계정
                        client.Capabilities.HasFlag(MailKit.Net.Smtp.SmtpCapabilities.StartTLS);    //----- TLS
                        await client.SendAsync(message);                                            //----- 메일 전송
                        await client.DisconnectAsync(true);                                         //----- 연결 해제
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }


        private static byte[] StreamToBytes(System.IO.Stream stream)
        {
            System.IO.MemoryStream TempMemoryStream;
            Int32 reads = 0; //임시 메모리스트림에 작성 
            using (System.IO.Stream st = stream)
            {
                using (System.IO.MemoryStream output = new System.IO.MemoryStream())
                {
                    st.Position = 0; Byte[] buffer = new Byte[256];
                    while (0 < (reads = st.Read(buffer, 0, buffer.Length)))
                    {
                        output.Write(buffer, 0, reads);
                    }
                    TempMemoryStream = output;
                    output.Flush();
                } // in using 
            } // out using
            byte[] bytes = TempMemoryStream.ToArray();

            return bytes;
        }
    }
}
