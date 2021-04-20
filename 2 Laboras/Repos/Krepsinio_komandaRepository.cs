using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using _2_Laboras.Models;
using MySql.Data.MySqlClient;

namespace _2_Laboras.Repos
{
    public class Krepsinio_komandaRepository
    {
        public List<Krepsinio_komanda> getKrepsinio_Komanda()
        {
            List<Krepsinio_komanda> komandos = new List<Krepsinio_komanda>();
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT * FROM krepšinio_komanda";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                komandos.Add(new Krepsinio_komanda
                {
                    Pavadinimas = Convert.ToString(item["Pavadinimas"]),
                    Miestas = Convert.ToString(item["Miestas"]),
                    Treneris = Convert.ToString(item["Treneris"]),
                    Biudzetas = Convert.ToInt32(item["Biudžetas"]),
                    Arena = Convert.ToString(item["Arena"]),
                    Lygu_licenzija = Convert.ToString(item["Lygu_licenzija"]),
                    Laimejimai = Convert.ToString(item["Leimejimai"]),
                    id = Convert.ToInt32(item["id_KREPŠINIO_KOMANDA"])

                });
            }

            return komandos;
        }

        public Krepsinio_komanda getKrepsinio_Komanda(int id)
        {
            Krepsinio_komanda komanda = new Krepsinio_komanda();
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT * FROM krepšinio_komanda where id_KREPŠINIO_KOMANDA=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                komanda.Pavadinimas = Convert.ToString(item["Pavadinimas"]);
                komanda.Miestas = Convert.ToString(item["Miestas"]);
                komanda.Treneris = Convert.ToString(item["Treneris"]);
                komanda.Biudzetas = Convert.ToInt32(item["Biudžetas"]);
                komanda.Arena = Convert.ToString(item["Arena"]);
                komanda.Lygu_licenzija = Convert.ToString(item["Lygu_licenzija"]);
                komanda.Laimejimai = Convert.ToString(item["Leimejimai"]);
                komanda.id = Convert.ToInt32(item["id_KREPŠINIO_KOMANDA"]);

            }

            return komanda;
        }

        public int addKrepsinio_Komanda(Krepsinio_komanda komanda)
        {
            int id = getLastID();
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = @"INSERT INTO krepšinio_komanda(Pavadinimas,Miestas,Treneris,Biudžetas,Arena,Lygu_licenzija,Leimejimai,id_KREPŠINIO_KOMANDA) " +
                "VALUES(?pavadinimas,?miestas,?treneris,?biudzetas,?arena,?lygu_licenzija,?laimejimai,?id);";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = komanda.Pavadinimas;
            mySqlCommand.Parameters.Add("?miestas", MySqlDbType.VarChar).Value = komanda.Miestas;
            mySqlCommand.Parameters.Add("?treneris", MySqlDbType.VarChar).Value = komanda.Treneris;
            mySqlCommand.Parameters.Add("?biudzetas", MySqlDbType.Int32).Value = komanda.Biudzetas;
            mySqlCommand.Parameters.Add("?arena", MySqlDbType.VarChar).Value = komanda.Arena;
            mySqlCommand.Parameters.Add("?lygu_licenzija", MySqlDbType.VarChar).Value = komanda.Lygu_licenzija;
            mySqlCommand.Parameters.Add("?laimejimai", MySqlDbType.VarChar).Value = komanda.Laimejimai;
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return id;
        }

        private int getLastID()
        {
            int id = 0;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT id_KREPŠINIO_KOMANDA FROM krepšinio_komanda ORDER BY id_KREPŠINIO_KOMANDA DESC LIMIT 1";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                id = Convert.ToInt32(item["id_KREPŠINIO_KOMANDA"]);
            }

            return id + 1;
        }

        public bool updateKrepsinio_Komanda(int id, Krepsinio_komanda komanda)
        {
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(connection);
                string sqlquery = @"UPDATE krepšinio_komanda a SET a.Pavadinimas=?pavadinimas, a.Miestas=?miestas, a.Treneris=?treneris, a.Biudžetas=?biudzetas, a.Arena=?arena, a.Lygu_licenzija=?lygu_licenzija, a.Leimejimai=?laimejimai WHERE a.id_KREPŠINIO_KOMANDA="+id;
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = komanda.Pavadinimas;
                mySqlCommand.Parameters.Add("?miestas", MySqlDbType.VarChar).Value = komanda.Miestas;
                mySqlCommand.Parameters.Add("?treneris", MySqlDbType.VarChar).Value = komanda.Treneris;
                mySqlCommand.Parameters.Add("?biudzetas", MySqlDbType.Int32).Value = komanda.Biudzetas;
                mySqlCommand.Parameters.Add("?arena", MySqlDbType.VarChar).Value = komanda.Arena;
                mySqlCommand.Parameters.Add("?lygu_licenzija", MySqlDbType.VarChar).Value = komanda.Lygu_licenzija;
                mySqlCommand.Parameters.Add("?laimejimai", MySqlDbType.VarChar).Value = komanda.Laimejimai;
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

        public void deleteKrepsinio_Komanda(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "DELETE FROM krepšinio_komanda WHERE id_KREPŠINIO_KOMANDA="+id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public bool findDublicates(string pavadinimas)
        {

            int temp = 0;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT COUNT(Pavadinimas) as skaicius FROM krepšinio_komanda WHERE Pavadinimas=?pavadinimas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = pavadinimas;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                temp = Convert.ToInt32(item["skaicius"]);
            }

            return temp > 0;

        }

        public bool findDbulicatesForEdit(string pavadinimas, int id)
        {
            int temp = 0;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT COUNT(Pavadinimas) as skaicius FROM krepšinio_komanda WHERE Pavadinimas=?pavadinimas AND id_KREPŠINIO_KOMANDA !=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = pavadinimas;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                temp = Convert.ToInt32(item["skaicius"]);
            }

            return temp > 0;
        }
    }
}