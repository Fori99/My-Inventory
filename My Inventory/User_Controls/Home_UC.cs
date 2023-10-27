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
    public partial class Home_UC : UserControl
    {
        public Home_UC()
        {
            InitializeComponent();
            show_furnizimet();
            show_shitjet();
        }

        private void Home_UC_Load(object sender, EventArgs e)
        {

        }

        public void show_furnizimet()
        {
            furnizimet_dataGridView.Rows.Clear();
            furnizimet_dataGridView.ClearSelection();

            List<Furnizimet> list = new List<Furnizimet>();
            list = Produkti_Function.get_furnizimet();

            if (list != null)
            {
                int furnizim_total = 0;
                foreach (Furnizimet furnizim in list)
                {
                    furnizimet_dataGridView.Rows.Add(furnizim.Produkt_Name, furnizim.Cmimi, furnizim.Sasia, furnizim.Total, furnizim.Data);
                    furnizim_total = furnizim_total + int.Parse(furnizim.Total);
                }

                groupBox1.Text = "Supplies - " + furnizim_total.ToString() + " ALL";
            }
        }

        public void show_shitjet()
        {
            shitjet_dataGridView.Rows.Clear();
            shitjet_dataGridView.ClearSelection();

            List<Shitje> list = new List<Shitje>();
            list = Produkti_Function.get_shitjet();

            if (list != null)
            {
                int shitje_totale = 0;
                foreach (Shitje shitje in list)
                {
                    shitjet_dataGridView.Rows.Add(shitje.Produkt_Name, shitje.Cmimi, shitje.Sasia, shitje.Total, shitje.Data);
                    shitje_totale = shitje_totale + int.Parse(shitje.Total);
                }

                groupBox2.Text = "Sales - " + shitje_totale.ToString() + " ALL";
            }
        }
    }
}
