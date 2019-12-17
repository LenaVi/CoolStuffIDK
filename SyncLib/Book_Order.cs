using System.Collections.Generic;

namespace BooksManagementLib
{
    public class BookOrder
    {
        public string CustomerName { get; set;}
        public string CustomerPhone { get; set; }
        public string Email { get; set; }
        public Dictionary<Book,int> Order { get; set; }
        public BookOrder(string customer_name, string phone_number, string email, Dictionary<Book,int> order)
        {
            CustomerName = customer_name;
            CustomerPhone = phone_number;
            Email = email;
            Order = order;
        }
    }
}
