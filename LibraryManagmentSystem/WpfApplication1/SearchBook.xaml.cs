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
    /// Interaction logic for SearchBook.xaml
    /// </summary>
    public partial class SearchBook : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
        public SearchBook()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool f1=false, f2=false, f3=false;
            string connString = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(connString);
            string command = "select * from library.books where ";
            string isbn_str=ISBN.Text;
            connection.Open();
            MySqlCommand isbn = new MySqlCommand("select ISBN from library.books where books.ISBN='" + isbn_str + "';",connection);
            MySqlDataAdapter i = new MySqlDataAdapter(isbn);
            DataTable dt = new DataTable();
            i.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                command = command + " books.ISBN='" + isbn_str + "'";
                
                f1 = true;
            }
            string author_str = Author.Text;
            MySqlCommand author = new MySqlCommand("select Author from library.books where books.author='" + author_str + "';", connection);
            MySqlDataAdapter a = new MySqlDataAdapter(author);
            dt.Reset();
            a.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if(f1)
                {
                    command = command + " AND books.Author='" + author_str + "'";
                    
                    f2 = true;
                }
                else
                {
                    command = command + " books.Author='" + author_str + "'";
                    f2 = true;
                }
            }
            string tittle_str = Tittle.Text;
            MySqlCommand tittle = new MySqlCommand("select Tittle from library.books where books.Tittle='" + tittle_str + "';", connection);
            MySqlDataAdapter t = new MySqlDataAdapter(tittle);
            dt.Reset();
            t.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (f1||f2)
                {
                    command = command + " AND books.tittle='" + tittle_str + "'";
           
                    f3 = true;
                }
                else
                {
                    command = command + " books.tittle='" + tittle_str + "'";
                }
            }
            string ed_str = Edition.Text;
            MySqlCommand ed = new MySqlCommand("select edition from library.books where books.edition='" + ed_str + "';", connection);
            MySqlDataAdapter edition = new MySqlDataAdapter(ed);
            dt.Reset();
            edition.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (f1 || f2||f3)
                {
                    command = command + " AND books.edition='" + ed_str + "'";
                    
                }
                else
                {
                    command = command + " books.edition='" + ed_str + "'";
                    
                }
            }
            MessageBox.Show(command);
            MySqlCommand cmd1 = new MySqlCommand(command+";", connection);
            MySqlDataAdapter dataAdapter1 = new MySqlDataAdapter(cmd1);
            Books searchedBooks = new Books(dataAdapter1);
            searchedBooks.Owner = this;
            this.Hide();
            searchedBooks.Show();
            connection.Close();
        }

        private void ISBN_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }

 
        
    }
}
