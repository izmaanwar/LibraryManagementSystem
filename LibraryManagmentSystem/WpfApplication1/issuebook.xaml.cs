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
    /// Interaction logic for issuebook.xaml
    /// </summary>
    public partial class issuebook : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
        public issuebook(string user)
        {
            InitializeComponent();
            s_ID.Content = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           string connectionString = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand issueID = new MySqlCommand("select max(issued_id) from library.issued_Books;", connection);
            MySqlDataReader mdr ;
            mdr = issueID.ExecuteReader();
            int count = 0;
            if(mdr.Read())
            {
                try
                {
                    count = Convert.ToInt32(mdr.GetString("max(Issued_id)"));
                }
                catch(Exception a)
                {
                    count = 0;
                }
            }
            mdr.Close();
            MySqlCommand getCopies = new MySqlCommand("select noOfCopies,total_issued_copies from library.books where book_id=" 
                + id_book.Text + ";", connection);
            MySqlDataReader gDR = getCopies.ExecuteReader();
            string noOfCopies="", TotalCopies="";
            if(gDR.Read())
            {
                noOfCopies = gDR.GetString("noOfCopies");
                TotalCopies = gDR.GetString("total_issued_copies");
            }
            gDR.Close();
            if (noOfCopies != TotalCopies)
            {
                DateTime dueDate = issueDate.SelectedDate.Value;
                dueDate = dueDate.AddDays(10);
                string user = s_ID.Content.ToString();
                string cmd = "insert into  library.issued_Books(issued_id,mem_id,staff_id,issueDate,dueDate,status,Book_id) values("
                    + (count + 1) + ",'" + mem_id.Text + "','" + user + "', convert('" + issueDate.SelectedDate.Value.ToString("yyyy-MM-dd")
                    + "',date),convert('" + dueDate.ToString("yyyy-MM-dd") + "',date),'borrowed','" + id_book.Text + "');";
                MySqlCommand cmd1 = new MySqlCommand("update books set total_issued_copies=total_issued_copies+1 where book_id="
                    + id_book.Text + ";", connection);
                
                MySqlCommand command = new MySqlCommand(cmd, connection);
                command.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                MySqlCommand userCopies = new MySqlCommand("update library.members set total_issued_books=total_issued_books+1 where mem_id='" + mem_id.Text + "';", connection);
                userCopies.ExecuteNonQuery();
                MessageBox.Show("Book Issued");
                connection.Close();
            }
            else
                MessageBox.Show("No Books Available");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }


    }
}
