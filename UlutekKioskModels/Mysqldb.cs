using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UlutekKioskModels
{
    public class Mysqldb
    {
        public static MySqlConnection Connection = null;
        public static string ConnectionString {get; set;}
        public static ConnectionState State { get { return Connection.State; } }

        public static bool Open()
        {
            bool rlt;
            try
            {
                if (Connection == null)
                {
                    Connection = new MySqlConnection(ConnectionString);
                }

                Connection.Open();
                rlt = true;
            }
            catch (Exception)
            {
                rlt = false;
            }

            return rlt;
        }

        public static DataSet Select(string query)
        {
            DataSet rlt = new DataSet();
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(rlt);
            }
            return rlt;
        }

        public static bool Insert(string query)
        {
            bool rlt = false;
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
                rlt = true;
            }
            return rlt;
        }

        public static bool Insert(string query, List<MySqlParameter> parameters)
        {
            bool rlt = false;
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                cmd.Parameters.AddRange(parameters.ToArray());
                 cmd.ExecuteNonQuery();
                rlt = true;
            }
            return rlt;
        }

        public static bool Update(string query)
        {
            bool rlt = false;
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
                rlt = true;
            }
            return rlt;
        }

        public static bool Update(string query, List<MySqlParameter> parameters)
        {
            bool rlt = false;
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                cmd.Parameters.AddRange(parameters.ToArray());
                cmd.ExecuteNonQuery();
                rlt = true;
            }
            return rlt;
        }

        public static bool Delete(string query)
        {
            bool rlt = false;
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
                rlt = true;
            }
            return rlt;
        }

        public static string GetConnectionString(string ServerIP, string Database, string Username, string Pwd)
        {
            string rlt = $"Server={ServerIP};Database={Database};Uid={Username};Pwd={Pwd};";
            ConnectionString = rlt;
            return rlt;
        }

        public static void Close()
        {
            Connection.Close();
        }

    }   
}   
