using Database_Item;
using Npgsql;
using System;

namespace RDBMS_Postgre
{
    /// <summary>
    /// * Npgsql (NuGet)
    /// </summary>
    public class Postgre
    {
        string connString = string.Empty;
        NpgsqlConnection conn = null;

        /// <summary>
        /// Npgsql (* NuGet 설치)
        /// </summary>
        /// <param name="host"></param>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        /// <param name="port"></param>
        public Postgre(string host, string id, string password, string database, int port)
        {
            //POSTGRESQL DB 연결 정보
            connString = "HOST=" + host + ";PORT=" + port + ";USERNAME=" + id + ";PASSWORD=" + password + ";DATABASE=" + database + "";
            this.conn = new NpgsqlConnection(connString);
        }

        #region Postgre / Mysql / Oracle 사용법 통일
        public bool Insert(string queryString)
        {
            try
            {
                conn.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = queryString;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
            return true;
        }

        public JSResults Select(string queryString)
        {
            JSResults result = new JSResults();

            try
            {
                conn.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = queryString;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JSResult item = new JSResult();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader.IsDBNull(i) == false)
                                {
                                    Console.WriteLine(reader.GetName(i) + " : " + reader.GetPostgresType(i).ToString());
                                    switch (reader.GetPostgresType(i).ToString())
                                    {
                                        //case :
                                        //    byte[] data = (byte[])reader[reader.GetName(i)];
                                        //    item.Add(reader.GetName(i), data);
                                        //    break;

                                        case "integer":
                                            item.Add(reader.GetName(i), reader.GetInt32(i));
                                            break;
                                        case "date":
                                            item.Add(reader.GetName(i), reader.GetDate(i));
                                            break;
                                        case "timestamp without time zone":
                                            item.Add(reader.GetName(i), reader.GetTimeStamp(i));
                                            break;
                                        case "jsonb":
                                        case "character":
                                        case "character varying":
                                            item.Add(reader.GetName(i), reader.GetString(i));
                                            break;
                                        default:
                                            Console.WriteLine("===================" + reader.GetName(i) + " : " + reader.GetPostgresType(i).ToString());
                                            break;
                                    }
                                }
                                else
                                {
                                    item.Add(reader.GetName(i), null);
                                }
                            }
                            result.Add(item);
                        }

                        reader.Close();
                    }
                }

            }
            catch (Npgsql.PostgresException ex)
            {
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public bool Update(string queryString)
        {
            return Insert(queryString);
        }

        public bool Delete(string queryString)
        {
            return Insert(queryString);
        }
        #endregion



    }
}
