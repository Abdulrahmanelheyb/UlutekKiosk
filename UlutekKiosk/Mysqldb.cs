using System;
using System.Data;
using System.Net.NetworkInformation;
using MySql.Data.MySqlClient;

namespace SBSS_Models
{
    public class Mysqldb
    {
        // This mysqldb class is contain all mysql services.

        public static string ServerIP = Properties.Settings.Default.ServerIP; //"192.168.0.134";
        private static string Database = "sbss";
        private static string Uid = "schoolserver";
        private static string Pwd = "School+admin3";
        public static ConnectionState State { get { return Connection.State; } }

        private static MySqlConnection Connection = new MySqlConnection($"Server={ServerIP};Database={Database};Uid={Uid};Pwd={Pwd}; Connect Timeout=1");

        public static void OpenConnection()
        {
            try
            {
                Connection.OpenAsync();
            }
            catch (Exception) { }
        }

        public static void Close()
        {
            Connection.Close();
        }

        public static DataSet Select(string query)
        {
            // Read data from mysql server
            DataSet dt = new DataSet();
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable() && Connection.State == ConnectionState.Open)
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection)
                    {
                        CommandTimeout = 0
                    };
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex) { throw ex; }
            return dt;
        }

        public static void Update(string query)
        {
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable() && Connection.State == ConnectionState.Open)
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection)
                    {
                        CommandTimeout = 0
                    };
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
