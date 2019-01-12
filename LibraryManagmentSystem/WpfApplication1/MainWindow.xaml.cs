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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string myConnectionString = "server=localHost;uid=root;" +
           "pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            if(type.Text=="Members")
            {
                MySqlCommand cmd = new MySqlCommand("select * from library.members where members.mem_id='"
                + id1.Text + "' AND members.password='" + pass.Password + "';", connection);
                DataTable dt = new DataTable();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                dataAdapter.Fill(dt);
                int countOfRows = Convert.ToInt32(dt.Rows.Count.ToString());
                if (countOfRows == 1)
                {
                    label3.Content = "";
                    pass.Password = "";
                    memWindow m1 = new memWindow(id1.Text);
                    m1.Owner = this;
                    this.Hide();
                    m1.ShowDialog();
                }
                else if (countOfRows == 0)
                {
                    label3.Content = "Invalid user or password";
                }
            }
            else
            {
                MySqlCommand cmd1 = new MySqlCommand("select * from library.staff where staff.staff_id='"
               + id1.Text + "' AND staff.password='" + pass.Password + "';", connection);
                DataTable dt1 = new DataTable();
                MySqlDataAdapter dataAdapter1 = new MySqlDataAdapter(cmd1);
                dataAdapter1.Fill(dt1);
                int countOfRows = Convert.ToInt32(dt1.Rows.Count.ToString());
                if(countOfRows == 1)
                {
                    label3.Content = "";
                    pass.Password = "";
                    staffWindow s1 = new staffWindow(id1.Text);
                    s1.Owner = this;
                    this.Hide();
                    s1.ShowDialog();
                    
                    
                    connection.Close();
                }
                else if (countOfRows == 0)
                {
                    label3.Content = "Invalid user or password";
                }
            }
            connection.Close();
            // grid.DataContext = dt;
        }

    }
}
