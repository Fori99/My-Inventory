using My_Inventory.Forms;
using My_Inventory.Models;
using My_Inventory.MySql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Inventory.User_Controls
{
    public partial class Produktet_UC : UserControl
    {
        public static string product_id = "";
        public static string product_name = "";
        public static string product_stock = "";
        public static string product_price = "";
        public static string kategori_name = "";

        public Produktet_UC()
        {
            InitializeComponent();
            show_produktet();
        }

        public void show_produktet()
        {
            produktet_dataGridView.Rows.Clear();
            produktet_dataGridView.ClearSelection();

            List<Produkt> list_produkte = new List<Produkt>();
            list_produkte = Produkti_Function.get_produktet(); ;

            if (list_produkte != null)
            {
                foreach (Produkt produkt in list_produkte)
                {
                    produktet_dataGridView.Rows.Add(produkt.ID, produkt.Produkt_Name, produkt.Price, produkt.Stock, produkt.Kategori_Name);
                }
            }
        }

        private void refresh_produktet(object sender, FormClosedEventArgs e)
        {
            show_produktet();
        }

        private void produktet_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in produktet_dataGridView.SelectedRows)
            {
                product_id = (row.Cells[0].Value.ToString());
                product_name = (row.Cells[1].Value.ToString());
                product_price = (row.Cells[2].Value.ToString());
                product_stock = (row.Cells[3].Value.ToString());
                kategori_name = (row.Cells[4].Value.ToString());

                Edit_Product ep = new Edit_Product();
                ep.FormClosed += refresh_produktet;
                ep.Show();
            }
        }

        private void add_new_button_Click(object sender, EventArgs e)
        {
            Add_Product ap = new Add_Product();
            ap.FormClosed += refresh_produktet;
            ap.Show();
        }
    }
}
