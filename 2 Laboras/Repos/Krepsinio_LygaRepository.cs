using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using _2_Laboras.Models;
using _2_Laboras.ViewModels;
using MySql.Data.MySqlClient;

namespace _2_Laboras.Repos
{
    public class Krepsinio_LygaRepository
    {
        public List<Krepsinio_lyga> getLyga()
        {
            List<Krepsinio_lyga> lyga = new List<Krepsinio_lyga>();
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT * FROM krepšinio_lyga";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                lyga.Add(new Krepsinio_lyga {
                    Lygos_pavadinimas = Convert.ToString(item["Lygos_pavadinimas"]),
                    Prizinis_fondas = Convert.ToInt32(item["Prizinis_fondas"]),
                    Komandu_skaicius = Convert.ToInt32(item["Komandu_skaicius"]),
                    Turnyro_trukme = Convert.ToString(item["Turnyro_trukme"]),
                    Formatas = Convert.ToString(item["Formatas"]),
                    Remejas = Convert.ToString(item["Remejas"]),
                    id = Convert.ToInt32(item["id_KREPŠINIO_LYGA"])

                });
            }

            return lyga;
        }

        public KrepsinioLygaEditViewModel getLyga(int id)
        {
            KrepsinioLygaEditViewModel krepsinio_Lyga = new KrepsinioLygaEditViewModel();
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT * FROM krepšinio_lyga where id_KREPŠINIO_LYGA=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                krepsinio_Lyga.LygosPavadinimas = Convert.ToString(item["Lygos_pavadinimas"]);
                krepsinio_Lyga.PrizinisFondas = Convert.ToInt32(item["Prizinis_fondas"]);
                krepsinio_Lyga.KomanduSkaicius = Convert.ToInt32(item["Komandu_skaicius"]);
                krepsinio_Lyga.TurnyroTrukme = Convert.ToString(item["Turnyro_trukme"]);
                krepsinio_Lyga.Formatas = Convert.ToString(item["Formatas"]);
                krepsinio_Lyga.Remejas = Convert.ToString(item["Remejas"]);
                krepsinio_Lyga.fk_remejas = getRemejoID(id);
                krepsinio_Lyga.id = Convert.ToInt32(item["id_KREPŠINIO_LYGA"]);

            }

            return krepsinio_Lyga;
        }

        private string getRemejoPavadinimas(int id)
        {
            string pavadinimas = string.Empty;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = @"SELECT Pavadinimas FROM imone WHERE id_IMONE=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                pavadinimas = Convert.ToString(item["Pavadinimas"]);
            }


            return pavadinimas;
        }

        private int getRemejoID(int id)
        {
            int fk = 0;

            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = @"SELECT fk_IMONE FROM remia WHERE fk_KREPŠINIO_LYGA=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                fk = Convert.ToInt32(item["fk_IMONE"]);
            }

            return fk;
        }

        public int getIDbyName(string pavadinimas)
        {
            int id = 0;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT id_KREPŠINIO_LYGA FROM krepšinio_lyga WHERE Lygos_pavadinimas=?pavadinimas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = pavadinimas;
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                id = Convert.ToInt32(item["id_KREPŠINIO_LYGA"]);
            }


            return id;
        }
        public int addLyga(KrepsinioLygaEditViewModel krepsinio_Lyga)
        {

            try
            {
                string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(connection);
                string sqlquery = "BEGIN; " +
                                   "INSERT INTO krepšinio_lyga(Lygos_pavadinimas,Prizinis_fondas,Komandu_skaicius,Turnyro_trukme,Formatas,Remejas,id_KREPŠINIO_LYGA) " +
                                   "VALUES(?pavadinimas,?prizinisFondas,?komanduSkaicius,?trukme,?formatas,?remejas,?id); " +
                                   "INSERT INTO remia(fk_KREPŠINIO_LYGA,fk_IMONE) " +
                                   "VALUES(?id,?fk_imone); " +
                                   "COMMIT;";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = krepsinio_Lyga.LygosPavadinimas;
                mySqlCommand.Parameters.Add("?prizinisFondas", MySqlDbType.Int32).Value = krepsinio_Lyga.PrizinisFondas;
                mySqlCommand.Parameters.Add("?komanduSkaicius", MySqlDbType.Int32).Value = krepsinio_Lyga.KomanduSkaicius;
                mySqlCommand.Parameters.Add("?trukme", MySqlDbType.VarChar).Value = krepsinio_Lyga.TurnyroTrukme;
                mySqlCommand.Parameters.Add("?formatas", MySqlDbType.VarChar).Value = krepsinio_Lyga.Formatas;
                mySqlCommand.Parameters.Add("?remejas", MySqlDbType.VarChar).Value = getRemejoPavadinimas(krepsinio_Lyga.fk_remejas);
                mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = getLastID();
                mySqlCommand.Parameters.Add("?fk_imone", MySqlDbType.Int32).Value = krepsinio_Lyga.fk_remejas;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();

                return krepsinio_Lyga.PrezidentoKadencijos.Count;
            }
            catch(Exception)
            {
                return 100;
            }

        }

        private int getLastID()
        {
            int id = 0;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT id_KREPŠINIO_LYGA FROM krepšinio_lyga ORDER BY id_KREPŠINIO_LYGA DESC LIMIT 1";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                id = Convert.ToInt32(item["id_KREPŠINIO_LYGA"]);
            }


            return id + 1;
        }

        public bool updateLyga(int id,KrepsinioLygaEditViewModel krepsinio_Lyga)
        {
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(connection);
                string sqlquery = @"UPDATE krepšinio_lyga
                                    SET Lygos_pavadinimas=?pavadinimas,Prizinis_fondas=?prizinisFondas,Komandu_skaicius=?komanduSkaicius,Turnyro_trukme=?trukme,Formatas=?formatas,Remejas=?remejas
                                    WHERE id_KREPŠINIO_LYGA=" + id;

                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = krepsinio_Lyga.LygosPavadinimas;
                mySqlCommand.Parameters.Add("?prizinisFondas", MySqlDbType.Int32).Value = krepsinio_Lyga.PrizinisFondas;
                mySqlCommand.Parameters.Add("?komanduSkaicius", MySqlDbType.Int32).Value = krepsinio_Lyga.KomanduSkaicius;
                mySqlCommand.Parameters.Add("?trukme", MySqlDbType.VarChar).Value = krepsinio_Lyga.TurnyroTrukme;
                mySqlCommand.Parameters.Add("?formatas", MySqlDbType.VarChar).Value = krepsinio_Lyga.Formatas;
                mySqlCommand.Parameters.Add("?remejas", MySqlDbType.VarChar).Value = getRemejoPavadinimas(krepsinio_Lyga.fk_remejas);
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                updateRemimas(id, krepsinio_Lyga.fk_remejas);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void updateRemimas(int id,int fk)
        {
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "UPDATE remia SET fk_imone=?fk WHERE fk_KREPŠINIO_LYGA=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?fk", MySqlDbType.Int32).Value = fk;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        private void deleteRemimas(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "DELETE FROM remia WHERE fk_KREPŠINIO_LYGA=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
        public void deleteLyga(int id)
        {
            deleteRemimas(id);
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "DELETE FROM krepšinio_lyga WHERE id_KREPŠINIO_LYGA=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public bool findDublicates(string pavadinimas)
        {

            int temp = 0;
            string connection = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            string sqlquery = "SELECT COUNT(Lygos_pavadinimas) as skaicius FROM krepšinio_lyga WHERE Lygos_pavadinimas=?pavadinimas";
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
            string sqlquery = "SELECT COUNT(Lygos_pavadinimas) as skaicius FROM krepšinio_lyga WHERE Lygos_pavadinimas=?pavadinimas AND id_KREPŠINIO_LYGA !=" + id;
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