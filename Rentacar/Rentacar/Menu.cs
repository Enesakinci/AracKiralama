using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rentacar
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AracDuzenleme frm = new AracDuzenleme();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KullaniciDuzenleme frm = new KullaniciDuzenleme();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Giris frm = new Giris();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kiralama frm = new Kiralama();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Kasa frm = new Kasa();
            frm.ShowDialog();    
        }
    }
}
