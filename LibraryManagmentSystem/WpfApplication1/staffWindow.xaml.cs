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
using System.Data;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for staffWindow.xaml
    /// </summary>
    public partial class staffWindow : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
        public staffWindow(string staff)
        {
            InitializeComponent();
            sID.Content = staff;   
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SearchBook search = new SearchBook();
            search.Owner = this;
            this.Hide();
            search.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {  
            issuebook issue = new issuebook(sID.Content.ToString());
            issue.Owner = this;
            issue.Show();
            this.Hide();
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ManageMember mngMember = new ManageMember();
            mngMember.Owner = this;
            this.Hide();
            mngMember.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Returnbook retBook = new Returnbook();
            retBook.Owner = this;
            this.Hide();
            retBook.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Manage_Books mngBook = new Manage_Books();
            mngBook.Owner = this;
            this.Hide();
            mngBook.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
           string connectionString="server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection=new MySqlConnection(connectionString);
            string command="select * from library.books;";
            MySqlCommand cmd=new MySqlCommand(command,connection);
            MySqlDataAdapter dataAdapter=new MySqlDataAdapter(cmd);
            Books book = new Books(dataAdapter);
            book.Owner = this;
            this.Hide();
            book.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string command = "select * from library.issued_books;";
            MySqlCommand cmd = new MySqlCommand(command, connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            Books book = new Books(dataAdapter);
            book.Owner = this;
            this.Hide();
            book.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
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

        private void Button_Click_9(object sender, RoutedEventArgs e)
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
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            string user = sID.Content.ToString();
            MySqlConnection connection = new MySqlConnection("server=localHost;uid=root;pwd=nakr1234;database=library;");
            connection.Open();
            MySqlDataReader mdr;

            string s = "select * from library.staff,library.address where staff.staff_id='" + user + "' AND  staff.staff_id=address.s_id;";
            MySqlCommand cmd = new MySqlCommand(s, connection);
            mdr = cmd.ExecuteReader();
            updateAccount update = new updateAccount(mdr, "staff", user);
            connection.Close();
            update.Owner = this;
            this.Hide();
            update.Show();

        }
    }
}
