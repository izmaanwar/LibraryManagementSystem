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
using System.Data;
using MySql.Data.MySqlClient;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Returnbook.xaml
    /// </summary>
    public partial class Returnbook : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
        public Returnbook()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string cnStr= "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection cn = new MySqlConnection(cnStr);
            cn.Open();
            MySqlCommand ret = new MySqlCommand("select * from library.issued_books",cn);
            MySqlDataReader mdr = ret.ExecuteReader();
            string s="";
            int i,j;
            if (mdr.Read())
            {
                i = Convert.ToInt32(mdr.GetString("issued_id"));
                j = Convert.ToInt32(mdr.GetString("book_id"));
                s = mdr.GetString("dueDate");
            }
            mdr.Close();
            DateTime dt = returnDate.SelectedDate.Value;
            DateTime dueDate=Convert.ToDateTime(s);
            string status = "update issued_books set status='return', returnDate= convert('"+dt.ToString("yyyy-MM-dd")+"',date)";
            double count=(dt-dueDate).TotalDays;
            if (count > 0)
            {
                status = status + ",fine=" + count.ToString() + "*10";
                MessageBox.Show("Your Fine is " + (count * 10).ToString());
                if (fine.Text == "")
                    MessageBox.Show("Enter Fine Status");
                else
                {

                    status = status + ",fineStatus='" + fine.Text + "' where issued_id=" + issueId.Text + ";";
                    MySqlCommand cmd = new MySqlCommand(status, cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Returned");
                }
            }
            else
            {
                status = status + " where issued_id=" + issueId.Text + ";";
                MySqlCommand cmd = new MySqlCommand(status, cn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Returned");
            }
            cn.Close();












        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }
    }
}
