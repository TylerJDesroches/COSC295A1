using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace Assign1CST209
{
    /// <summary>
    /// Interactions contain information about a user's interaction with a customer.
    /// This information includes the customer's ID, the date of the interaction, comments
    /// about the interaction, the product ID of the product that was discussed, and whether
    /// the customer purchased the product
    /// </summary>
    public class Interactions : INotifyPropertyChanged
    {
        /// <summary>
        /// This event is used to handle when a property for this object has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public int ProductID { get; set; }
        public bool Purchased { get; set; }

        /// <summary>
        /// OnPropertyChanged is called to see if a property has changed and display the new information
        /// </summary>
        /// <param name="property">the name of the property in as a string</param>
        public virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
