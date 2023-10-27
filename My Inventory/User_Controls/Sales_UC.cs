using My_Inventory.Forms;
using My_Inventory.Models;
using My_Inventory.MySql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Inventory.User_Controls
{
    public partial class Sales_UC : UserControl
    {
        public Sales_UC()
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

            int row_nr = produktet_dataGridView.Rows.Count;

            bool found = false;

            if (row_nr > 1)
            {
                for (int i = 0; i < row_nr - 1; i++)
                {
                    String cellText = produktet_dataGridView.Rows[i].Cells[0].Value.ToString();

                    if (product_Button.Text == cellText)
                    {
                        int quantity = int.Parse(produktet_dataGridView.Rows[i].Cells[2].Value.ToString());
                        produktet_dataGridView.Rows[i].Cells[2].Value = quantity + 1;
                        found = true;
                        break;
                    }
                }

                if (found == false)
                {
                    produktet_dataGridView.Rows.Add(product_Button.Text, product_Button.Tag, 1);
                }
            }
            else
            {
                produktet_dataGridView.Rows.Add(product_Button.Text, product_Button.Tag, 1);
            }
        }

        private void produktet_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in produktet_dataGridView.SelectedRows)
            {
                int quantity = int.Parse(row.Cells[2].Value.ToString());

                if (quantity == 1)
                {
                    produktet_dataGridView.Rows.RemoveAt(produktet_dataGridView.SelectedRows[0].Index);
                }
                else
                {
                    row.Cells[2].Value = quantity - 1;
                }
            }
        }

        private void add_new_button_Click(object sender, EventArgs e)
        {
            int row_nr = produktet_dataGridView.Rows.Count;

            if (row_nr > 1)
            {
                int sale_ret = 0;
                bool stock_ret = false;

                for (int i = 0; i < row_nr - 1; i++)
                {
                    String product_name = produktet_dataGridView.Rows[i].Cells[0].Value.ToString();
                    String sasia = produktet_dataGridView.Rows[i].Cells[2].Value.ToString();

                    stock_ret = Produkti_Function.check_stock(product_name, sasia);

                    if (stock_ret == false)
                    {
                        break;
                    }
                }

                if (stock_ret == true)
                {
                    for (int i = 0; i < row_nr - 1; i++)
                    {
                        String product_name = produktet_dataGridView.Rows[i].Cells[0].Value.ToString();
                        String sasia = produktet_dataGridView.Rows[i].Cells[2].Value.ToString();

                        sale_ret = Produkti_Function.sell_produkte(product_name, sasia);

                        if (sale_ret == -1)
                        {
                            break;
                        }
                    }

                    if (sale_ret == 1)
                    {
                        MessageBox.Show("Sale was successfull!");
                        produktet_dataGridView.Rows.Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("No products selected!");
            }
            }
    }
}
