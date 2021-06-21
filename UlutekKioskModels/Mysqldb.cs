using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UlutekKioskModels
{
    public static class Mysqldb
    {
        private static MySqlConnection Connection;
        public static bool Open()
        {
            bool rlt;
            try
            {
                if (Connection == null)
                {
                    Connection = new MySqlConnection(Globals.ConnectionString);
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

        public static void CheckConnection(MySqlCommand cmd = null, MySqlDataAdapter adapter = null)
        {
            if (cmd != null && cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            if (adapter != null && adapter.SelectCommand.Connection.State != ConnectionState.Open)
            {
                adapter.SelectCommand.Connection.Open();
            }
        }

        public static DataSet Select(string query)
        {
            DataSet rlt = new DataSet();
            using (MySqlDataAdapter da = new MySqlDataAdapter(query, Connection))
            {
                CheckConnection(adapter: da);
                da.Fill(rlt);
            }
            return rlt;
        }

        public static bool Insert(string query)
        {
            bool rlt = false;
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                CheckConnection(cmd: cmd);
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
                CheckConnection(cmd: cmd);
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
                CheckConnection(cmd: cmd);
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
                CheckConnection(cmd: cmd);
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
                CheckConnection(cmd: cmd);
                cmd.ExecuteNonQuery();
                rlt = true;
            }
            return rlt;
        }
    }   
}   
