using My_Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Inventory.MySql
{
    public class Kategori_Function
    {
        public static string connestion_string = MySql_Connection_String.ConnectionString;
        public static MySqlConnection conn = new MySqlConnection(connestion_string);

        public static List<Kategori> get_kategorit()
        {
            try
            {
                List<Kategori> list_kategori = new List<Kategori>();

                string querry = "SELECT * FROM kategorit";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Kategori k = new Kategori();
                        k.ID = int.Parse(dr[0].ToString());
                        k.Name = dr[1].ToString();
                        list_kategori.Add(k);
                    }
                    conn.Close();
                    //MessageBox.Show("done");
                    return list_kategori;
                }
                else
                {
                    MessageBox.Show("There are no categories in database.");
                    conn.Close();
                    return list_kategori;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                List<Kategori> list_kategori = null;
                return list_kategori;
            }
        }

        public void update_kategori(int id, string name)
        {
            try
            {
                string querry = "UPDATE kategorit SET Emri = '" + name + "' WHERE ID = " + id + ";";

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(querry, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Category has been updated!");

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conn.Close();
            }
        }

        public void delete_kategori(int id)
        {
            try
            {
                string querry = "DELETE FROM kategorit WHERE ID = " + id + ";";

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(querry, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Category has been deleted!");

                conn.Close();
            }
            catch (MySqlException ex)
            {
                if ((ex.Source == "MySql.Data") && (ex.Number == 1451))
                {
                    MessageBox.Show("This category can't be deleted!");
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }                    
                conn.Close();
            }
        }

        public void add_kategori(string name)
        {
            try
            {
                string querry = "INSERT INTO kategorit(Emri) VALUES('" + name + "');";

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(querry, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Category has been added!");

                conn.Close();
            }
            catch (MySqlException ex)
            {
                if ((ex.Source == "MySql.Data") && (ex.Number == 1062))
                {
                    MessageBox.Show("This category alwready exists.");
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }
    }
}
