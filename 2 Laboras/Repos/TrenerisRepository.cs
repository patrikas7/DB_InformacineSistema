using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using _2_Laboras.Models;
using MySql.Data.MySqlClient;

namespace _2_Laboras.Repos
{
    public class TrenerisRepository
    {
        public List<Treneris> getTreneris()
        {
            List<Treneris> treneriai = new List<Treneris>();
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT * FROM treneris";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                treneriai.Add(new Treneris
                {
                    Vardas = Convert.ToString(item["Vardas"]),
                    Pavarde = Convert.ToString(item["Pavarde"]),
                    Tautybe = Convert.ToString(item["Tautybe"]),
                    id = Convert.ToInt32(item["id__TRENERIS"])
                });
            }
            return treneriai;
        }

        public Treneris getTreneris(int id)
        {
            Treneris treneris = new Treneris();
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT * FROM treneris where id__TRENERIS=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                treneris.Vardas = Convert.ToString(item["Vardas"]);
                treneris.Pavarde = Convert.ToString(item["Pavarde"]);
                treneris.Tautybe = Convert.ToString(item["Tautybe"]);
                treneris.id = Convert.ToInt32(item["id__TRENERIS"]);

            }

            return treneris;
        }

        public bool addTreneris(Treneris treneris)
        {
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(connection);
                string sqlquery = @"INSERT INTO treneris(Vardas,Pavarde,Tautybe,id__TRENERIS)
                VALUES(?vardas,?pavarde,?tautybe,?id);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = treneris.Vardas;
                mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = treneris.Pavarde;
                mySqlCommand.Parameters.Add("?tautybe", MySqlDbType.VarChar).Value = treneris.Tautybe;
                mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = getLastID();
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int getLastID()
        {
            int id = 0;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT id__TRENERIS FROM treneris ORDER BY id__TRENERIS DESC LIMIT 1";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                id = Convert.ToInt32(item["id__TRENERIS"]);
            }

            return id + 1;
        }

        public bool updateTreneris(Treneris treneris)
        {
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(connection);
                string sqlquery = @"UPDATE treneris a SET a.Vardas=?vardas, a.Pavarde=?pavarde, a.Tautybe=?tautybe WHERE a.id__TRENERIS=?id";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = treneris.id;
                mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = treneris.Vardas;
                mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = treneris.Pavarde;
                mySqlCommand.Parameters.Add("?tautybe", MySqlDbType.VarChar).Value = treneris.Tautybe;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public void deleteTreneris(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM treneris where id__TRENERIS=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public List<Asistentas> getAsistentai(int id)
        {
            List<Asistentas> asistentas = new List<Asistentas>();
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT * FROM asistentas where fk__TRENERIS=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                asistentas.Add(new Asistentas
                {
                    Vardas = Convert.ToString(item["Vardas"]),
                    Pavarde = Convert.ToString(item["Pavarde"]),
                    Tautybe = Convert.ToString(item["Tautybe"]),
                });
            }

            return asistentas;
        }
    }
  
}