using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColeBrightSchedulingAppC969
{
    public partial class Main : Form
    {
        int indexSelectedCust = -1;
        static MySqlConnection con;
        public Main()
        {
            InitializeComponent();

            CurrentID.SelectedDate = monthCalendar1.SelectionRange.Start;

            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.DefaultCellStyle.SelectionBackColor = dgvCustomers.DefaultCellStyle.BackColor;
            dgvCustomers.DefaultCellStyle.SelectionForeColor = dgvCustomers.DefaultCellStyle.ForeColor;
            dgvCustomers.RowHeadersVisible = false;

            dgvAppts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppts.DefaultCellStyle.SelectionBackColor = dgvAppts.DefaultCellStyle.BackColor;
            dgvAppts.DefaultCellStyle.SelectionForeColor = dgvAppts.DefaultCellStyle.ForeColor;
            dgvAppts.RowHeadersVisible = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            paintCustomer();
            paintAppointment();

        }
        private void dgvAppts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void OnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnAddCust_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddCustomer addcus = new AddCustomer();
            addcus.Show();
        }

        private void BtnModifyCust_Click(object sender, EventArgs e)
        {
           
            Hide();
            ModifyCustomer modcus = new ModifyCustomer();
            modcus.Show();
        }

        private void BtnDeleteCust_Click(object sender, EventArgs e)
        {
            MySqlConnection deletecustcon = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None;convert zero datetime=True;");
            string checkassociatedappointment = "SELECT * FROM appointment WHERE customerId ="+ CurrentID.SelectedID + ";";
            MySqlCommand selcom = new MySqlCommand(checkassociatedappointment, deletecustcon);
            MySqlDataAdapter mysda = new MySqlDataAdapter(selcom);
            DataTable dattab = new DataTable();
            mysda.Fill(dattab);

            if (dattab.Rows.Count >= 1)
            {
                MessageBox.Show("Cannot delete customer with appointments.");
            }
            else
                {
                    string deletecustquery = "DELETE FROM customer WHERE customerId = "+ CurrentID.SelectedID +";";
                    using (MySqlCommand delcom = new MySqlCommand(deletecustquery, deletecustcon))
                    {
                        try
                        {
                            deletecustcon.Open();
                            delcom.ExecuteNonQuery();
                            MessageBox.Show("Customer deleted.");   
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error " + ex.Message);
                        }

                    }
                }
            paintCustomer();
        }

        private void DgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            indexSelectedCust = dgvCustomers.CurrentCell.RowIndex;
            CurrentID.SelectedID = (int)dgvCustomers.Rows[indexSelectedCust].Cells[0].Value;
            dgvCustomers.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Cyan;

            con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None;convert zero datetime=True");
            con.Open();
            String sqlString2 = "SELECT * FROM appointment WHERE customerId = " + CurrentID.SelectedID + ";";
            MySqlCommand cmd2 = new MySqlCommand(sqlString2, con);
            MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
            DataTable ds2 = new DataTable();
            adp2.Fill(ds2);
            dgvAppts.DataSource = ds2;
            dgvAppts.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            cmd2.ExecuteNonQuery();
            con.Close();

            //check appointment w/in 15 min
            TimeZoneInfo currenttimezone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
            DateTime localtime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, currenttimezone);

            foreach (DataRow row in ds2.Rows)
            {
                DateTime srowtime = TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("start"), currenttimezone);
                row[9] = srowtime;
                DateTime erowtime = TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("end"), currenttimezone);
                row[10] = erowtime;
                //DateTime 
                if (srowtime.Date == localtime.Date)
                {
                    int min = (srowtime - localtime).Minutes;
                    if (min <= 15 && min > 0)
                    {
                        MessageBox.Show("You have an appointment within 15 minutes.");
                    }
                }
            }




        }

        private void BtnAddAppt_Click(object sender, EventArgs e)
        {
            //This delegate exhibits using a lambda to take a one-off method with no parameters, slightly 
            //reducing the time to call up the Add Appointment form
            Action action = () =>
            {
                this.Hide();
                Add_Appointment addapt = new Add_Appointment();
                addapt.Show();
            };
            action.Invoke();
        }

        private void BtnDeleteAppt_Click(object sender, EventArgs e)
        {
            MySqlConnection deleteapptcon = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None");
            string deleteapptquery = "DELETE FROM appointment WHERE appointmentId = " + CurrentID.ApptID + ";";
            using (MySqlCommand delcom = new MySqlCommand(deleteapptquery, deleteapptcon))
            {
                try
                {
                    deleteapptcon.Open();
                    delcom.ExecuteNonQuery();
                    MessageBox.Show("Appointment deleted.");
                }
                catch (Exception ex)
                {
                    throw new Exception("Error " + ex.Message);
                }
            }
            paintAppointment();
        }

        private void DgvAppts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAppts.Rows.Count == 0)
            {
                MessageBox.Show("There's nothing here.");
            }
            else
            {
                indexSelectedCust = dgvAppts.CurrentCell.RowIndex;
                CurrentID.ApptID = (int)dgvAppts.Rows[indexSelectedCust].Cells[0].Value;
                CurrentID.SelectedID = (int)dgvAppts.Rows[indexSelectedCust].Cells[1].Value;
                dgvAppts.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Cyan;
            }
        }

        private void paintCustomer()
        {
            con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None");
            con.Open();
            String sqlString = "SELECT * FROM customer";
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            dgvCustomers.DataSource = ds;
            dgvCustomers.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void paintAppointment()
        {
            con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None;convert zero datetime=True");
            con.Open();
            String sqlString2 = "SELECT * FROM appointment  ";
            MySqlCommand cmd2 = new MySqlCommand(sqlString2, con);
            MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
            DataTable ds2 = new DataTable();
            adp2.Fill(ds2);
            dgvAppts.DataSource = ds2;
            dgvAppts.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            cmd2.ExecuteNonQuery();
            con.Close();

            //check appointment w/in 15 min
            TimeZoneInfo currenttimezone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
            DateTime localtime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, currenttimezone);

            foreach (DataRow row in ds2.Rows)
            {
                DateTime srowtime = TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("start"), currenttimezone);
                row[9] = srowtime;
                DateTime erowtime = TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("end"), currenttimezone);
                row[10] = erowtime;
                //DateTime 
                if (srowtime.Date == localtime.Date)
                {
                    int min = (srowtime - localtime).Minutes;
                    if (min <= 15 && min > 0)
                    {
                        MessageBox.Show("You have an appointment within 15 minutes.");
                    }
                }
            }
            

        }

        private void BtnModifyAppt_Click(object sender, EventArgs e)
        {
            if (indexSelectedCust >= 0)
            {
                this.Hide();
                ModifyAppointment modappt = new ModifyAppointment();
                modappt.Show();
            }
        }

        private void RbAll_CheckedChanged(object sender, EventArgs e)
        {
            paintAppointment();
        }

        private void RbWeek_CheckedChanged(object sender, EventArgs e)
        {
            if(rbWeek.Checked == true)
            if (indexSelectedCust >= 0)
            {
                CurrentID.SelectedDate = monthCalendar1.SelectionRange.Start;
                DateTime weekFromNow = CurrentID.SelectedDate.AddDays(7);
                string week = weekFromNow.ToString("yyyy-MM-dd");
                string today = CurrentID.SelectedDate.ToString("yyyy-MM-dd");
                con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None;convert zero datetime=True;");
                con.Open();
                String sqlString2 = "SELECT * FROM appointment WHERE customerId = " + CurrentID.SelectedID +
                    " AND start BETWEEN '" + today + "' AND '" + week + "';";
                MySqlCommand cmd2 = new MySqlCommand(sqlString2, con);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                DataTable ds2 = new DataTable();
                adp2.Fill(ds2);
                dgvAppts.DataSource = ds2;
                dgvAppts.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                cmd2.ExecuteNonQuery();
                con.Close();

                //check appointment w/in 15 min
                TimeZoneInfo currenttimezone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
                DateTime localtime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, currenttimezone);

                foreach (DataRow row in ds2.Rows)
                {
                        DateTime srowtime = TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("start"), currenttimezone);
                        row[9] = srowtime;
                        DateTime erowtime = TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("end"), currenttimezone);
                        row[10] = erowtime;
                        //DateTime 
                        if (srowtime.Date == localtime.Date)
                        {
                            int min = (srowtime - localtime).Minutes;
                            if (min <= 15 && min > 0)
                            {
                                MessageBox.Show("You have an appointment within 15 minutes.");
                            }
                        }
                    }
            }
            else
            {
                MessageBox.Show("Please select a customer.");
            }
        }

        private void RbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if(rbMonth.Checked == true)
            if (indexSelectedCust >= 0)
            {
                CurrentID.SelectedDate = monthCalendar1.SelectionRange.Start;
                //DateTime today = DateTime.Today;
                DateTime monthStart = new DateTime(CurrentID.SelectedDate.Year, CurrentID.SelectedDate.Month, 1);
                DateTime monthEnd = monthStart.AddMonths(1);
                string Start = monthStart.ToString("yyyy-MM-dd");
                string End = monthEnd.ToString("yyyy-MM-dd");
                con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None;convert zero datetime=True");
                con.Open();
                String sqlString2 = "SELECT * FROM appointment WHERE customerId = " + CurrentID.SelectedID +
                    " AND(start BETWEEN '" + Start + "' AND '" + End + "');";
                MySqlCommand cmd2 = new MySqlCommand(sqlString2, con);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                DataTable ds2 = new DataTable();
                adp2.Fill(ds2);
                dgvAppts.DataSource = ds2;
                dgvAppts.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                cmd2.ExecuteNonQuery();
                con.Close();

                //check appointment w/in 15 min
                TimeZoneInfo currenttimezone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
                DateTime localtime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, currenttimezone);
                
                

                foreach (DataRow row in ds2.Rows)
                {
                        DateTime srowtime = TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("start"), currenttimezone);
                        row[9] = srowtime;
                        DateTime erowtime = TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("end"), currenttimezone);
                        row[10] = erowtime;
                        //DateTime 
                        if (srowtime.Date == localtime.Date)
                        {
                            int min = (srowtime - localtime).Minutes;
                            if (min <= 15 && min > 0)
                            {
                                MessageBox.Show("You have an appointment within 15 minutes.");
                            }
                        }
                    }
            }
            else { MessageBox.Show("Please select a customer."); }
        }

        private void BtLoginRec_Click(object sender, EventArgs e)
        {
            UserLog ul = new UserLog();
            ul.Show();
        }

        private void btMonthApp_Click(object sender, EventArgs e)
        {
            MonthApp ma = new MonthApp();
            ma.Show();
        }

        private void btSchedule_Click(object sender, EventArgs e)
        {
            Schedule sched = new Schedule();
            sched.Show();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            CurrentID.SelectedDate = monthCalendar1.SelectionRange.Start;
            if (rbWeek.Checked == true)
            {
                rbWeek_Click(sender, e);
            }
            else if (rbMonth.Checked == true)
            {
                rbMonth_Click(sender, e);            
            }
        }

        private void rbWeek_Click(object sender, EventArgs e)
        {
            if (rbWeek.Checked == true)
                if (indexSelectedCust >= 0)
                {
                    CurrentID.SelectedDate = monthCalendar1.SelectionRange.Start;
                    DateTime weekFromNow = CurrentID.SelectedDate.AddDays(7);
                    string week = weekFromNow.ToString("yyyy-MM-dd");
                    string today = CurrentID.SelectedDate.ToString("yyyy-MM-dd");
                    con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None;convert zero datetime=True;");
                    con.Open();
                    String sqlString2 = "SELECT * FROM appointment WHERE customerId = " + CurrentID.SelectedID +
                        " AND start BETWEEN '" + today + "' AND '" + week + "';";
                    MySqlCommand cmd2 = new MySqlCommand(sqlString2, con);
                    MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                    DataTable ds2 = new DataTable();
                    adp2.Fill(ds2);
                    dgvAppts.DataSource = ds2;
                    dgvAppts.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    cmd2.ExecuteNonQuery();
                    con.Close();

                    //check appointment w/in 15 min
                    TimeZoneInfo currenttimezone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
                    DateTime localtime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, currenttimezone);

                    foreach (DataRow row in ds2.Rows)
                    {
                        DateTime srowtime = TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("start"), currenttimezone);
                        row[9] = srowtime;
                        DateTime erowtime = TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("end"), currenttimezone);
                        row[10] = erowtime;
                        //DateTime 
                        if (srowtime.Date == localtime.Date)
                        {
                            int min = (srowtime - localtime).Minutes;
                            if (min <= 15 && min > 0)
                            {
                                MessageBox.Show("You have an appointment within 15 minutes.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a customer.");
                }
        }

        private void rbMonth_Click(object sender, EventArgs e)
        {
            if (rbMonth.Checked == true)
                if (indexSelectedCust >= 0)
                {
                    CurrentID.SelectedDate = monthCalendar1.SelectionRange.Start;
                    //DateTime today = DateTime.Today;
                    DateTime monthStart = new DateTime(CurrentID.SelectedDate.Year, CurrentID.SelectedDate.Month, 1);
                    DateTime monthEnd = monthStart.AddMonths(1);
                    string Start = monthStart.ToString("yyyy-MM-dd");
                    string End = monthEnd.ToString("yyyy-MM-dd");
                    con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None;convert zero datetime=True");
                    con.Open();
                    String sqlString2 = "SELECT * FROM appointment WHERE customerId = " + CurrentID.SelectedID +
                        " AND(start BETWEEN '" + Start + "' AND '" + End + "');";
                    MySqlCommand cmd2 = new MySqlCommand(sqlString2, con);
                    MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                    DataTable ds2 = new DataTable();
                    adp2.Fill(ds2);
                    dgvAppts.DataSource = ds2;
                    dgvAppts.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    cmd2.ExecuteNonQuery();
                    con.Close();

                    //check appointment w/in 15 min
                    TimeZoneInfo currenttimezone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
                    DateTime localtime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, currenttimezone);

                    foreach (DataRow row in ds2.Rows)
                    {
                        DateTime srowtime = TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("start"), currenttimezone);
                        row[9] = srowtime;
                        DateTime erowtime = TimeZoneInfo.ConvertTimeFromUtc(row.Field<DateTime>("end"), currenttimezone);
                        row[10] = erowtime;
                        //DateTime 
                        if (srowtime.Date == localtime.Date)
                        {
                            int min = (srowtime - localtime).Minutes;
                            if (min <= 15 && min > 0)
                            {
                                MessageBox.Show("You have an appointment within 15 minutes.");
                            }
                        }
                    }
                }
                else { MessageBox.Show("Please select a customer."); }
        }
    }
}
