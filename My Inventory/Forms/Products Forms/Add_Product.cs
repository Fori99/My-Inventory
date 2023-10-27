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

namespace My_Inventory.Forms
{
    public partial class Add_Product : Form
    {
        public Add_Product()
        {
            InitializeComponent();
            show_kategorit();
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

        private void add_button_Click(object sender, EventArgs e)
        {
            Produkti_Function pf = new Produkti_Function();
            pf.add_produkt(product_name_textBox.Text, price_textBox.Text, kategori_comboBox.GetItemText(kategori_comboBox.SelectedItem));
            this.Close();
        }
    }
}
