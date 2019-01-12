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
    /// Interaction logic for ManageMember.xaml
    /// </summary>
    public partial class ManageMember : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
        public ManageMember()
        {
            InitializeComponent();
        }

        private void addMember_Click(object sender, RoutedEventArgs e)
        {
            Account addAcount = new Account();
            addAcount.Owner = this;
            this.Hide();
            addAcount.Show();
        }


        private void updateMember_Click(object sender, RoutedEventArgs e)
        {
            string user = "";
            if(memCheck.IsChecked==true)
                user = Convert.ToString("member");
            else if(staffCheck.IsChecked==true)
                user = Convert.ToString("staff");

            if (user == "")
            {
                MessageBox.Show("Please Select Member or Staff", "error");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection("server=localHost;uid=root;pwd=nakr1234;database=library;");
                connection.Open();
                MySqlDataReader mdr;
                string s = "";
                if(user=="member")
                {
                     s = "select * from library.members,library.address where members.mem_id='"+up_id.Text+ "' AND members.mem_id=address.m_id;";
                }
                else if(user=="staff")
                    s = "select * from library.staff,library.address where staff.staff_id='" + up_id.Text + "' AND  staff.staff_id=address.s_id;";
                MySqlCommand cmd = new MySqlCommand(s, connection);
                mdr = cmd.ExecuteReader();
                updateAccount update = new updateAccount(mdr,user,up_id.Text);
                connection.Close();
                update.Owner = this;
                this.Hide();
                update.Show();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }

        private void memCheck_Click(object sender, RoutedEventArgs e)
        {
            if (memCheck.IsChecked == true)
            {
                staffCheck.IsChecked = false;
            }
        }

        private void staffCheck_Click(object sender, RoutedEventArgs e)
        {
            if (staffCheck.IsChecked == true)
            {
                memCheck.IsChecked = false;
            }
        }

        private void staff_check_r_Click(object sender, RoutedEventArgs e)
        {

            if (staff_check_r.IsChecked == true)
            {
                memCheck_r.IsChecked = false;
            }
        }

        private void memCheck_r_Click(object sender, RoutedEventArgs e)
        {
            if (memCheck_r.IsChecked == true)
            {
                staff_check_r.IsChecked = false;
            }
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            string user = "";
            if (memCheck.IsChecked == true)
                user = Convert.ToString("member");
            else if (staffCheck.IsChecked == true)
                user = Convert.ToString("staff");

            if (user == "")
            {
                MessageBox.Show("Please Select Member or Staff", "error");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection("server=localHost;uid=root;pwd=nakr1234;database=library;");
                connection.Open();
                string s = "";
                if (user == "member")
                {
                    s = "delete from library.members where members.mem_id='" + up_id.Text + ";";
                }
                else if (user == "staff")
                    s = "delete from library.staff where staff.staff_id='" + up_id.Text + ";";
                MySqlCommand cmd = new MySqlCommand(s, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("User Succesfully Deleted");
            }
        }
        
    }
}
