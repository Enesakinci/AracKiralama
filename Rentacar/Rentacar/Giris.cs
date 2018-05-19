using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Rentacar
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'c:\users\enes\documents\visual studio 2015\Projects\Rentacar\Rentacar\Rentacar.mdf'; Integrated Security = True";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand komut = new SqlCommand("select * from yonetici where tc='" + textBox1.Text + "' and sifre='" + textBox2.Text + "'", con);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Menu frm = new Rentacar.Menu();
                this.Hide();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("kullanıcı adı ve şifre yanlış !");
            }
        }
    }
}
