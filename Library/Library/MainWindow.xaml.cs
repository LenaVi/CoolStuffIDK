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
using BooksManagementLib;

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ListBook<BookAndCount> listBook;

        public MainWindow()
        {
            InitializeComponent();
            listBook = new ListBook<BookAndCount>();
            listBook.AddBook(new BookAndCount(new Book("Бесы", new List<string> { "Федор Достоевский" }, new DateTime(2015, 10, 2), "Вече", new List<string> { "Классическая литература", "Русская классика", "Роман" }, 509, 459, 650, 1), 20));
            listBook.AddBook(new BookAndCount(new Book("Собор Парижской Богоматери", new List<string> { "Виктор Гюго" }, new DateTime(1988, 8, 29), "Москва", new List<string> { "Классическая литература", "Зарубежная классика", "Исторический роман" }, 608, 650, 780, 2), 20));
            listBook.AddBook(new BookAndCount(new Book("Мартин Иден", new List<string> { "Джек Лондон" }, new DateTime(1909, 1, 11), "Вече", new List<string> { "Классическая проза", "Зарубежная классика", "Бестселлер" }, 460, 380, 430, 3), 20));
            listBook.AddBook(new BookAndCount(new Book("Мастер и Маргарита", new List<string> { "Михаил Булгаков" }, new DateTime(1967, 12, 30), "Астрель СПб", new List<string> { "Классическая отечественная проза", "Бестселлер" }, 310, 604, 640, 4), 20));
            foreach (var it_book in listBook)
            {
                BoxBook.Items.Add(listBook.PrintBook(it_book));
            }
        }

        private void Click_FiltrList(object sender, RoutedEventArgs e)
        {
            ListBook<BookAndCount> result = listBook;
            if(Allyear.IsChecked==true)
            {
                
            }
            else if(int.Parse(year1.Text)>=0 && int.Parse(year2.Text) >= int.Parse(year1.Text))
            {
                int a = int.Parse(year1.Text);
                result = result.FindAllDate(int.Parse(year1.Text), int.Parse(year2.Text));
            }
            else
            {
                MessageBox.Show("Поля заполнены неверно");
            }
            if (Allpage.IsChecked == true)
            {
                
            }
            else if (int.Parse(page1.Text) >= 0 && int.Parse(page2.Text) >= int.Parse(page1.Text))
            {
                result = result.FindAllDate(int.Parse(page1.Text), int.Parse(page2.Text));
            }
            else
            {
                MessageBox.Show("Поля заполнены неверно");
            }
            if (Allprice.IsChecked == true)
            {

            }
            else if (int.Parse(price1.Text) >= 0 && int.Parse(price2.Text) >= int.Parse(price1.Text))
            {
                result = result.FindAllDate(int.Parse(price1.Text), int.Parse(price2.Text));
            }
            else
            {
                MessageBox.Show("Поля заполнены неверно");
            }
            switch (SortBy.Text)
            {
                case "Год издания ↑":
                    result.SortByDatePublish();
                    BoxBook.Items.Clear();
                    foreach (var it_book in result)
                    {
                        BoxBook.Items.Add(result.PrintBook(it_book));
                    }
                    break;
                case "Год издания ↓":
                    result.SortByDatePublish();
                    result.ReverseList();
                    BoxBook.Items.Clear();
                    foreach (var it_book in result)
                    {
                        BoxBook.Items.Add(result.PrintBook(it_book));
                    }
                    break;
                case "Количество страниц ↑":
                    result.SortByPageCount();
                    BoxBook.Items.Clear();
                    foreach (var it_book in result)
                    {
                        BoxBook.Items.Add(result.PrintBook(it_book));
                    }
                    break;
                case "Количество страниц ↓":
                    result.SortByPageCount();
                    result.ReverseList();
                    BoxBook.Items.Clear();
                    foreach (var it_book in result)
                    {
                        BoxBook.Items.Add(result.PrintBook(it_book));
                    }
                    break;
                case "Цена ↑":
                    result.SortByPrice();
                    BoxBook.Items.Clear();
                    foreach (var it_book in result)
                    {
                        BoxBook.Items.Add(result.PrintBook(it_book));
                    }
                    break;
                case "Цена ↓":
                    result.SortByPrice();
                    result.ReverseList();
                    BoxBook.Items.Clear();
                    foreach (var it_book in result)
                    {
                        BoxBook.Items.Add(result.PrintBook(it_book));
                    }
                    break;
                case "Рейтинг спроса ↑":
                    result.SortByRating();
                    BoxBook.Items.Clear();
                    foreach (var it_book in result)
                    {
                        BoxBook.Items.Add(result.PrintBook(it_book));
                    }
                    break;
                case "Рейтинг спроса ↓":
                    result.SortByRating();
                    result.ReverseList();
                    BoxBook.Items.Clear();
                    foreach (var it_book in result)
                    {
                        BoxBook.Items.Add(result.PrintBook(it_book));
                    }
                    break;
                case "-":
                    BoxBook.Items.Clear();
                    foreach (var it_book in result)
                    {
                        BoxBook.Items.Add(result.PrintBook(it_book));
                    }
                    break;
                default:
                    MessageBox.Show("Не выбран тип сортирвки. Если не нужно сортировать, выберите '-'");
                    break;
            }
        }

        private void Click_AllList(object sender, RoutedEventArgs e)
        {
            year1.Text = "0"; year2.Text = "2020"; page1.Text = "0"; page2.Text = "0"; price1.Text = "0"; price2.Text = "0";
            foreach (var it_book in listBook)
            {
                BoxBook.Items.Add(listBook.PrintBook(it_book));
            }
        }

        private void Order_book(object sender, RoutedEventArgs e)
        {
            Book_Order_Win book_Order_Win = new Book_Order_Win();
            book_Order_Win.Show();
        }

        private void Click_Order_this_book(object sender, RoutedEventArgs e)
        {
            Book_Order_Win book_Order_Win = new Book_Order_Win(listBook[BoxBook.SelectedIndex]);
            book_Order_Win.Show();
        }
    }
}
