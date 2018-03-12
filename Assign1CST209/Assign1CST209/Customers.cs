using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Assign1CST209
{
    /// <summary>
    /// The Customers objects will hold information about a customer's
    /// name, address, phone, and email
    /// </summary>
    public class Customers
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
