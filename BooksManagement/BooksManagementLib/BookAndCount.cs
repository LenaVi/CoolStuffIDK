using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManagementLib
{
    public class BookAndCount
    {
        public Book book { get; }
        public int count { get; set; }
        public BookAndCount(Book _book,int _count)
        {
            book = _book;
            count = _count;
        }
    }
}
