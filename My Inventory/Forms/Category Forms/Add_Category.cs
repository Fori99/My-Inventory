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
    public partial class Add_Category : Form
    {
        public Add_Category()
        {
            InitializeComponent();
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Kategori_Function kf = new Kategori_Function();
            kf.add_kategori(name_textBox.Text);
            this.Close();
        }
    }
}
