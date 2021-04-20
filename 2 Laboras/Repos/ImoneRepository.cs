using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using _2_Laboras.ViewModels;

namespace _2_Laboras.Repos
{
    public class ImoneRepository
    {
        public List<ImoneViewModel> getImones()
        {
            List<ImoneViewModel> imones = new List<ImoneViewModel>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "SELECT * FROM imone";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach(DataRow item in dt.Rows)
            {
                imones.Add(new ImoneViewModel
                {
                    id = Convert.ToInt32(item["id_IMONE"]),
                    Pavadinimas = Convert.ToString(item["Pavadinimas"]),
                    GaminamaProdukcija = Convert.ToString(item["Gaminama_produkcija"])
                });
            }

            return imones;
        }
    }
}