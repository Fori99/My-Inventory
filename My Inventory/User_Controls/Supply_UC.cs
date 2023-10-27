using My_Inventory.Forms.Supply_Forms;
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
    public partial class Supply_UC : UserControl
    {
        public static string product_name = "";

        public Supply_UC()
        {
            InitializeComponent();
            show_category();
        }

        public void show_category()
        {
            List<Kategori> list_kategori = new List<Kategori>();
            list_kategori = Kategori_Function.get_kategorit();

            foreach (Kategori kategori in list_kategori)
            {
                Button b = new Button();
                b.Text = kategori.Name;
                b.Click += new EventHandler(this.category_button_click);
                flowLayoutPanel1.Controls.Add(b);
                //kategorit_dataGridView.Rows.Add(kategori.ID, kategori.Name);
            }
        }

        protected void category_button_click(object sender, EventArgs e)
        {
            Button dynamicButton = (sender as Button);
            show_products(dynamicButton.Text);
        }

        public void show_products(string category_name)
        {
            flowLayoutPanel2.Controls.Clear();

            List<Produkt> list_produktet = new List<Produkt>();
            list_produktet = Produkti_Function.get_produktet_per_kategori(category_name);

            foreach (Produkt produkt in list_produktet)
            {
                Button b = new Button();
                b.Text = produkt.Produkt_Name;
                b.Tag = produkt.Price;
                b.Click += new EventHandler(this.product_button_click);
                flowLayoutPanel2.Controls.Add(b);
            }
        }

        protected void product_button_click(object sender, EventArgs e)
        {
            Button product_Button = (sender as Button);
            product_name = product_Button.Text;

            Supply s = new Supply();
            s.Show();
        }
    }
}
