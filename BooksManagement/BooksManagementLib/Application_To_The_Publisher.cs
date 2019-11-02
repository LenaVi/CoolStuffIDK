using System.Collections.Generic;

namespace BooksManagementLib
{
    public class Publisher
    {
        public Dictionary<Book,int> Order { get; set; }
        public Publisher(Dictionary<Book, int> order)
        {
            Order = order;
        }
    }
}
