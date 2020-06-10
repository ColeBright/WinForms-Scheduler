using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using MySql.Data.MySqlClient;
using System.IO;

namespace ColeBrightSchedulingAppC969
{

    public partial class Form1 : Form
    {
        private MySqlConnection con;
        private string errorString;
        public Form1()
        {
            
            void switchLang(string cult)
            {
                switch (cult)
                {
                    case "en":
                        lbuserID.Text = "User ID: ";
                        lbpassword.Text = "Password: ";
                        btnSubmit.Text = "Submit";
                        errorString = "Incorrect User ID or Password.";
                        break;
                    case "es":
                        lbuserID.Text = "ID de User: ";
                        lbpassword.Text = "Contrasena: ";
                        btnSubmit.Text = "Enviar";
                        errorString = "Identificación de usuario o contraseña incorrecta.";
                        break;
                }

            }
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture.ClearCachedData();
            var thread = new Thread(
            s => ((Place)s).Result = Thread.CurrentThread.CurrentCulture);
            //this lambda takes a parameter, casts it as type "Place", invokes its sole member Result 
            //passes it to an inline function that finds the user's culture on  
            //another thread
            var place = new Place();
            thread.Start(place);
            thread.Join();
            var culture = place.Result;
            switchLang(culture.TwoLetterISOLanguageName);
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None");
            string query = "Select * from user where userName= '" + tbUserID.Text + "' and password='" + tbPassword.Text + "'";
            MySqlDataAdapter mysda = new MySqlDataAdapter(query, con);
            DataTable dattab = new DataTable();
            mysda.Fill(dattab);
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    CurrentID.CurrentUser = (reader["userName"].ToString());
                    CurrentID.CurrentUserId = (Convert.ToInt32(reader["userId"]));
                }
                if (dattab.Rows.Count == 1)
                {
                    CurrentID.CurrentUser = tbUserID.Text;
                    string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "LoginLogs.txt"), true))
                    {
                        outputFile.WriteLine(DateTime.Now);
                    }
                    this.Hide();
                    Main main = new Main();
                    main.Show();
                }
                else
                {
                    MessageBox.Show(errorString);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new MySqlConnection("server=52.206.157.109;userid=U062m2;database=U062m2;password=53688674498;port=3306;SslMode=None");
            con.Open();
            String sqlString = "";
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
        }
    }

    class Place
    {
        public CultureInfo Result { get; set; }
    }
}
