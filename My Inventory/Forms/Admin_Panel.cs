using My_Inventory.Forms;
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

namespace My_Inventory
{
    public partial class Admin_Panel : Form
    {
        public Admin_Panel()
        {
            InitializeComponent();

            var myControl = new Home_UC();
            main_panel.Controls.Add(myControl);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void kategorit_button_Click(object sender, EventArgs e)
        {
            var myControl = new Kategorit_UC();
            main_panel.Controls.Clear();
            main_panel.Controls.Add(myControl);
        }

        private void products_buttons_Click(object sender, EventArgs e)
        {
            var myControl = new Produktet_UC();
            main_panel.Controls.Clear();
            main_panel.Controls.Add(myControl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void home_button_Click(object sender, EventArgs e)
        {
            var myControl = new Home_UC();
            main_panel.Controls.Clear();
            main_panel.Controls.Add(myControl);
        }

        private void sell_button_Click(object sender, EventArgs e)
        {
            var myControl = new Sales_UC();
            main_panel.Controls.Clear();
            main_panel.Controls.Add(myControl);
        }

        private void supply_button_Click(object sender, EventArgs e)
        {
            var myControl = new Supply_UC();
            main_panel.Controls.Clear();
            main_panel.Controls.Add(myControl);
        }
    }
}
