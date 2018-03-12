using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Assign1CST209
{
    /// <summary>
    /// This class will take a Customers object and create a ViewCell to display information about the
    /// customer in a ListView on the CustomersPage
    /// </summary>
    public class CustCell : ViewCell
    {
        public const int RowHeight = 55;//Set the row height of the cell

        /// <summary>
        /// This class will create labels and bind data from the Customers object to them
        /// </summary>
        public CustCell()
        {
            //Create a label for the first name
            Label lblFName = new Label { FontAttributes = FontAttributes.Bold };
            //Bind the value of the first name to the label
            lblFName.SetBinding(Label.TextProperty, "FirstName");
            //Create a label for the last name
            Label lblLName = new Label { FontAttributes = FontAttributes.Bold };
            //Bind the value of the last name to the label
            lblLName.SetBinding(Label.TextProperty, "LastName");
            //Create a label for the phone number
            Label lblPhone = new Label { FontAttributes = FontAttributes.Italic };
            //Bind the value of the phone number to the label
            lblPhone.SetBinding(Label.TextProperty, "Phone");

            //Set the view and format it, setting the labels above as the children
            View = new StackLayout
            {
                Spacing = 2,
                Padding = 5,
                Orientation = StackOrientation.Horizontal,
                Children = { lblFName, lblLName, lblPhone }
            };

            //This menuitem will allow users to swipe (tap and hold on Android) to display a delete option, which will delete the ViewCell and the database record
            MenuItem mi = new MenuItem { Text = "Delete", IsDestructive = true };
            mi.Clicked += (sender, e) =>
            {
                //Delete the customer from the database
                App.Database.DeleteCustomer(this.BindingContext as Customers);
                //Delete the ViewCell from the ObservableCollection
                CustomerPage.CustList.Remove(this.BindingContext as Customers);
            };
            ContextActions.Add(mi);
            
        }
        
    }
}
