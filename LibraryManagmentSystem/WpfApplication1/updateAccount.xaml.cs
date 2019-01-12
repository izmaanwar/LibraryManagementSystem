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
    /// Interaction logic for updateAccount.xaml
    /// </summary>
    public partial class updateAccount : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            this.Hide();
            this.Owner.Show();
            base.OnClosed(e);
        }
        public updateAccount(MySqlDataReader mdr,string user,string user_id)
        {
            InitializeComponent();
            if (user == "staff")
            {
                job.IsEnabled = true;
                sal.IsEnabled = true;
                hire.IsEnabled = true;
                user_type.IsEnabled = false;
                if(mdr.Read())
                {
                    job.Text = mdr.GetString("job");
                    sal.Text = mdr.GetString("salary");
                    hire.SelectedDate = Convert.ToDateTime(mdr.GetString("hiredate"));
                }
            }
            else if (user == "member")
            {
                job.IsEnabled = false;
                sal.IsEnabled = false;
                hire.IsEnabled = false;
                user_type.IsEnabled = true;
                if(mdr.Read())
                {
                    user_type.Text = mdr.GetString("type");
                }
             }
            while(mdr.Read())
            {
                fname.Text=mdr.GetString("FirstName");
                lname.Text = mdr.GetString("LastName");
                email.Text = mdr.GetString("email");
                currH.Text = mdr.GetString("houseNo");
                currB.Text = mdr.GetString("Block");
                currT.Text = mdr.GetString("Town");
                currC.Text = mdr.GetString("City");
                currS.Text = mdr.GetString("State");
                currZ.Text = mdr.GetString("zip");
                perrH.Text = mdr.GetString("houseNo");
                perrB.Text = mdr.GetString("Block");
                perrT.Text = mdr.GetString("Town");
                perrC.Text = mdr.GetString("City");
                perrS.Text = mdr.GetString("State");
                perrZ.Text = mdr.GetString("zip");

            }
            u_id.Content = user_id;
            u_type.Content = user;
        }
        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            string con_str = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(con_str);
            connection.Open();
            MySqlCommand addkey = new MySqlCommand("select max(addressKey) from library.address;", connection);
            MySqlDataReader mdr;
            mdr = addkey.ExecuteReader();
            int count = 0;
            if (mdr.Read())
            {
                count = Convert.ToInt32(mdr.GetString("max(addressKey)"));
            }
            mdr.Close();
            if (user_type.IsEnabled==false)
            {

                MySqlCommand comm = new MySqlCommand("update library.staff set firstName='"
                    +fname.Text+"',lastName='"+lname.Text+"',email='"+email.Text+"',job='"
                    +job.Text+"',salary="+sal.Text+",hiredate=convert('"+hire.SelectedDate.Value.ToString("yyyy-MM-dd")+"',date) where staff_id='"+u_id.Content+"';",connection);
                string cm = "update library.address set houseNo=" + currH.Text + ",Block='" + currB.Text + "',Town='"
                    + currT.Text + "',City='" + currC.Text + "',State='" + currS.Text + "',zip='" + currZ.Text + "',type='current' where s_id='" + u_id.Content + "';";


                string cm1 = "update library.address set houseNo=" + perrH.Text + ",Block='" + perrB.Text + "',Town='"
                    + perrT.Text + "',City='" + perrC.Text + "',State='" + perrS.Text + "',zip='" + perrZ.Text + "',type='permanent' where s_id='" + u_id.Content + "';";

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
            {
               MySqlCommand comm=new MySqlCommand( "update library.members set firstName='"
                    +fname.Text+"',lastName='"+lname.Text+"',email='"+email.Text+"',type='"+user_type.Text+"' where mem_id='"+u_id.Content+"';",connection);
               string cm = "update library.address set houseNo=" + currH.Text + ",Block='" + currB.Text + "',Town='"
                   + currT.Text + "',City='" + currC.Text + "',State='" + currS.Text + "',zip='" + currZ.Text + "',type='current' where m_id='" + u_id.Content + "';";


               string cm1 = "update library.address set houseNo=" + perrH.Text + ",Block='" + perrB.Text + "',Town='"
                   + perrT.Text + "',City='" + perrC.Text + "',State='" + perrS.Text + "',zip='" + perrZ.Text + "',type='permanent' where m_id='" + u_id.Content + "';";
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

            connection.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            changePass pass = new changePass(u_id.Content.ToString(),u_type.Content.ToString());
            pass.Owner = this;
            pass.Show();

        }

    }
}
