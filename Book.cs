using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    public class Book
    {
        public string _Title { get; set; }
        public List<string> _Authors { get; set; }
        public DateTime _Publish_Date { get; set; }
        public string _Publishing { get; set; }
        public List<string> _Genre { get; set; }
        public int _Page_Count { get; set; }
        public double _Price { get; set; }
        public double _Markup { get; set; }
        public int _The_Rating_Of_Demand {get;set;}
        public Book(string title, List<string> authors, DateTime publish_date, string publishing, List<string> genre, 
            int page_count, double price, double markup,int the_rating_of_demand)
        {
            _Title = title;
            _Authors = authors;
            _Publish_Date = publish_date;
            _Publishing = publishing;
            _Genre = genre;
            _Page_Count = page_count;
            _Price = price;
            _Markup = markup;
            _The_Rating_Of_Demand = the_rating_of_demand;
        }
        public string ShowInfo()
        {
            string str="Название: "+_Title+ "\n"+"Авторы: ";
            for(int i=0;i<_Authors.Count-1;i++)
            {
                str += _Authors[i] + ", ";
            }
            str += _Authors[_Authors.Count - 1] + "\n"+"Дата публикации: "+_Publish_Date+ "\n"+"Издательство: "+_Publishing+ "\n"+"Жанры: ";
            for (int i = 0; i < _Genre.Count - 1; i++)
            {
                str += _Genre[i] + ", ";
            }
            str += _Genre[_Genre.Count - 1] + "\n" + "Колличество страниц: " + _Page_Count + "\n" + "Цена: " + _Price + "\n" 
                + "Розничная цена: " + _Markup + "\n" + "Рейтинг спроса: " + _The_Rating_Of_Demand;
            return str;
        }
    }
}
