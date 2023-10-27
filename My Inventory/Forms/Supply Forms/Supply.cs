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

namespace My_Inventory.Forms.Supply_Forms
{
    public partial class Supply : Form
    {
        public Supply()
        {
            InitializeComponent();
            product_name_textBox.Text = Supply_UC.product_name;
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Produkti_Function.supply_produkte(product_name_textBox.Text, quantity_textBox.Text, price_textBox.Text);
            this.Close();
        }
    }
}
