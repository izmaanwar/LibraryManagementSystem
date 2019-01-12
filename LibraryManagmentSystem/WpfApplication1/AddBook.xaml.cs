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
using MySql.Data.MySqlClient;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
        public AddBook()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connString = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(connString);
            connection.Open();
            string cmd = "insert into library.books(Book_iD,tittle,author,noOfCopies,PurchaseDate,Publisher,Edition,shelf_id,ISBN,subject) Values ("
                  + B_id.Text + ",'" + B_title.Text + "','"
                   + B_au.Text + "'," + B_copy.Text + ",convert('"
           + PurDate.SelectedDate.Value.ToString("yyyy-MM-dd") + "',date),'" + B_pub.Text + "'," + B_ed.Text + "," + B_shelf.Text + ",'" + B_ISBN.Text + "','"+subj.Text+"');";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("Book Added Succesfully");
            connection.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }
    }  
}
