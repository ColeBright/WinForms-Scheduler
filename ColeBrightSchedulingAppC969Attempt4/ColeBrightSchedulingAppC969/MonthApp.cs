using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColeBrightSchedulingAppC969
{
    public partial class MonthApp : Form
    {
        public MonthApp()
        {
            InitializeComponent();
        }

        private void MonthApp_Load(object sender, EventArgs e)
        {
            DateTime monthStart = new DateTime(CurrentID.SelectedDate.Year, CurrentID.SelectedDate.Month, 1);
            DateTime monthEnd = monthStart.AddMonths(1);
            string Start = monthStart.ToString("yyyy-MM-dd");
            string End = monthEnd.ToString("yyyy-MM-dd");
            //DateTime today = DateTime.Today;
            //DateTime monthStart = new DateTime(today.Year, today.Month, 1);
            //DateTime monthEnd = monthStart.AddMonths(1);
            //string Start = monthStart.ToString("yyyy-MM-dd");
            //string End = monthEnd.ToString("yyyy-MM-dd");
            String sqlString1 = "SELECT COUNT(DISTINCT type) AS Count FROM appointment WHERE" +
                " (start BETWEEN '" + Start + "' AND '" + End + "');";
            MySqlConnection con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None;convert zero datetime=True");
            MySqlCommand cmd1 = new MySqlCommand(sqlString1, con);
            con.Open();
            using (MySqlDataReader reader = cmd1.ExecuteReader())
            {
                while (reader.Read())
                {
                    textBox1.Text = reader["Count"].ToString();  
                }
            }
            con.Close();
        }
    }
}
