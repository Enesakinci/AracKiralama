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
    public partial class Kasa : Form
    {
        static string str = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'c:\users\enes\documents\visual studio 2015\Projects\Rentacar\Rentacar\Rentacar.mdf'; Integrated Security = True";
        SqlConnection con = new SqlConnection(str);
        public Kasa()
        {
            InitializeComponent();
        }
        public void verigoster(string komut)
        {
            SqlDataAdapter da = new SqlDataAdapter(komut, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void Kasa_Load(object sender, EventArgs e)
        {
            verigoster("SELECT Kasa.Kasa_Id, Arac.marka, Arac.model, Kirala.fiyat FROM Kasa INNER JOIN Arac ON Kasa.Arac_Id = Arac.Arac_Id INNER JOIN Kirala ON Kasa.Kirala_Id = Kirala.Kirala_Id");
        }
    }
}
