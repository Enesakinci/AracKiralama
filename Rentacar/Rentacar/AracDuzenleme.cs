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
    public partial class AracDuzenleme : Form
    {
        static string str = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'c:\users\enes\documents\visual studio 2015\Projects\Rentacar\Rentacar\Rentacar.mdf'; Integrated Security = True";
        SqlConnection con = new SqlConnection(str);
        public AracDuzenleme()
        {
            InitializeComponent();
        }

        private void kydt_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Arac(plaka,marka,model, uretim_yili, yakit_tipi, sanzuman, gunluk_fiyat,durum) values ('"+ textBox1.Text+"','" + textBox2.Text + "','" + textBox3.Text + "','"
                + textBox4.Text + "','" + comboBox1.SelectedItem + "','" + comboBox2.SelectedItem + "','"
                + Convert.ToInt32(textBox5.Text) + "','"+Convert.ToInt32(textBox6.Text)+"')",con);

            cmd.ExecuteNonQuery();
            verigoster("select * from arac");
            con.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;


        }
        public void verigoster(string komut)
        {
            SqlDataAdapter da = new SqlDataAdapter(komut, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void AracDuzenleme_Load(object sender, EventArgs e)
        {
            verigoster("Select * from Arac");
            // TODO: This line of code loads data into the 'rentacarDataSet.Arac' table. You can move, or remove it, as needed.
            this.aracTableAdapter.Fill(this.rentacarDataSet.Arac);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Arac Set plaka=@plaka, marka=@marka, model=@model,uretim_yili=@uretim_yili,yakit_tipi=@yakit_tipi, sanzuman=@sanzuman,gunluk_fiyat=@gunluk_fiyat,durum=@durum Where Arac_Id="+id, con);
            cmd.Parameters.AddWithValue("@plaka", textBox1.Text);
            cmd.Parameters.AddWithValue("@marka", textBox2.Text);
            cmd.Parameters.AddWithValue("@model", textBox3.Text);
            cmd.Parameters.AddWithValue("@uretim_yili", textBox4.Text);
            cmd.Parameters.AddWithValue("@gunluk_fiyat", Convert.ToInt32(textBox5.Text));
            cmd.Parameters.AddWithValue("@yakit_tipi", comboBox1.SelectedItem);
            cmd.Parameters.AddWithValue("@sanzuman", comboBox2.SelectedItem);
            cmd.Parameters.AddWithValue("@durum", textBox6.Text);
            cmd.ExecuteNonQuery();
            verigoster("select * from arac");
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SqlDataAdapter da;
            DataSet ds;
            SqlCommand komut;
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)  //Seçili Satırları Silme
            {
                int numara = Convert.ToInt32(drow.Cells[0].Value);
                string sql = "DELETE FROM Arac WHERE Arac_Id=@Arac_Id";
                komut = new SqlCommand(sql, con);
                komut.Parameters.AddWithValue("@Arac_Id", numara);
                con.Open();
                komut.ExecuteNonQuery();
                verigoster("select * from arac");
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            RentacarDataSet.AracRow r = rentacarDataSet.Arac.FindByArac_Id(id);
            textBox1.Text = r.plaka;
            textBox2.Text = r.marka;
            textBox3.Text = r.model;
            textBox4.Text = r.uretim_yili;
            textBox5.Text = r.gunluk_fiyat.ToString();
            comboBox1.SelectedItem = r.yakit_tipi;
            comboBox2.SelectedItem = r.sanzuman;
            textBox6.Text = r.durum.ToString();
            aracTableAdapter.Update(r);

        }
    }
}
