using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using _2_Laboras.Models;
using MySql.Data.MySqlClient;
using _2_Laboras.ViewModels;

namespace _2_Laboras.Repos
{
    public class ArenaRepository
    {
        public List<ArenaViewModel> getArena()
        {
            List<ArenaViewModel> arenos = new List<ArenaViewModel>();
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = @"SELECT m.id_ARENA, m.Pavadinimas, m.Talpa, m.Pastatymo_metai, m.Adresas, mm.Pavadinimas 
            AS miestas FROM arena m LEFT JOIN miestas mm ON mm.id_MIESTAS=m.fk_MIESTAS";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                arenos.Add(new ArenaViewModel
                {
                    id = Convert.ToInt32(item["id_Arena"]),
                    Talpa = Convert.ToInt32(item["Talpa"]),
                    Pavadinimas = Convert.ToString(item["Pavadinimas"]),
                    Miestas = Convert.ToString(item["Miestas"]),
                    Pastatymo_metai = Convert.ToInt32(item["Pastatymo_metai"]),
                    Adresas = Convert.ToString(item["Adresas"]),
                });
            }

            return arenos;
        }

        public ArenaEditViewModel getArena(int id)
        {
            ArenaEditViewModel arena = new ArenaEditViewModel();
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = @"SELECT m.* FROM arena m WHERE m.id_ARENA=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                arena.Pavadinimas = Convert.ToString(item["Pavadinimas"]);
                arena.id = Convert.ToInt32(item["id_Arena"]);
                arena.Miestas = Convert.ToString(item["Miestas"]);
                arena.Pastatymo_metai = Convert.ToInt32(item["Pastatymo_metai"]);
                arena.Adresas = Convert.ToString(item["Adresas"]);
                arena.fk_miestas = Convert.ToInt32(item["fk_MIESTAS"]);
            }

            return arena;
        }

        public List<Miestas> getMiestai()
        {
            List<Miestas> miestai = new List<Miestas>();
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "select * from miestas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                miestai.Add(new Miestas
                {
                    id = Convert.ToInt32(item["id_MIESTAS"]),
                    Pavadinimas = Convert.ToString(item["Pavadinimas"]),
                    Gyventoju_skaicius = Convert.ToInt32(item["Gyventoju_skaicius"])
                });
            }

            return miestai;
        }

        private string getMiestas(int id)
        {
            string miestas = string.Empty;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = @"SELECT Pavadinimas FROM miestas WHERE id_MIESTAS=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                miestas = Convert.ToString(item["Pavadinimas"]);
            }

            return miestas;
        }

        public bool addArena(ArenaEditViewModel arena)
        {

                string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(connection);
                string sqlquery = @"INSERT INTO arena(Pavadinimas,Talpa,Miestas,Pastatymo_metai,Adresas,id_ARENA, fk_MIESTAS) VALUES(?pavadinimas,?talpa,?miestas,?pastatymoMetai,?adresas,?id,?fk)";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = arena.Pavadinimas;
                mySqlCommand.Parameters.Add("?talpa", MySqlDbType.Int32).Value = arena.Talpa;
                mySqlCommand.Parameters.Add("?miestas", MySqlDbType.VarChar).Value = getMiestas(arena.fk_miestas);
                mySqlCommand.Parameters.Add("?pastatymoMetai", MySqlDbType.Int32).Value = arena.Pastatymo_metai;
                mySqlCommand.Parameters.Add("?adresas", MySqlDbType.VarChar).Value = arena.Adresas;
                mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = getID();
                mySqlCommand.Parameters.Add("?fk", MySqlDbType.Int32).Value = arena.fk_miestas;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();

                return true;
        }

        private int getID()
        {
            int id = 0;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT id_ARENA FROM arena ORDER BY id_ARENA DESC LIMIT 1";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                id = Convert.ToInt32(item["id_ARENA"]);
            }


            return id + 1;
        }

        public bool updateArena(ArenaEditViewModel arena)
        {
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = @"UPDATE arena SET Pavadinimas=?pavadinimas, Talpa=?talpa, Miestas=?miestas, Pastatymo_metai=?pastatymoMetai, Adresas=?adresas, fk_MIESTAS=?fkMiestas WHERE id_ARENA=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = arena.id;
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = arena.Pavadinimas;
            mySqlCommand.Parameters.Add("?talpa", MySqlDbType.Int32).Value = arena.Talpa;
            mySqlCommand.Parameters.Add("?miestas", MySqlDbType.VarChar).Value = getMiestas(arena.fk_miestas);
            mySqlCommand.Parameters.Add("?pastatymoMetai", MySqlDbType.Int32).Value = arena.Pastatymo_metai;
            mySqlCommand.Parameters.Add("?adresas", MySqlDbType.VarChar).Value = arena.Adresas;
            mySqlCommand.Parameters.Add("?fkMiestas", MySqlDbType.Int32).Value = arena.fk_miestas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public void deleteArena(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM arena where id_ARENA=?id";
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
            string sqlquery = "SELECT COUNT(Pavadinimas) as skaicius FROM arena WHERE Pavadinimas=?pavadinimas";
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
            string sqlquery = "SELECT COUNT(Pavadinimas) as skaicius FROM arena WHERE Pavadinimas=?pavadinimas AND id_ARENA !=" + id;
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