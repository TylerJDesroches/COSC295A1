using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace Assign1CST209
{
    /// <summary>
    /// The SalesDatabase is a SQLite database which contains information about customers, 
    /// products, and interactions.
    /// </summary>
    public class SalesDatabase
    {
        readonly SQLiteConnection database;
        /// <summary>
        /// This class contains many functions to help manage the database and it's entities.
        /// </summary>
        /// <param name="dbPath">The path to the database</param>
        public SalesDatabase(string dbPath)
        {
            //Create the database at the passed in path
            database = new SQLiteConnection(dbPath);
            //Create the tables in the database
            database.CreateTable<Customers>();
            database.CreateTable<Products>();
            database.CreateTable<Interactions>();
        }
        /// <summary>
        /// Saves a new customer object or updates an existing one
        /// </summary>
        /// <param name="item">the customer to save or update</param>
        /// <returns></returns>
        public int SaveCustomer(Customers item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }
        /// <summary>
        /// Saves a new product object or updates an existing one
        /// </summary>
        /// <param name="item">the product to save or update</param>
        /// <returns></returns>
        public int SaveProduct(Products item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }
        /// <summary>
        /// Saves a new interaction object or updates an existing one
        /// </summary>
        /// <param name="item">the interaction to save or update</param>
        /// <returns></returns>
        public int SaveInteraction(Interactions item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }
        /// <summary>
        /// Returns a list of all interactions for a specific customer
        /// </summary>
        /// <param name="custID">The customer whose interactions are being returned</param>
        /// <returns>A list of interactions for a specific customer</returns>
        public List<Interactions> GetAllInteractions(int custID)
        {
            //instantiate a new list
            List<Interactions> list = new List<Interactions>();
            //Make sure the ID is valid
            if(custID > 0)
            {
                //Query for each interaction associated to the custID and loop through them
                foreach (Interactions interaction in database.Query<Interactions>("SELECT * FROM [Interactions] WHERE CustomerID = " + custID))
                {
                    //add the interaction to the list
                    list.Add(interaction);
                }
            }
            //return the list
            return list;
        }
        /// <summary>
        /// Returns a count of interactions that the passed in product ID is associated to
        /// </summary>
        /// <param name="prodID">The product ID to search on</param>
        /// <returns>A count of interactions that the product is associated to</returns>
        public string GetInteractionCount(int prodID)
        {
            //Query the database, seaching for every interaction that contains the product ID, then return the count
            return Convert.ToString(database.Query<Interactions>("SELECT * FROM [Interactions] WHERE ProductID = " + prodID).Count);
        }
        /// <summary>
        /// Returns a list of all products in the database
        /// </summary>
        /// <returns>All products in the database</returns>
        public List<Products> GetAllProducts()
        {
            //Query the database for all products
            return database.Query<Products>("SELECT * FROM [Products]");
        }
        /// <summary>
        /// Deletes the passed in customer object
        /// </summary>
        /// <param name="item">The customer to delete</param>
        public void DeleteCustomer(Customers item)
        {
            //Make sure that the ID valid and that the customer exists
            if(item.ID != 0 && database.Get<Customers>(item.ID) != null)
            {
                //Delete all interactions associated to the customer
                foreach(Interactions interaction in GetAllInteractions(item.ID))
                {
                    database.Delete(interaction);
                }
                //Delete the customer
                database.Delete(item);
            }
        }
        /// <summary>
        /// Returns a list of all customers in the database
        /// </summary>
        /// <returns>A list of all customers</returns>
        public List<Customers> GetAllCustomers()
        {
            //Query the database for all customers and return the list
            return database.Query<Customers>("SELECT * FROM [Customers]");
        }

        /// <summary>
        /// Returns a single product name based on the passed in product ID
        /// </summary>
        /// <param name="prodID">The product ID to search on</param>
        /// <returns>A single product name</returns>
        public string GetProductName(int prodID)
        {
            //Query the database for a specific product and return it's name
            return database.Query<Products>("SELECT ProductName FROM [Products] WHERE ID = " + prodID)[0].ProductName.ToString();
        }

        /// <summary>
        /// Returns a single product object based on the passed in the product ID
        /// </summary>
        /// <param name="prodID">The product ID to search on</param>
        /// <returns>A single product object</returns>
        public Products GetProduct(int prodID)
        {
            //Query the database for a specific product and return it
            return database.Query<Products>("SELECT * FROM [Products] WHERE ID = " + prodID)[0];
        }
        /// <summary>
        /// Returns a single customer object based on the passed in customer ID
        /// </summary>
        /// <param name="custID">The customer ID to search on</param>
        /// <returns>A single customer object</returns>
        public Customers GetCustomer(int custID)
        {
            //Query the database for a single customer object and return it
            return database.Query<Customers>("SELECT * FROM [Customers] WHERE ID = " + custID)[0];
        }
        /// <summary>
        /// Delete a single interaction object based on the passed in interaction ID
        /// </summary>
        /// <param name="interID">The ID of the interaction object to delete</param>
        public void DeleteInteraction(int interID)
        {
            //If the ID is valid
            if (interID != 0)
            {
                //Get the interaction
                Interactions inter = database.Get<Interactions>(interID);
                //Make sure the interaction object isn't null
                if(inter != null)
                {
                    //Delete the interaction
                    database.Delete(inter);
                }
                
            }
        }
        /// <summary>
        /// Returns a single interaction object based on the passed in interaction ID
        /// </summary>
        /// <param name="interID">The interaction ID to search on</param>
        /// <returns>A single interaction object</returns>
        public Interactions GetInteraction(int interID)
        {
            //create a new interaction object
            Interactions inter = new Interactions();
            //if the ID is valid
            if(interID != 0)
            {
                //Get the interaction object
                inter = database.Get<Interactions>(interID);
            }
            //return the interaction object
            return inter;
            
        }
        /// <summary>
        /// Drops and creates all tables and inserts 3 new products.
        /// </summary>
        public void ResetData()
        {
            //Drop all tables
            database.DropTable<Customers>();
            database.DropTable<Interactions>();
            database.DropTable<Products>();
            //Create all tables
            database.CreateTable<Interactions>();
            database.CreateTable<Products>();
            database.CreateTable<Customers>();
            //Create 3 new product objects and save them
            SaveProduct(new Products { ProductName = "Wonder Jacket", Description = "A wonderful jacket", Price = 499.99 });
            SaveProduct(new Products { ProductName = "Wonder Hat", Description = "A wonderful hat", Price = 124.99 });
            SaveProduct(new Products { ProductName = "Wonder Boots", Description = "A wonderful pair of high quality boots", Price = 224.99 });
        }

        ///// <summary>
        ///// Used to fill the database with test data TESTING PURPOSES ONLY
        ///// </summary>
        //public void FillData()
        //{
        //    //drop all tables
        //    database.DropTable<Customers>();
        //    database.DropTable<Interactions>();
        //    database.DropTable<Products>();
        //    //create all tables
        //    database.CreateTable<Interactions>();
        //    database.CreateTable<Products>();
        //    database.CreateTable<Customers>();
        //    //create new test data and save it
        //    SaveCustomer(new Customers { FirstName = "Tyler", LastName = "Desroches", Address = "124-901 4th St S", Email = "tylerjdesroches@gmail.com", Phone = "(306)361-4181" });
        //    SaveCustomer(new Customers { FirstName = "Tyler", LastName = "Desroches", Address = "124-901 4th St S", Email = "tylerjdesroches@gmail.com", Phone = "(306)361-4181" });
        //    SaveProduct(new Products { ProductName = "Fancy Hat", Description = "The fanciest hat of all", Price = 420.69 });
        //    SaveInteraction(new Interactions { CustomerID = 1, Comments = "He liked the fancy hat", Date = new DateTime(2017,7,5), ProductID = 1, Purchased = true });
        //    SaveInteraction(new Interactions { CustomerID = 1, Comments = "He hated the fancy hat", Date = new DateTime(2017, 7, 5), ProductID = 1, Purchased = false });

        //}
    }
}
