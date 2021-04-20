using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using _2_Laboras.Models;
using _2_Laboras.ViewModels;

namespace _2_Laboras.Repos
{
    public class DarbuotojasRepository
    {

        public List<DarbuotojasViewModel> getDarbuotojai(int id)
        {
            List<DarbuotojasViewModel> darbuotojasViewModels = new List<DarbuotojasViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "SELECT * FROM darbuotojas where fk_KREPŠINIO_KOMANDA=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                darbuotojasViewModels.Add(new DarbuotojasViewModel
                {
                    Vardas = Convert.ToString(item["Vardas"]),
                    Pavarde = Convert.ToString(item["Pavarde"]),
                    EinamosPareigos = Convert.ToString(item["Einamos_pareigos"]),
                    id = Convert.ToInt32(item["id_DARBUOTOJAS"]),
                    fk_krepsinioKomanda = Convert.ToInt32(item["fk_KREPŠINIO_KOMANDA"])
                });
            }

                return darbuotojasViewModels;
        }


        private int getLastID()
        {
            int id = 0;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT id_DARBUOTOJAS FROM darbuotojas ORDER BY id_DARBUOTOJAS DESC LIMIT 1";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                id = Convert.ToInt32(item["id_DARBUOTOJAS"]);
            }

            return id + 1;
        }
        public bool addDarbuotojas(DarbuotojasViewModel darbuotojasViewModel)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "INSERT INTO darbuotojas(Vardas,Pavarde,Einamos_pareigos,id_DARBUOTOJAS,fk_KREPŠINIO_KOMANDA) " +
                               "VALUES(?vardas,?pavarde,?einamosPareigos,?id,?fk_krepsinioKomanda)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = darbuotojasViewModel.Vardas;
            mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = darbuotojasViewModel.Pavarde;
            mySqlCommand.Parameters.Add("?einamosPareigos", MySqlDbType.VarChar).Value = darbuotojasViewModel.EinamosPareigos;
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = getLastID();
            mySqlCommand.Parameters.Add("?fk_krepsinioKomanda", MySqlDbType.VarChar).Value = darbuotojasViewModel.fk_krepsinioKomanda;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public void deleteDarbuotojas(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM darbuotojas where fk_KREPŠINIO_KOMANDA=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

        }
    }
}