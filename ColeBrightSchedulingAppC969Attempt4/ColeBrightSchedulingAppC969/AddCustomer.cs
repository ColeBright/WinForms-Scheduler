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
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Hide();
            Main mn = new Main();
            mn.Show();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (tbCountry.Text != "" && tbcity.Text != "" && tbAddr.Text != "" && tbAddr2.Text != "" && tbcity.Text != "" && tbZip.Text != "" && tbPhone.Text != "" && tbname.Text != "")
            {
                using (MySqlConnection mysqa = new
          MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None"))
                {

                    string query1 = "INSERT INTO country(country, createDate, createdBy, lastUpdateBy) VALUES(@country, @createdate, @createby, @lastUpdateBy)";
                    MySqlCommand cmd1 = new MySqlCommand(query1, mysqa);
                    cmd1.Connection.Open();
                    cmd1.Parameters.AddWithValue("@country", tbCountry.Text);
                    cmd1.Parameters.AddWithValue("@createdate", createdate.Value);
                    cmd1.Parameters.AddWithValue("@createby", CurrentID.CurrentUser);
                    cmd1.Parameters.AddWithValue("@lastUpdateBy", CurrentID.CurrentUser);
                    cmd1.ExecuteNonQuery();
                    int countid = (int)cmd1.LastInsertedId;
                    cmd1.Connection.Close();

                    string query2 = "INSERT INTO city(city, countryId, createDate, createdBy, lastUpdateBy) VALUES(@city, @countryId, @createdate, @createby, @lastUpdateBy)";
                    MySqlCommand cmd2 = new MySqlCommand(query2, mysqa);
                    cmd2.Connection.Open();
                    cmd2.Parameters.AddWithValue("@city", tbcity.Text);
                    cmd2.Parameters.AddWithValue("@countryId", countid);
                    cmd2.Parameters.AddWithValue("@createdate", createdate.Value);
                    cmd2.Parameters.AddWithValue("@createby", CurrentID.CurrentUser);
                    cmd2.Parameters.AddWithValue("@lastUpdateBy", CurrentID.CurrentUser);
                    cmd2.ExecuteNonQuery();
                    int citid = (int)cmd2.LastInsertedId;
                    cmd2.Connection.Close();

                    string query3 = "Insert INTO address(address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdateBy) " +
                        "VALUES(@address, @address2, @cityId, @zip, @phone, @createdate, @createby, @lastUpdateBy);";
                    MySqlCommand cmd3 = new MySqlCommand(query3, mysqa);
                    cmd3.Connection.Open();
                    cmd3.Parameters.AddWithValue("@address", tbAddr.Text);
                    cmd3.Parameters.AddWithValue("@address2", tbAddr2.Text);
                    cmd3.Parameters.AddWithValue("@cityId", citid);
                    cmd3.Parameters.AddWithValue("@zip", tbZip.Text);
                    cmd3.Parameters.AddWithValue("@phone", tbPhone.Text);
                    cmd3.Parameters.AddWithValue("@createdate", createdate.Value);
                    cmd3.Parameters.AddWithValue("@createby", CurrentID.CurrentUser);
                    cmd3.Parameters.AddWithValue("@lastUpdateBy", CurrentID.CurrentUser);
                    cmd3.ExecuteNonQuery();
                    int addid = (int)cmd3.LastInsertedId;
                    cmd3.Connection.Close();

                    string query4 = "INSERT INTO customer(customerName, addressId, createDate, createdBy, lastUpdateBy) " +
                        "VALUES (@Name, @addid, @createdate, @createby, @lastUpdateBy);";
                    MySqlCommand cmd4 = new MySqlCommand(query4, mysqa);
                    cmd4.Connection.Open();
                    cmd4.Parameters.AddWithValue("@Name", tbname.Text);
                    cmd4.Parameters.AddWithValue("@addId", addid);
                    cmd4.Parameters.AddWithValue("@createdate", createdate.Value);
                    cmd4.Parameters.AddWithValue("@createby", CurrentID.CurrentUser);
                    cmd4.Parameters.AddWithValue("@lastUpdateBy", CurrentID.CurrentUser);
                    cmd4.ExecuteNonQuery();
                    cmd4.Connection.Close();

                }
                Hide();
                Main mn = new Main();
                mn.Show();
            }
            else
            {
                MessageBox.Show("Please fill all fields.");
            }
        }
    }
}
