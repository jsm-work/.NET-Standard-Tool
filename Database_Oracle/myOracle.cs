using Oracle.ManagedDataAccess.Client;
using System;
using Database_Item;
using System.Collections.Generic;

namespace Database_Oracle
{
    /// <summary>
    /// Oracle.ManagedDataAccess 설치 필요
    /// </summary>
    public class myOracle
    {
        private OracleConnection connToORACLE;
        string strHost;
        int nPort;
        string strSID;
        string strUserID;
        string strPassword;
        //string strDatabase;


        public myOracle(string host, string userId, string password, int port, string SIDorDATABASE = "")
        {
            strHost = host;
            nPort = port;
            strUserID = userId;
            strPassword = password;

            if (SIDorDATABASE == "")
            {
                Console.WriteLine("SID가 입력되지 않았습니다.");
                return;
            }
            strSID = SIDorDATABASE;
        }

        string GetConnectStringToOracle()
        {
            string result = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + strHost + ")(PORT=" + nPort + ")))(CONNECT_DATA=(SERVER=DEDICATED)(SID=" + strSID + ")));User Id=" + strUserID + ";Password=" + strPassword + ";";
            return result;
        }

        #region connect / unconnect
        public bool ConnectToOracle()
        {
            try
            {
                connToORACLE = new OracleConnection(GetConnectStringToOracle());

                connToORACLE.Open();
                return true;
            }
            catch (OracleException oex)
            {
                Console.WriteLine("Oracle DB에 연결할 수 없습니다.");
                return false;
            }
            return false;
        }
        public void UnconnectToOracle()
        {
            connToORACLE.Close();
        }
        #endregion

        #region Mysql / Oracle 사용법 통일
        public JSResults Select(string query)
        {
            if (true == ConnectToOracle())
            {
                JSResults result = new JSResults();
                Dictionary<int, byte[]> images = new Dictionary<int, byte[]>();

                OracleCommand oracle_cmd = new OracleCommand(query, connToORACLE);
                OracleDataReader oracle_reader = oracle_cmd.ExecuteReader();
                try
                {
                    while (oracle_reader.Read())
                    {
                        JSResult item = new JSResult();
                        for (int i = 0; i < oracle_reader.FieldCount; i++)
                        {
                            string s = oracle_reader.GetName(i);
                            item.Add(oracle_reader.GetName(i), (string)oracle_reader.GetValue(i).ToString());
                            //오라클 바이트처리 해야됨......
                        }
                        result.Add(item);
                    }
                }
                finally
                {
                    UnconnectToOracle();
                }
                if (result.Count != 0)
                    return result;
                else
                    return null;
            }
            return null;
        }
        #endregion


        public int Insert(string queryString)
        {
            if (true == ConnectToOracle())
            {
                try
                {
                    if (ConnectToOracle())
                    {
                        OracleCommand oracle_cmd = new OracleCommand(queryString, connToORACLE);
                        oracle_cmd.ExecuteNonQuery();
                        Console.WriteLine(queryString + "\r\n");
                        return 1;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    UnconnectToOracle();
                }
            }
            return -1;
        }

        /// <summary>
        /// Insert를 제외하고 입력
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public int Inserts(string[] queryString)
        {
            if (true == ConnectToOracle())
            {
                try
                {
                    if (ConnectToOracle())
                    {
                        string sql = "INSERT ALL \r\n";
                        foreach (string item in queryString)
                        {
                            sql += item + "\r\n";
                        }
                        sql += "SELECT * FROM DUAL";

                        OracleCommand oracle_cmd = new OracleCommand(sql, connToORACLE);
                        oracle_cmd.CommandType = System.Data.CommandType.Text;
                        oracle_cmd.ExecuteNonQuery();
                        Console.WriteLine(queryString + "\r\n");
                        return 1;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    UnconnectToOracle();
                }
            }
            return -1;
        }

    }
}
