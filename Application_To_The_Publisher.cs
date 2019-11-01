using System;
using Books;
using System.Collections.Generic;

namespace Application_To_The_Publisher
{
    public class Application_To_The_Publisher
    {
        public Dictionary<Book,int> Order { get; set; }
        public Application_To_The_Publisher(Dictionary<Book, int> order)
        {
            Order = order;
        }
    }
}
