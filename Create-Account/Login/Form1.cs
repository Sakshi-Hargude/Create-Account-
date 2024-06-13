using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Login
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        MySqlCommand command;
        MySqlDataReader mdr;


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            connection.Open();
            string selectQuery = "SELECT * FROM loginform.userinfo WHERE Username = '" + txtUsername.Text + "' AND Password = '" + txtPassword.Text + "';";
            command = new MySqlCommand(selectQuery, connection);
            mdr = command.ExecuteReader();
            if (mdr.Read())
            {
                string MyConnection2 = "datasource=localhost;port=3306;username=root;password=";
                string Query = "update loginform.userinfo set LastLogin='" + dateTimePicker1.Value + "' where Username='" + this.txtUsername.Text + "';";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);

                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();

                MessageBox.Show("Login Successful!");
                this.Hide();
                LoginSuccessForm frm2 = new LoginSuccessForm();
                frm2.ShowDialog();

            }
            else
            {

                MessageBox.Show("Incorrect Login Information! Try again.");
            }

            connection.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            connection.Open();
            string selectQuery = "SELECT * FROM loginform.userinfo WHERE Username = '" + txtUsername.Text + "';";
            command = new MySqlCommand(selectQuery, connection);
            mdr = command.ExecuteReader();
            if (mdr.Read())
            {
                MessageBox.Show("Username not available!");

            }
            else
            {

                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=loginform;";
                string iquery = "INSERT INTO userinfo(`ID`,`Username`, `Password`, `DateCreated`,`LastLogin`) VALUES (NULL, '" + txtUsername.Text + "', '" + txtPassword.Text + "', '" + dateTimePicker1.Value + "', '" + dateTimePicker1.Value + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(iquery, databaseConnection);
                commandDatabase.CommandTimeout = 60;

                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    databaseConnection.Close();
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }

                MessageBox.Show("Account Successfully Created!");
            }

            connection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    }
    

