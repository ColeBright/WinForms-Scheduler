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
    public partial class ModifyCustomer : Form
    {
        int countid = 0;
        public ModifyCustomer()
        {
            InitializeComponent();
            
            MySqlConnection con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None");
            try
            {
                con.Open();
                int dgvselection = CurrentID.SelectedID;
                String sqlString = "SELECT * FROM customer, address, city, country WHERE customer.customerId =" + dgvselection +
                    " AND address.addressId = customer.addressId AND city.cityId = address.cityId " +
                     "AND country.countryId =  city.countryId;";
                MySqlCommand cmd = new MySqlCommand(sqlString, con);
                countid = (int)cmd.LastInsertedId;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tbname.Text = (reader["customerName"].ToString());
                        tbAddr.Text = (reader["address"].ToString());
                        tbAddr2.Text = (reader["address2"].ToString());
                        tbcity.Text = (reader["city"].ToString());
                        tbZip.Text = (reader["postalCode"].ToString());
                        tbPhone.Text = (reader["phone"].ToString());
                        tbCountry.Text = (reader["country"].ToString());
                    }
                }
            }
            finally
            {
                con.Close();
            }
            
        }

        private void ModifyCustomer_Load(object sender, EventArgs e)
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
            using (MySqlConnection mysqa = new
             MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None"))
            {

                string query1 = "UPDATE country SET country = @country, lastUpdateBy = @lastUpdateBy " +
                    "WHERE countryId = "+ countid + ";";
                MySqlCommand cmd1 = new MySqlCommand(query1, mysqa);
                cmd1.Connection.Open();
                cmd1.Parameters.AddWithValue("@country", tbCountry.Text);
                cmd1.Parameters.AddWithValue("@lastUpdateBy", CurrentID.CurrentUser);
                cmd1.Parameters.AddWithValue("@city", tbcity.Text);
                cmd1.ExecuteNonQuery();
                countid = (int)cmd1.LastInsertedId;
                cmd1.Connection.Close();

                string query2 = "UPDATE city INNER JOIN address ON(city.cityId = address.cityId) SET city= @city, city.lastUpdateBy = @lastUpdateBy" +
                    ";"; 
                MySqlCommand cmd2 = new MySqlCommand(query2, mysqa);
                cmd2.Connection.Open();
                cmd2.Parameters.AddWithValue("@city", tbcity.Text);
                //cmd2.Parameters.AddWithValue("@countryId", countid);
                cmd2.Parameters.AddWithValue("@lastUpdateBy", CurrentID.CurrentUser);
                cmd2.ExecuteNonQuery();
                int citid = (int)cmd2.LastInsertedId;
                cmd2.Connection.Close();

                string query3 = "UPDATE address SET address = @address, address2 = @address2," +
                    " postalCode = @zip, phone = @phone, lastUpdateBy = @lastUpdateBy" +
                    " WHERE addressId =" + CurrentID.SelectedID + ";";
                MySqlCommand cmd3 = new MySqlCommand(query3, mysqa);
                cmd3.Connection.Open();
                cmd3.Parameters.AddWithValue("@address", tbAddr.Text);
                cmd3.Parameters.AddWithValue("@address2", tbAddr2.Text);
                //cmd3.Parameters.AddWithValue("@cityId", citid);
                cmd3.Parameters.AddWithValue("@zip", tbZip.Text);
                cmd3.Parameters.AddWithValue("@phone", tbPhone.Text);
                cmd3.Parameters.AddWithValue("@lastUpdateBy", CurrentID.CurrentUser);
                cmd3.ExecuteNonQuery();
                int addid = (int)cmd3.LastInsertedId;
                cmd3.Connection.Close();

                string query4 = "UPDATE customer SET customerName =  @Name, lastUpdateBy = @lastUpdateBy " +
                    " WHERE customerId =" + CurrentID.SelectedID + ";";
                MySqlCommand cmd4 = new MySqlCommand(query4, mysqa);
                cmd4.Connection.Open();
                cmd4.Parameters.AddWithValue("@Name", tbname.Text);
                cmd4.Parameters.AddWithValue("@addId", addid);
                cmd4.Parameters.AddWithValue("@lastUpdateBy", CurrentID.CurrentUser);
                cmd4.ExecuteNonQuery();
                cmd4.Connection.Close();

            }
            Hide();
            Main mn = new Main();
            mn.Show();
        }
    }
}
