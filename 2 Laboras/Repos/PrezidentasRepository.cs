using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using _2_Laboras.ViewModels;
using _2_Laboras.Models;

namespace _2_Laboras.Repos
{
    public class PrezidentasRepository
    {

        private int getLastID()
        {
            int id = 0;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT id_KADENCIJA FROM kadencija ORDER BY id_KADENCIJA DESC LIMIT 1";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                id = Convert.ToInt32(item["id_KADENCIJA"]);
            }

            return id + 1;
        }

        public bool addPrezidentoKadencija(int lygaID, PrezidentoKadencija prezidentoKadencija)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "INSERT INTO kadencija(Pradžia, Pabaiga, id_KADENCIJA, fk_KREPŠINIO_LYGA, fk_PREZIDENTAS) " +
                               "VALUES(?pradzia,?pabaiga,?id,?fk_lyga,?fk_prezidentas)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pradzia", MySqlDbType.Date).Value = prezidentoKadencija.KadencijosPradzia;
            mySqlCommand.Parameters.Add("?pabaiga", MySqlDbType.Date).Value = prezidentoKadencija.KadencijosPabaiga;
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = getLastID();
            mySqlCommand.Parameters.Add("?fk_lyga", MySqlDbType.Int32).Value = lygaID;
            mySqlCommand.Parameters.Add("?fk_prezidentas", MySqlDbType.Int32).Value = prezidentoKadencija.fk_prezidentas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }
        public List<PrezidentasViewModel> getPrezidentai()
        {
            List<PrezidentasViewModel> prezidentai = new List<PrezidentasViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "SELECT * FROM prezidentas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                prezidentai.Add(new PrezidentasViewModel
                {
                    id = Convert.ToInt32(item["id_PREZIDENTAS"]),
                    Vardas = Convert.ToString(item["Vardas"]),
                    Pavarde = Convert.ToString(item["Pavarde"])
                });
            }


            return prezidentai;
        }
        
        private PrezidentasViewModel getPrezidentas(int id)
        {
            PrezidentasViewModel prezidentasViewModel = new PrezidentasViewModel();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "SELECT * FROM prezidentas WHERE id_PREZIDENTAS=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                prezidentasViewModel.Vardas = Convert.ToString(item["Vardas"]);
                prezidentasViewModel.Pavarde = Convert.ToString(item["Pavarde"]);
            }

                return prezidentasViewModel;
        }

        public List<PrezidentoKadencija> getKadencijos(int id)
        {
            List<PrezidentoKadencija> prezidentoKadencijos = new List<PrezidentoKadencija>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "SELECT * FROM kadencija where fk_KREPŠINIO_LYGA="+id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {

                prezidentoKadencijos.Add(new PrezidentoKadencija
                {
                    KadencijosPradzia = Convert.ToDateTime(item["Pradžia"]),
                    KadencijosPabaiga = Convert.ToDateTime(item["Pabaiga"]),
                    fk_prezidentas = Convert.ToInt32(item["fk_PREZIDENTAS"]),
                    fk_komanda = Convert.ToInt32(item["fk_KREPŠINIO_LYGA"]),
                    vardas = getPrezidentas(Convert.ToInt32(item["fk_PREZIDENTAS"])).Vardas,
                    pavarde = getPrezidentas(Convert.ToInt32(item["fk_PREZIDENTAS"])).Pavarde
                }); 
            }

            return prezidentoKadencijos;
        }



        public void deleteKadencija(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "DELETE FROM kadencija WHERE fk_KREPŠINIO_LYGA=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}