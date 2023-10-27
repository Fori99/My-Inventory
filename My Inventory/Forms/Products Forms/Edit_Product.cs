using My_Inventory.Models;
using My_Inventory.MySql;
using My_Inventory.User_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Inventory.Forms
{
    public partial class Edit_Product : Form
    {
        public Edit_Product()
        {
            InitializeComponent();
            show_kategorit();

            id_textBox.Text = Produktet_UC.product_id;
            product_name_textBox.Text = Produktet_UC.product_name;
            price_textBox.Text = Produktet_UC.product_price;
            stock_textBox.Text = Produktet_UC.product_stock;
            kategori_comboBox.SelectedIndex = kategori_comboBox.Items.IndexOf(Produktet_UC.kategori_name);
        }

        public void show_kategorit()
        {
            List<Kategori> list_kategori = new List<Kategori>();
            list_kategori = Kategori_Function.get_kategorit();

            if (list_kategori != null)
            {
                foreach (Kategori kategori in list_kategori)
                {
                    kategori_comboBox.Items.Add(kategori.Name);
                    //kategorit_dataGridView.Rows.Add(kategori.ID, kategori.Name);
                }
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            Produkti_Function pf = new Produkti_Function();
            pf.delete_produkt(int.Parse(id_textBox.Text));
            this.Close();
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            Produkti_Function pf = new Produkti_Function();
            pf.update_produkt(int.Parse(id_textBox.Text), product_name_textBox.Text, price_textBox.Text, kategori_comboBox.GetItemText(kategori_comboBox.SelectedItem));
            this.Close();
        }
    }
}
