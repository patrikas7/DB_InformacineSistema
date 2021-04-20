using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using _2_Laboras.ViewModels;

namespace _2_Laboras.Repos
{
    public class AsistentasRepository
    {
        public List<AsistenasViewModel> getAsistentai()
        {
            List<AsistenasViewModel> asistenasViewModels = new List<AsistenasViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.id_ASISTENTAS, m.Vardas, m.Pavarde, m.Tautybe, CONCAT(mm.Vardas,"" "", mm.Pavarde)" +
                "AS Treneris FROM asistentas m LEFT JOIN treneris mm ON mm.id__TRENERIS=m.fk__TRENERIS";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                asistenasViewModels.Add(new AsistenasViewModel
                {
                    id = Convert.ToInt32(item["id_ASISTENTAS"]),
                    Vardas = Convert.ToString(item["Vardas"]),
                    Pavarde = Convert.ToString(item["Pavarde"]),
                    Tautybe = Convert.ToString(item["Tautybe"]),
                    Treneris = Convert.ToString(item["Treneris"])
                });
            }
                return asistenasViewModels;

        }

        public AsistentasEditViewModel getAsistentas(int id)
        {
            AsistentasEditViewModel asistentas = new AsistentasEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.* FROM asistentas m WHERE m.id_ASISTENTAS=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach(DataRow item in dt.Rows)
            {
                asistentas.Vardas = Convert.ToString(item["Vardas"]);
                asistentas.Pavarde = Convert.ToString(item["Pavarde"]);
                asistentas.Tautybe = Convert.ToString(item["Tautybe"]);
                asistentas.id = Convert.ToInt32(item["id_ASISTENTAS"]);
                asistentas.fk_treneris = Convert.ToInt32(item["fk__TRENERIS"]);
            }

            return asistentas;
        }

        public bool updateAsistentas(AsistentasEditViewModel asistentas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE asistentas a SET a.Vardas=?vardas, a.Pavarde=?pavarde, a.Tautybe=?tautybe, a.fk__TRENERIS=?treneris WHERE a.id_ASISTENTAS=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = asistentas.id;
            mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = asistentas.Vardas;
            mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = asistentas.Pavarde;
            mySqlCommand.Parameters.Add("?tautybe", MySqlDbType.VarChar).Value = asistentas.Tautybe;
            mySqlCommand.Parameters.Add("?treneris", MySqlDbType.Int32).Value = asistentas.fk_treneris;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addAsistentas(AsistentasEditViewModel asistentas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO asistentas(Vardas,Pavarde,Tautybe,fk__TRENERIS,id_ASISTENTAS) VALUES(?vardas,?pavarde,?tautybe,?treneris,?id)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = asistentas.Vardas;
            mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = asistentas.Pavarde;
            mySqlCommand.Parameters.Add("?tautybe", MySqlDbType.VarChar).Value = asistentas.Tautybe;
            mySqlCommand.Parameters.Add("?treneris", MySqlDbType.Int32).Value = asistentas.fk_treneris;
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = getID();
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public int getAsistentasCount(int id)
        {
            int count = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT COUNT(id_ASISTENTAS) as kiekis FROM asistentas WHERE fk__TRENERIS=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                count = Convert.ToInt32(item["kiekis"] == DBNull.Value ? 0 : item["kiekis"]);
            }

            return count;
        }

        private int getID()
        {
            int id = 0;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT id_ASISTENTAS FROM asistentas ORDER BY id_ASISTENTAS DESC LIMIT 1";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                id = Convert.ToInt32(item["id_ASISTENTAS"]);
            }
            return id + 1;
        }

        public void deleteAsistentas(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM asistentas where id_ASISTENTAS=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}