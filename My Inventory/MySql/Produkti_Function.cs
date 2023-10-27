using My_Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Inventory.MySql
{
    public class Produkti_Function
    {
        public static string connestion_string = MySql_Connection_String.ConnectionString;
        public static MySqlConnection conn = new MySqlConnection(connestion_string);

        public static List<Produkt> get_produktet()
        {
            try
            {
                List<Produkt> list_produkte = new List<Produkt>();

                string querry = "SELECT produktet.ID, produktet.Emri, produktet.Cmimi, produktet.Stock, kategorit.Emri FROM produktet INNER JOIN kategorit ON produktet.Kategori_ID = kategorit.ID";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Produkt p = new Produkt();
                        p.ID = int.Parse(dr[0].ToString());
                        p.Produkt_Name = dr[1].ToString();
                        p.Price = int.Parse(dr[2].ToString());
                        p.Stock = int.Parse(dr[3].ToString());
                        p.Kategori_Name = dr[4].ToString();
                        list_produkte.Add(p);
                    }
                    conn.Close();
                    //MessageBox.Show("done");
                    return list_produkte;
                }
                else
                {
                    MessageBox.Show("There are no products in database.");
                    conn.Close();
                    return list_produkte;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                List<Produkt> list_produkte = null;
                return list_produkte;
            }
        }

        public static List<Produkt> get_produktet_per_kategori(string category_name)
        {
            try
            {
                List<Produkt> list_produkte = new List<Produkt>();

                string querry = "SELECT Emri, Cmimi FROM produktet WHERE Kategori_ID = (SELECT ID FROM kategorit WHERE Emri = '" + category_name + "')";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Produkt p = new Produkt();
                        p.Produkt_Name = dr[0].ToString();
                        p.Price = int.Parse(dr[1].ToString());
                        list_produkte.Add(p);
                    }
                    conn.Close();
                    //MessageBox.Show("done");
                    return list_produkte;
                }
                else
                {
                    MessageBox.Show("There are no products in database.");
                    conn.Close();
                    return list_produkte;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                List<Produkt> list_produkte = null;
                return list_produkte;
            }
        }

        public void delete_produkt(int id)
        {
            try
            {
                string querry = "DELETE FROM produktet WHERE ID = " + id + ";";

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(querry, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Product has been deleted!");

                conn.Close();
            }
            catch (MySqlException ex)
            {
                if ((ex.Source == "MySql.Data") && (ex.Number == 1451))
                {
                    MessageBox.Show("This product can't be deleted!");
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }

        public void update_produkt(int id, string produkt_name, string price, string kategori_name)
        {
            try
            {
                string querry = "UPDATE produktet SET Emri = '" + produkt_name + "', Cmimi = " + price + ", Kategori_ID = (SELECT ID FROM kategorit WHERE Emri = '" + kategori_name + "') WHERE ID = " + id + ";";

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(querry, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Product has been updated!");

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conn.Close();
            }
        }

        public void add_produkt(string produkt_name, string price, string kategori_name)
        {
            try
            {
                string querry = "INSERT INTO produktet (Emri, Cmimi, Kategori_ID) VALUES ('" + produkt_name + "', " + price + ", (SELECT ID FROM kategorit WHERE Emri = '" + kategori_name + "'))";

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(querry, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Product has been added!");

                conn.Close();
            }
            catch (MySqlException ex)
            {
                if ((ex.Source == "MySql.Data") && (ex.Number == 1062))
                {
                    MessageBox.Show("This product alwready exists.");
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }

        public static int sell_produkte(string produkt_name, string sasia)
        {
            try
            {
                string data = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
                string querry = "INSERT INTO shitjet (Produkt_ID, Sasia, Data) VALUES ((SELECT ID FROM produktet WHERE Emri='" + produkt_name + "'), " + sasia + ", '" + data + "'); " +
                    "UPDATE produktet SET Stock = Stock - " + sasia + " WHERE Emri = '" + produkt_name + "';";

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(querry, conn);

                cmd.ExecuteNonQuery();

                conn.Close();

                return 1;

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
                return -1;
            }
        }

        public static void supply_produkte(string produkt_name, string sasia, string cmimi)
        {
            try
            {
                string data = DateTime.Now.ToString("yyyy-MM-dd hh:mm");

                string querry = "INSERT INTO furnizimet (Produkt_ID, Sasia, Cmimi, Data) VALUES ((SELECT ID FROM produktet WHERE Emri='" + produkt_name + "'), " + sasia + ", " + cmimi + ", '" + data + "'); " +
                    "UPDATE produktet SET Stock = Stock + " + sasia + " WHERE Emri = '" + produkt_name + "';";

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(querry, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Supply was successfull!");

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }
        }

        public static List<Furnizimet> get_furnizimet()
        {
            try
            {
                List<Furnizimet> list = new List<Furnizimet>();

                string querry = "SELECT produktet.Emri, furnizimet.Sasia, furnizimet.Cmimi, (furnizimet.Sasia * furnizimet.Cmimi) AS Total, Data FROM produktet INNER JOIN furnizimet on furnizimet.Produkt_ID = produktet.ID ORDER by furnizimet.ID DESC";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Furnizimet f = new Furnizimet();
                        f.Produkt_Name = dr[0].ToString();
                        f.Sasia = dr[1].ToString();
                        f.Cmimi = dr[2].ToString();
                        f.Total = dr[3].ToString();
                        f.Data = dr[4].ToString();
                        list.Add(f);
                    }
                    conn.Close();
                    //MessageBox.Show("done");
                    return list;
                }
                else
                {
                    MessageBox.Show("There are no records in database.");
                    conn.Close();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                List<Furnizimet> list = null;
                return list;
            }
        }

        public static List<Shitje> get_shitjet()
        {
            try
            {
                List<Shitje> list = new List<Shitje>();

                string querry = "SELECT produktet.Emri, shitjet.Sasia, produktet.Cmimi, (shitjet.Sasia * produktet.Cmimi) AS Total, Data FROM produktet INNER JOIN shitjet on shitjet.Produkt_ID = produktet.ID ORDER by shitjet.ID DESC";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Shitje sh = new Shitje();
                        sh.Produkt_Name = dr[0].ToString();
                        sh.Sasia = dr[1].ToString();
                        sh.Cmimi = dr[2].ToString();
                        sh.Total = dr[3].ToString();
                        sh.Data = dr[4].ToString();
                        list.Add(sh);
                    }
                    conn.Close();
                    //MessageBox.Show("done");
                    return list;
                }
                else
                {
                    MessageBox.Show("There are no records in database.");
                    conn.Close();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                List<Shitje> list = null;
                return list;
            }
        }

        public static bool check_stock(string produkt_name, string sasia)
        {
            try
            {
                string querry = "SELECT Stock FROM produktet WHERE Emri = '" + produkt_name + "' limit 1";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int stock = int.Parse(dr[0].ToString());

                    conn.Close();

                    if (stock >= int.Parse(sasia))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("There is not enough stock for this sale: " + produkt_name + "");
                        return false;
                    }
                    //MessageBox.Show("done");

                }
                else
                {
                    MessageBox.Show("There is not enough stock for this sale: " + produkt_name + "");
                    conn.Close();
                    return false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conn.Close();
                return false;
            }
        }
    }
}

