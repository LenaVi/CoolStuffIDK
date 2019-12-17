using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManagementLib
{
    public class Book
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public DateTime PublishDate { get; set; }
        public string Publisher { get; set; }
        public List<string> Genre { get; set; }
        public int PageCount { get; set; }
        public double Price { get; set; }
        public double Markup { get; set; }
        public int Rating {get;set;}
        public Book(string title, List<string> authors, DateTime publish_date, string publishing, List<string> genre, 
            int page_count, double price, double markup,int the_rating_of_demand)
        {
            Title = title;
            Authors = authors;
            PublishDate = publish_date;
            Publisher = publishing;
            Genre = genre;
            PageCount = page_count;
            Price = price;
            Markup = markup;
            Rating = the_rating_of_demand;
        }
        public StringBuilder ShowInfo()
        {
            StringBuilder str = new StringBuilder();
            str.Append(string.Format("Название: " + Title + "\n" + "Авторы: "));
            for(int i=0;i<Authors.Count-1;i++)
            {
                str.Append(string.Format(Authors[i] + ", "));
            }
            str.Append(Authors[Authors.Count - 1] + "\n" + "Дата публикации: " + PublishDate + "\n" + "Издательство: " + Publisher + "\n" + "Жанры: ");
            for (int i = 0; i < Genre.Count - 1; i++)
            {
                str.Append(Genre[i] + ", ");
            }
            str.Append(Genre[Genre.Count - 1] + "\n" + "Колличество страниц: " + PageCount + "\n" + "Цена: " + Price + "\n"
                + "Розничная цена: " + Markup + "\n" + "Рейтинг спроса: " + Rating);
            return str;
        }
    }
}
