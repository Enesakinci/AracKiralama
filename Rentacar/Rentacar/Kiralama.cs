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
    public partial class Kiralama : Form
    {
        static string str = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'c:\users\enes\documents\visual studio 2015\Projects\Rentacar\Rentacar\Rentacar.mdf'; Integrated Security = True";
        SqlConnection con = new SqlConnection(str);
        public Kiralama()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void verigoster(string komut)
        {
            SqlDataAdapter da = new SqlDataAdapter(komut, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }
        public  int musteriEkle()
        {
            con.Open();
            SqlCommand Muscmd = new SqlCommand("insert into Musteri(isim,soyisim,yas, tc, ehliyet, telefon, mail) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','"
                + textBox4.Text + "','" + comboBox1.SelectedItem + "','" + textBox5.Text + "','" + textBox6.Text + "');"+ "SELECT SCOPE_IDENTITY();", con);
            int sonID = Convert.ToInt32(Muscmd.ExecuteScalar());
            //cmd.ExecuteNonQuery();

            con.Close();
            return sonID;
        }
        public void kasaEkle(int kiralaId, int musteri_Id)
        {
            con.Open();
            SqlCommand kasa = new SqlCommand("insert into Kasa(Arac_Id,Kirala_Id,Musteri_Id) values('" + (int)dataGridView1.SelectedRows[0].Cells[0].Value + "','"
             + kiralaId + "','" + musteri_Id + "')", con);
            kasa.ExecuteNonQuery();
            con.Close();

        }
        public int aracKirala(int SonKayitID,int tutar)
        {
            con.Open();
            SqlCommand arackira = new SqlCommand("insert into kirala(Arac_Id, Musteri_Id, fiyat, alis_tarih,veris_tarih) values('" + (int)dataGridView1.SelectedRows[0].Cells[0].Value + "','"
                + SonKayitID + "','" + tutar + "','" + dateTimePicker1.Value.ToShortDateString() + "','" + dateTimePicker2.Value.ToShortDateString() + "')"+ "SELECT SCOPE_IDENTITY();", con);
            int sonID = Convert.ToInt32(arackira.ExecuteScalar());
            //arackira.ExecuteNonQuery();
            con.Close();
            return sonID;

        }
        public void guncelle() {
            con.Open();
            SqlCommand aracDurum = new SqlCommand("Update Arac set durum=1 where Arac_Id=" + (int)dataGridView1.SelectedRows[0].Cells[0].Value , con);
            aracDurum.ExecuteNonQuery();
            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sonID= musteriEkle();
            TimeSpan GunFarki = dateTimePicker2.Value.Subtract(dateTimePicker1.Value);
            int fark = GunFarki.Days+1;//Gün farkı hesaplandı
            int fiyat = (int)dataGridView1.SelectedRows[0].Cells[7].Value;//Fiyat alındı
            int tutar = fiyat * fark;
            int kiraId= aracKirala(sonID, tutar);

            kasaEkle(kiraId, sonID);
            guncelle();

            MessageBox.Show("Araç kiralandı");
            verigoster("select * from arac where durum=0");

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.SelectedItem = null;
        }

        private void Kiralama_Load(object sender, EventArgs e)
        {
            verigoster("select * from arac where durum=0");
        }
    }
}
