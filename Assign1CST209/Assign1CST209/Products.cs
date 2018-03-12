using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Assign1CST209
{
    /// <summary>
    /// The Products object contains information about a product's name, description and price
    /// </summary>
    public class Products
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
