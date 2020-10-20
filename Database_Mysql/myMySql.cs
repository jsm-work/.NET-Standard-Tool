using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Database_Item;

namespace Database_Mysql
{
    public class myMySql
    {
        string connStr;
        public MySqlConnection conn;

        public myMySql(string server, string id, string password, string database, int port)
        {
            connStr = String.Format("server=" + server + ";user id=" + id + "; password=" + password + "; database=" + database + "; port=" + port + "; CharSet = utf8; ");
            conn = new MySqlConnection(connStr);
        }

        #region Mysql / Oracle 사용법 통일
        public SQLResults Select(string queryString)
        {
            SQLResults result = new SQLResults();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SQLResult item = new SQLResult();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        try
                        {
                            switch (reader.GetValue(i).ToString())
                            {
                                case "System.Byte[]":
                                    byte[] data = (byte[])reader[reader.GetName(i)];
                                    item.Add(reader.GetName(i), data);
                                    break;

                                default:
                                    item.Add(reader.GetName(i), reader.GetString(i));
                                    break;
                            }
                        }
                        catch (System.Data.SqlTypes.SqlNullValueException ex)
                        {
                            item.Add(reader.GetName(i), null);
                        }
                    }
                    result.Add(item);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        #endregion





        /// <summary>
        /// Insert 후 idx 반환 [등록 실패 시 -1 반환]
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public int Insert_ReturnID(string queryString)
        {
            MySqlCommand cmd;
            cmd = new MySqlCommand(queryString, conn);

            MySqlCommand lastId;
            MySqlDataReader lid = null;
            lastId = new MySqlCommand();
            lastId.Connection = conn;
            lastId.CommandText = ("SELECT LAST_INSERT_ID() AS idx;");

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                lid = lastId.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (lid.Read())
                {
                    int idx = lid.GetInt32("idx");
                    conn.Close();
                    return idx;
                }

            }
            catch (Exception)
            {
            }
            finally
            {
                conn.Close();
            }

            return -1;
        }

        public int Insert(string queryString)
        {
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Dictionary<string, object>> SelectBlob(string queryString, int size)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {

                        try
                        {
                            if (reader.GetValue(i).ToString() == "System.Byte[]")
                            {

                                byte[] data = new byte[size];
                                reader.GetBytes(i, 0, data, 0, size);
                                item.Add(reader.GetName(i), data);
                            }
                            else
                                item.Add(reader.GetName(i), reader.GetValue(i).ToString());
                        }
                        catch (Exception e)
                        {
                            item.Add(reader.GetName(i), "");
                        }
                    }
                    result.Add(item);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return result;
        }


        public string GetnewVersion(string queryString)
        {
            //List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            string result = "";
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                Dictionary<string, string> item = new Dictionary<string, string>();
                while (reader.Read())
                {

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        item.Add(reader.GetName(i), reader.GetString(i));
                    }

                }
                result = item["programVersion"];
            }

            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return result;
        }


    }
}
