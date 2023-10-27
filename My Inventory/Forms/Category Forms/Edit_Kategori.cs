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
    public partial class Edit_Kategori : Form
    {
        public Edit_Kategori()
        {
            InitializeComponent();
            id_textBox.Text = Kategorit_UC.kategori_id;
            name_textBox.Text = Kategorit_UC.katekori_name;
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            Kategori_Function kf = new Kategori_Function();
            kf.update_kategori(int.Parse(id_textBox.Text), name_textBox.Text);
            this.Close();
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            Kategori_Function kf = new Kategori_Function();
            kf.delete_kategori(int.Parse(id_textBox.Text));
            this.Close();
        }
    }
}
