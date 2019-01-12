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
    /// Interaction logic for Manage_Books.xaml
    /// </summary>
    public partial class Manage_Books : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
        public Manage_Books()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           AddBook add = new AddBook();
            add.Owner = this;
            this.Hide();
            add.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string book = Book_id.Text;
            string connectionString = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlDataReader mdr;
            string s= "select * from library.books where book_id=" + Book_id.Text + ";";
            MySqlCommand cmd = new MySqlCommand(s, connection);
            mdr = cmd.ExecuteReader();
            UpdateBook update = new UpdateBook(mdr, book);
             connection.Close();
            update.Owner = this;
            this.Hide();
            update.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }

        
    }
}
