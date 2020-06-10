using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ColeBrightSchedulingAppC969
{
    public partial class ModifyAppointment : Form
    { 
        public ModifyAppointment()
        {
            InitializeComponent();
            cbType.Items.AddRange(new object[] {"Scrum",
                        "Presentation",
                        "Initial",
                        });
            time1.Format = DateTimePickerFormat.Time;
            time1.ShowUpDown = true;
            time2.Format = DateTimePickerFormat.Time;
            time2.ShowUpDown = true;
            MySqlConnection con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None;convert zero datetime=True;");
            String sqlString = "SELECT customerId, customerName FROM customer WHERE customerId = " + CurrentID.SelectedID +";";
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "customer");
            customerLabel.Text = ds.Tables["customer"].Rows[0]["customerName"].ToString();

            //cbCustomer.DisplayMember = "customerName";
            //cbCustomer.ValueMember = "customerId";
            //cbCustomer.DataSource = ds.Tables["customer"];
            try
            {
                con.Open();
                String sqlString1 = "SELECT * FROM appointment WHERE appointment.appointmentId =" + 
                CurrentID.ApptID + 
                " AND customerId =" +
                CurrentID.SelectedID +
                    ";";
                MySqlCommand cmd1 = new MySqlCommand(sqlString1, con);
                time1.CustomFormat = "hh:mm tt";
                time2.CustomFormat = "hh:mm tt";
                using (MySqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TimeZoneInfo currenttimezone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
                        DateTime localtime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, currenttimezone); 
                        dateTimePicker1.Value = (DateTime)(reader["start"]);
                        time1.Value = TimeZoneInfo.ConvertTimeFromUtc((DateTime)(reader["start"]), currenttimezone);
                        time2.Value = TimeZoneInfo.ConvertTimeFromUtc((DateTime)(reader["end"]), currenttimezone);
                        cbType.Text = (reader["type"].ToString());
                        //cbCustomer.Text = (reader["customerName"].ToString());
                    }
                    //cbCustomer.SelectedIndex = CurrentID.SelectedID -1;
                }
            }
            finally
            {
                con.Close();
            }
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
                        string checkquery = "SELECT * FROM appointment WHERE DATE(start) = DATE(@start) AND appointmentId <> @apptId;";//select from today
                        //is endProposed inside existing, startProposed inside existing, or both inside existing
                        MySqlCommand cmd = new MySqlCommand(checkquery, mysqa);
                        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                        cmd.Parameters.AddWithValue("@start", start1);
                        cmd.Parameters.AddWithValue("@end", end2);
                        cmd.Parameters.AddWithValue("@apptId", CurrentID.ApptID);
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
                        string query = "UPDATE appointment SET customerId = @customerId, " +
                            "userId = @userId, type = @type, start =  @start, end = @end WHERE appointmentId =" + CurrentID.ApptID +";";
                        MySqlCommand cmd1 = new MySqlCommand(query, mysqa);
                        //cmd1.Connection.Open();
                        cmd1.Parameters.AddWithValue("@customerId", CurrentID.SelectedID);
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
