using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for changePass.xaml
    /// </summary>
    public partial class changePass : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            this.Hide();
            base.OnClosed(e);
        }
        public changePass(string user,string userType)
        {
            InitializeComponent();
            user_id.Content = user;
            type.Content = userType;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }

        private void chngPass_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("server=localHost;uid=root;pwd=nakr1234;database=library;");
            connection.Open();
            if (type.Content == "member")
            {
                MySqlCommand oldpass = new MySqlCommand("select password from library.members where mem_id='" + user_id.Content.ToString() + "';", connection);
                MySqlDataReader mdr = oldpass.ExecuteReader();
                string oldPassword="";
                if(mdr.Read())
                {
                    oldPassword=mdr.GetString("password");
                }
                mdr.Close();
                if (oldPassword == old.Password.ToString())
                {
                    MySqlCommand com = new MySqlCommand("update library.members set password='" + newPass.Password + "' where mem_id='"+user_id.Content.ToString()+"';", connection);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Password Changed");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect old Password");
                }
            }
            else
            {
                MySqlCommand oldpass = new MySqlCommand("select password from library.staff where staff_id='" + user_id.Content.ToString() + "';", connection);
                MySqlDataReader mdr = oldpass.ExecuteReader();
                string oldPassword = "";
                if (mdr.Read())
                {
                    oldPassword = mdr.GetString("password");
                }
                mdr.Close();
                if (oldPassword == old.Password.ToString())
                {
                    MySqlCommand com = new MySqlCommand("update library.staff set password='" + newPass.Password + "' where staff_id='"+user_id.Content.ToString()+"';", connection);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Password Changed");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect old Password");
                }
            }
            connection.Close();
        }
    }
}
