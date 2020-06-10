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
    public partial class Add_Appointment : Form
    {
        public Add_Appointment()
        {
            InitializeComponent();
            cbType.Items.AddRange(new object[] {"Scrum",
                        "Presentation",
                        "Initial",
                        });
            MySqlConnection con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None;convert zero datetime=True;");

            con.Open();
            
            String sqlString = "SELECT customerId, customerName FROM customer";
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "customer");
            cbCustomer.DisplayMember = "customerName";
            cbCustomer.ValueMember = "customerId";
            cbCustomer.DataSource = ds.Tables["customer"];
            cbCustomer.SelectedIndex = 0;
            time1.Format = DateTimePickerFormat.Time;
            time1.ShowUpDown = true;
            time2.Format = DateTimePickerFormat.Time;
            time2.ShowUpDown = true;
        }

        private void BtnSaveProduct_Click(object sender, EventArgs e)
        {
            DateTime start1 = dateTimePicker1.Value;
            DateTime start2 = time1.Value;
            DateTime dtCombined = new DateTime(start1.Year, start1.Month, start1.Day, start2.Hour, start2.Minute, start2.Second);
            DateTime end1 = dateTimePicker1.Value;
            DateTime end2 = time2.Value;
            DateTime dtCombined2 = new DateTime(end1.Year, end1.Month, end1.Day, end2.Hour, end2.Minute, end2.Second);
            
            if (start1.DayOfWeek != DayOfWeek.Saturday && start1.DayOfWeek != DayOfWeek.Sunday)
            {
                if (dtCombined.TimeOfDay.Hours >= 9 && dtCombined.TimeOfDay.Hours < 17 && dtCombined2.TimeOfDay.Hours >= 9 && dtCombined2.TimeOfDay.Hours < 17)
                {
                    using (MySqlConnection mysqa = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None;convert zero datetime=True;"))
                    {
                        string checkquery = "SELECT * FROM appointment WHERE DATE(start) = DATE(@start);";//select from today
                       
                        MySqlCommand cmd = new MySqlCommand(checkquery, mysqa);
                        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                        cmd.Parameters.AddWithValue("@start", start1);
                        cmd.Parameters.AddWithValue("@end", end2);

                        mysqa.Open();
                        cmd.ExecuteNonQuery();
                        DataTable ds = new DataTable();
                        adp.Fill(ds);
                        foreach (DataRow row in ds.Rows)
                        {
                            TimeZoneInfo currenttimezone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
                            DateTime localtime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, currenttimezone);
                            if (dtCombined < TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("end"), currenttimezone) && dtCombined2 > TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("start"), currenttimezone))
                            {
                                MessageBox.Show("Those times conflict with another appointment time!");
                                return;
                            }
                        }
                        string query = "INSERT into appointment(customerId, userId, type, start, end) "
                        + "VALUES(@customerId, @userId, @type, @start, @end);";
                        MySqlCommand cmd1 = new MySqlCommand(query, mysqa);
                        //cmd1.Connection.Open();
                        cmd1.Parameters.AddWithValue("@customerId", cbCustomer.SelectedValue);
                        cmd1.Parameters.AddWithValue("@userId", CurrentID.CurrentUserId);
                        cmd1.Parameters.AddWithValue("@type", cbType.SelectedItem);
                        cmd1.Parameters.AddWithValue("@start", dtCombined.ToUniversalTime());
                        cmd1.Parameters.AddWithValue("@end", dtCombined2.ToUniversalTime());
                        cmd1.Parameters.AddWithValue("@createdate", DateTime.Today);
                        cmd1.Parameters.AddWithValue("@createby", CurrentID.CurrentUser);
                        cmd1.Parameters.AddWithValue("@lastUpdateBy", CurrentID.CurrentUser);
                        cmd1.ExecuteNonQuery();
                    }
                    this.Hide();
                    Main mn = new Main();
                    mn.Show();
                }
                else
                {
                    MessageBox.Show("That time is outside business hours!");
                }
            }
            else
            {
                MessageBox.Show("Cannot set up appointment outside the business week, M-F.");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main mn = new Main();
            mn.Show();
        }
    }
}
