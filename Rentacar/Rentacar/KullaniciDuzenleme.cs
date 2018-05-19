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
    public partial class KullaniciDuzenleme : Form
    {
        public KullaniciDuzenleme()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'c:\users\enes\documents\visual studio 2015\Projects\Rentacar\Rentacar\Rentacar.mdf'; Integrated Security = True";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO Yonetici(tc,sifre) VALUES('" + textBox1.Text + "','" + textBox2.Text+ "')", con);

            if (komut.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Kullanıcı başarıyla eklendi.");
            }
            else
            {
                MessageBox.Show("Kullanici eklenemedi..!");
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'c:\users\enes\documents\visual studio 2015\Projects\Rentacar\Rentacar\Rentacar.mdf'; Integrated Security = True";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            string kayit = "update yonetici set sifre=@sifre where tc=@tc";
            // müşteriler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
            SqlCommand komut = new SqlCommand(kayit, con);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            komut.Parameters.AddWithValue("@tc", textBox1.Text);
            komut.Parameters.AddWithValue("@sifre", textBox2.Text);
            //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
            if (komut.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Kullanıcı başarıyla güncellendi.");
            }
            else
            {
                MessageBox.Show("Kullanici bulunamadı..!");
            }
            //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'c:\users\enes\documents\visual studio 2015\Projects\Rentacar\Rentacar\Rentacar.mdf'; Integrated Security = True";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            string secmeSorgusu = "SELECT * from yonetici where tc=@tc";
            //musterino parametresine bağlı olarak müşteri bilgilerini çeken sql kodu
            SqlCommand secmeKomutu = new SqlCommand(secmeSorgusu, con);
            secmeKomutu.Parameters.AddWithValue("@tc", textBox1.Text);
            //musterino parametremize textbox'dan girilen değeri aktarıyoruz.
            SqlDataAdapter da = new SqlDataAdapter(secmeKomutu);
            SqlDataReader dr = secmeKomutu.ExecuteReader();
            //DataReader ile müşteri verilerini veritabanından belleğe aktardık.
            if (dr.Read()) //Datareader herhangi bir okuma yapabiliyorsa aşağıdaki kodlar çalışır.
            {
                string isim = dr["tc"].ToString();
                dr.Close();
                //Datareader ile okunan müşteri ad ve soyadını isim değişkenine atadım.
                //Datareader açık olduğu sürece başka bir sorgu çalıştıramayacağımız için dr nesnesini kapatıyoruz.
                DialogResult durum = MessageBox.Show(isim + " kaydını silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
                //Kullanıcıya silme onayı penceresi açıp, verdiği cevabı durum değişkenine aktardık.
                if (DialogResult.Yes == durum) // Eğer kullanıcı Evet seçeneğini seçmişse, veritabanından kaydı silecek kodlar çalışır.
                {
                    string silmeSorgusu = "DELETE from yonetici where tc=@tc";
                    //musterino parametresine bağlı olarak müşteri kaydını silen sql sorgusu
                    SqlCommand silKomutu = new SqlCommand(silmeSorgusu,con);
                    silKomutu.Parameters.AddWithValue("@tc", textBox1.Text);
                    silKomutu.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Silindi...");
                    //Silme işlemini gerçekleştirdikten sonra kullanıcıya mesaj verdik.
                }
            }
            else
                MessageBox.Show("Yönetici Bulunamadı.");
            con.Close();
        }
    }
}
