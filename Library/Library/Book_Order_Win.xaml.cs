using System;
using System.Collections;
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
using BooksManagementLib;

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для Book_Order_Win.xaml
    /// </summary>
    public partial class Book_Order_Win : Window
    {
        Book it_book;
        public Book_Order_Win()
        {
            InitializeComponent();
        }

        public Book_Order_Win(Book book)
        {
            it_book = book;
            Author.Text = it_book.Authors[0];
            Title.Text = it_book.Title;
            InitializeComponent();
        }

        private void Click_Order(object sender, RoutedEventArgs e)
        {
            if(Name.Text!=string.Empty && Surname.Text != string.Empty && (Phone_number.Text != string.Empty || Email.Text != string.Empty) &&
                Author.Text != string.Empty && Title.Text != string.Empty && Count.Text != string.Empty && int.Parse(Count.Text)>0)
            {
                Dictionary<Book, int> order = new Dictionary<Book, int>();
                order.Add(it_book, int.Parse(Count.Text));
                BookOrder bookOrder = new BookOrder(string.Format(Surname.Text + Name.Text), Phone_number.Text, Email.Text, order);
            }
            {
                MessageBox.Show("Поля заполнены неверно");
            }
        }
    }
}
