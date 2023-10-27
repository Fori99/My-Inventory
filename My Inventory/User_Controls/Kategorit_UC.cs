using My_Inventory.Models;
using My_Inventory.MySql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Inventory.Forms
{
    public partial class Kategorit_UC : UserControl
    {
        public static string kategori_id = "";
        public static string katekori_name = "";

        public Kategorit_UC()
        {
            InitializeComponent();
            show_kategorit();
        }

        private void Kategorit_UC_Load(object sender, EventArgs e)
        {

        }

        public void show_kategorit()
        {
            kategorit_dataGridView.Rows.Clear();
            kategorit_dataGridView.ClearSelection();

            List<Kategori> list_kategori = new List<Kategori>();
            list_kategori = Kategori_Function.get_kategorit();

            if (list_kategori != null)
            {
                foreach (Kategori kategori in list_kategori)
                {
                    kategorit_dataGridView.Rows.Add(kategori.ID, kategori.Name);
                }
            }
        }

        private void kategorit_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in kategorit_dataGridView.SelectedRows)
            {
                kategori_id = (row.Cells[0].Value.ToString());
                katekori_name = (row.Cells[1].Value.ToString());

                Edit_Kategori ek = new Edit_Kategori();
                ek.FormClosed += refresh_category;
                ek.Show();                
            }
        }

        private void add_new_button_Click(object sender, EventArgs e)
        {
            Add_Category ad = new Add_Category();
            ad.FormClosed += refresh_category;
            ad.Show();
        }

        private void refresh_category(object sender, FormClosedEventArgs e)
        {
            show_kategorit();
        }

       
    }
}
