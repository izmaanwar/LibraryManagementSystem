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
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Controls.Primitives;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
        public Account()
        {
            InitializeComponent();
   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            string con_str = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(con_str);
            connection.Open();
            MySqlCommand addkey = new MySqlCommand("select max(addressKey) from library.address;", connection);
            MySqlDataReader mdr ;
            mdr = addkey.ExecuteReader();
            int count = 0;
            if(mdr.Read())
            {
                count = Convert.ToInt32( mdr.GetString("max(addressKey)"));
            }
            mdr.Close();
            if (staff.IsChecked==true)
            {
                
                MySqlCommand comm = new MySqlCommand("insert into library.staff(staff_id,firstname,lastname,password,job,salary,hiredate,phoneNo) Values ('"
                    + id.Text + "','" + first.Text + "','"
                    + last.Text + "','" + pass.Password + "','" + job.Text + "','" + sal.Text + ",convert('" + date.SelectedDate.Value.ToString("yyyy-MM-dd") + "',date),'"+phone.Text+"');", connection);  
                string cm = "insert into library.address(addressKey,HouseNo,Block,Town,city,state,zip,s_id,type) Values  (" + (count+1).ToString() +","
                + currH.Text + ",'" + currB.Text + "','" + currT.Text + "','" + currC.Text + "','"
                + currS.Text + "','" + currZ.Text + "','" + id.Text + "','current');";
                

                string cm1 = "insert into library.address(addressKey,HouseNo,Block,Town,city,state,zip,s_id,type) Values  (" + (count+2).ToString() + ","
                    + perrH.Text + ",'" + perrB.Text + "','" + perrT.Text + "','" + perrC.Text + "','"
                    + perrS.Text + "','" + perrZ.Text + "','" + id.Text + "','permanent');";
                
                if (currH.Text == "House#" || currB.Text == "Block" || currT.Text == "Town" || currC.Text == "City" || currS.Text == "State" || currZ.Text == "ZipCode")
                    MessageBox.Show("please Enter complete Current Address", "Error");
                else if (perrH.Text == "House#" || perrB.Text == "Block" || perrT.Text == "Town" || perrC.Text == "City" || perrS.Text == "State" || perrZ.Text == "ZipCode")
                    MessageBox.Show("please Enter complete Permanent Address", "Error");
                else
                {
                    MySqlCommand cmd1 = new MySqlCommand(cm, connection);
                    MySqlCommand cmd2 = new MySqlCommand(cm1, connection);
                    comm.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Member Added");
                    this.Hide();
                    this.Owner.Show();
                }
            }
            else if (member.IsChecked==true)
            {
                MySqlCommand comm = new MySqlCommand("insert into library.members(mem_id,firstname,lastname,email,type,password,phoneNo) Values ('"
                    + id.Text + "','" + first.Text + "','"
                    + last.Text + "','" + email.Text + "','" + user_type.Text + "','" + pass.Password + "','" + phone.Text + "');", connection);
                string cm = "insert into library.address(addressKey,HouseNo,Block,Town,city,state,zip,m_id,type) Values  ("+(count+1).ToString()+","
                + currH.Text + ",'" + currB.Text + "','" + currT.Text + "','" + currC.Text + "','"
                + currS.Text + "','" + currZ.Text + "','" + id.Text + "','current');";
                string cm1 = "insert into library.address(addressKey,HouseNo,Block,Town,city,state,zip,m_id,type) Values  ("+(count+2).ToString()+","
                    + perrH.Text + ",'" + perrB.Text + "','" + perrT.Text + "','" + perrC.Text + "','"
                    + perrS.Text + "','" + perrZ.Text + "','" + id.Text + "','permanent');";
                if (currH.Text == "House#" || currB.Text == "Block" || currT.Text == "Town" || currC.Text == "City" || currS.Text == "State" || currZ.Text == "ZipCode")
                    MessageBox.Show("please Enter complete Current Address", "Error");
                else if (perrH.Text == "House#" || perrB.Text == "Block" || perrT.Text == "Town" || perrC.Text == "City" || perrS.Text == "State" || perrZ.Text == "ZipCode")
                    MessageBox.Show("please Enter complete Permanent Address", "Error");
                else
                {
                    MySqlCommand cmd1 = new MySqlCommand(cm, connection);
                    MySqlCommand cmd2 = new MySqlCommand(cm1, connection);
                    comm.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Member Added");
                    this.Hide();
                    this.Owner.Show();
                }
            }
            else
                MessageBox.Show("Please Select Your ID Type Staff/Member");
            
            connection.Close();
        }
        private void member_Click(object sender, RoutedEventArgs e)
        {
            if (member.IsChecked == true)
            {
                user_type.IsEnabled = true;
                job.IsEnabled = false;
                staff.IsChecked = false;
                date.IsEnabled = false;
                sal.IsEnabled = false;
            }
            else
            {
                user_type.IsEnabled = false;
            }
        }

        private void staff_Click(object sender, RoutedEventArgs e)
        {
            if (staff.IsChecked == true)
            {
                user_type.IsEnabled = false;
                job.IsEnabled = true;
                sal.IsEnabled = true;
               date.IsEnabled = true;
                member.IsChecked = false;
            }
            else
            {
                job.IsEnabled = false;
                sal.IsEnabled = false;
                date.IsEnabled = false;
            }
        }

        private void b_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(date.SelectedDate.Value.ToString("yyyy-MM-dd"));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }   
    }
}
