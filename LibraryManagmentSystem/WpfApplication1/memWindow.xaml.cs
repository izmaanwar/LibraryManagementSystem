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
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for memWindow.xaml
    /// </summary>
    public partial class memWindow : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
        public memWindow(string user_id)
        {
            InitializeComponent();
            u_id.Content = user_id;
        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }

        private void viewBooks_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string command = "select * from library.books;";
            MySqlCommand cmd = new MySqlCommand(command, connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            Books book = new Books(dataAdapter);
            book.Owner = this;
            this.Hide();
            book.Show();
            connection.Close();
        }

        private void searchBook_Click(object sender, RoutedEventArgs e)
        {
            SearchBook search = new SearchBook();
            search.Owner = this;
            this.Hide();
            search.Show();
        }

        private void viewMag_Click(object sender, RoutedEventArgs e)
        {
            string str_con = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(str_con);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("select * from library.magazine;", connection);
            MySqlDataAdapter dtAdapter = new MySqlDataAdapter(cmd);
            Magazine mag = new Magazine(dtAdapter);
            mag.Owner = this;
            this.Hide();
            mag.Show();
            connection.Close();
        }

        private void viewNews_Click(object sender, RoutedEventArgs e)
        {
            string str_con = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(str_con);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("select * from library.newspaper;", connection);
            MySqlDataAdapter dtAdapter = new MySqlDataAdapter(cmd);
            Newspaper news = new Newspaper(dtAdapter);
            news.Owner = this;
            this.Hide();
            news.Show();
            connection.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string user = u_id.Content.ToString();
            MySqlConnection connection = new MySqlConnection("server=localHost;uid=root;pwd=nakr1234;database=library;");
            connection.Open();
            MySqlDataReader mdr;

            string s = "select * from library.members,library.address where members.mem_id='" + user + "' AND  members.mem_id=address.m_id;";
            MySqlCommand cmd = new MySqlCommand(s, connection);
            mdr = cmd.ExecuteReader();
            updateAccount update = new updateAccount(mdr, "member", user);
            connection.Close();
            update.Owner = this;
            this.Hide();
            update.Show();
        }

        private void IssuedBook_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string command = "select * from library.issued_books where mem_id='"+u_id.Content+"';";
            MySqlCommand cmd = new MySqlCommand(command, connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            Books book = new Books(dataAdapter);
            book.Owner = this;
            this.Hide();
            book.Show();
            connection.Close();
        }

        private void fine_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string command = "select sum(if(issued_books.fineStatus='Not Payed',fine,0)) MEMFINE from library.issued_books where mem_id='"+u_id.Content+"';";
            MySqlCommand cmd = new MySqlCommand(command, connection);
            MySqlDataReader mdr = cmd.ExecuteReader();
            string fine = "";
            if(mdr.Read())
            {
                fine = mdr.GetString("MEMFINE");
            }
            MessageBox.Show(fine);
            connection.Close();
        }
    }
}
