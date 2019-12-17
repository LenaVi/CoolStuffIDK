using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManagementLib
{
    public class Warehouse
    {
        public List<Book> Books;
        
        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            Books.Remove(book);
        }

        public void FindBook(IFinder find)
        {
            Books.FindAll(find);
        }
    }
}
