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
    /// Interaction logic for UpdateBook.xaml
    /// </summary>
    public partial class UpdateBook : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
        public UpdateBook(MySqlDataReader mdr, string book_id)
        {
            InitializeComponent();
            b_id.Content = book_id;
            if (mdr.Read())
            {
                title_id.Text = mdr.GetString("Tittle");
                name.Text = mdr.GetString("Author");
                NoOfCopies.Text = mdr.GetString("NoOfCopies");
                PurDate.SelectedDate = Convert.ToDateTime(mdr.GetString("PurchaseDate"));
                Publisher.Text = mdr.GetString("Publisher");
                Edition.Text= mdr.GetString("Edition");
                ShelfID.Text = mdr.GetString("Shelf_ID");
                ISBN_id.Text = mdr.GetString("ISBN");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string con_str = "server=localHost;uid=root;pwd=nakr1234;database=library;";
            MySqlConnection connection = new MySqlConnection(con_str);
            connection.Open();
            string cmd="update library.books set tittle='" + title_id.Text  + "',ISBN='"
                    + ISBN_id.Text + "', Author='" + name.Text + "',noOfCopies=" + NoOfCopies.Text + ",Edition=" + Edition.Text 
                    + ",PurchaseDate=convert('" +PurDate.SelectedDate.Value.ToString("yyyy-MM-dd") +
                    "',date)," + "Publisher='" + Publisher.Text+"', Shelf_id=" + ShelfID.Text+ " where book_id=" + b_id.Content + ";" ;
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.ExecuteNonQuery();
            this.Hide();
            this.Owner.Show();
            connection.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Show();
        }


       
    }
}
