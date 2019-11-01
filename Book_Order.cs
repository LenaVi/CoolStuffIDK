using System;
using Books;
using System.Collections.Generic;

namespace Book_Order
{
    public class Book_Order
    {
        public string Customer_Name { get; set;}
        public string Phone_Number { get; set; }
        public string Email { get; set; }
        public Dictionary<Book,int> Order { get; set; }
        public Book_Order(string customer_name, string phone_number, string email, Dictionary<Book,int> order)
        {
            Customer_Name = customer_name;
            Phone_Number = phone_number;
            Email = email;
            Order = order;
        }
    }
}
