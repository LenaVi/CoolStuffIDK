using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksManagementLib
{
    public class ListBook<T> : IEnumerable<T> where T : BookAndCount
    {
        private List<T> listBook = new List<T>();

        public T GetBook(int pos)
        {
            return listBook[pos];
        }

        public void AddBook(T c)
        {
            listBook.Add(c);
        }

        public void RemoveBookByIndex(int i)
        {
            listBook.RemoveAt(i);
        }

        public void ClearList()
        {
            listBook.Clear();
        }

        public void ReverseList()
        {
            listBook.Reverse();
        }

        public bool ContainsBook(BookAndCount b)
        {
            return listBook.Contains(b);
        }

        public void AddRangeBook(List<T> items)
        {
            listBook.AddRange(items);
        }

        public int Count
        {
            get
            {
                return listBook.Count;
            }
        }

        public void SortByDatePublish()
        {
            listBook.Sort(new Comparison<BookAndCount>((a, b) => a.book.PublishDate.CompareTo(b.book.PublishDate)));
        }

        public void SortByPageCount()
        {
            listBook.Sort(new Comparison<BookAndCount>((a, b) => a.book.PageCount.CompareTo(b.book.PageCount)));
        }

        public void SortByPrice()
        {
            listBook.Sort(new Comparison<BookAndCount>((a, b) => a.book.Price.CompareTo(b.book.Price)));
        }

        public void SortByRating()
        {
            listBook.Sort(new Comparison<BookAndCount>((a, b) => a.book.Rating.CompareTo(b.book.Rating)));
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return listBook.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return listBook.GetEnumerator();
        }

        public ListBook<T> FindAllAuthor(string author)
        {
            ListBook<T> items = new ListBook<T>();
            var listitems = listBook.FindAll(item => item.book.Authors.Contains(author));
            items.AddRangeBook(listitems);
            return items;
        }

        public ListBook<T> FindAllGenre(string genre)
        {
            ListBook<T> items = new ListBook<T>();
            var listitems = listBook.FindAll(item => item.book.Genre.Contains(genre));
            items.AddRangeBook(listitems);
            return items;
        }

        public ListBook<T> FindAllDate(int year1, int year2)
        {
            ListBook<T> items = new ListBook<T>();
            var listitems = listBook.FindAll(item => item.book.PublishDate.Year >= year1 && item.book.PublishDate.Year <= year2);
            items.AddRangeBook(listitems);
            return items;
        }

        public ListBook<T> FindAllPublishing(string publish)
        {
            ListBook<T> items = new ListBook<T>();
            var listitems = listBook.FindAll(item => item.book.Publisher == publish);
            items.AddRangeBook(listitems);
            return items;
        }

        public ListBook<T> FindAllPrice(int price1, int price2)
        {
            ListBook<T> items = new ListBook<T>();
            var listitems = listBook.FindAll(item => item.book.Price >= price1 && item.book.Price <= price2);
            items.AddRangeBook(listitems);
            return items;
        }

        public ListBook<T> FindAllPage(int page1, int page2)
        {
            ListBook<T> items = new ListBook<T>();
            var listitems = listBook.FindAll(item => item.book.PageCount >= page1 && item.book.PageCount <= page2);
            items.AddRangeBook(listitems);
            return items;
        }

        public StringBuilder PrintBook(BookAndCount it_book)
        {
            StringBuilder str = new StringBuilder();
            str.Append(it_book.book.ShowInfo());
            str.Append(string.Format("\n" + "Количество экземпляров: " + it_book.count));
            return str;
        }
    }
}
