using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assign1CST209
{
    /// <summary>
    /// The NewCustPage will display a table for users to fill in and save a new Customer to
    /// the database. Users will be required to fill in all fields.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewCustPage : ContentPage
	{
        /// <summary>
        /// The NewCustPage allows users to fill in information about a customer's name,
        /// address, phone, and email. Users will be required to fill in all fields. Once
        /// a user submits a new customer, the custList on the CustomerPage is automatically
        /// updated with the new customer.
        /// </summary>
		public NewCustPage ()
		{
            //Set the title
            Title = "New Customer";
            //Create EntryCells for each field for the user to fill in
            EntryCell eFName = new EntryCell { Label = "First Name" };
            EntryCell eLName = new EntryCell { Label = "Last Name" };
            EntryCell eAddress = new EntryCell { Label = "Address" };
            EntryCell ePhone = new EntryCell { Label = "Phone" };
            EntryCell eEmail = new EntryCell { Label = "Email" };
            //Create a table which will display the EntryCells
            TableView table = new TableView()
            {
                Intent = TableIntent.Form,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Root = new TableRoot()
                {
                    //This TableSection will have the title "Add New Customer"
                    new TableSection("Add New Customer")
                    {
                        
                        eFName,
                        eLName,
                        eAddress,
                        ePhone,
                        eEmail
                    }
                }
            };
            //Create a save button for users to save the new customer to the database and update the list
            Button btnSave = new Button { Text = "Save" };
            //When the save button is clicked
            btnSave.Clicked += (sender, e) =>
            {
                //Check to make sure that every field has been filled in and contains at least one character
                if(eFName.Text != null && eLName.Text != null && eAddress.Text != null && ePhone.Text != null && eEmail.Text != null &&
                    eFName.Text.Trim() != "" && eLName.Text.Trim() != "" && eAddress.Text.Trim() != "" && ePhone.Text.Trim() != "" && eEmail.Text.Trim() != "")
                {
                    //Create a new customer object
                    Customers cust = new Customers();
                    //Set all the properties from the table
                    cust.FirstName = eFName.Text;
                    cust.LastName = eLName.Text;
                    cust.Address = eAddress.Text;
                    cust.Phone = ePhone.Text;
                    cust.Email = eEmail.Text;
                    //Save the new customer to the database
                    App.Database.SaveCustomer(cust);
                    //add the customer to the CustomerPage's custList
                    CustomerPage.CustList.Add(cust);
                    //Blank out all fields
                    eFName.Text = null;
                    eLName.Text = null;
                    eAddress.Text = null;
                    ePhone.Text = null;
                    eEmail.Text = null;
                }
                else //If the user did not properly fill out all fields
                {
                    //Display an alert
                    DisplayAlert("Invalid Data", "Please fill in all fields", "Okay");
                }
                
            };

            //Add a Products tab to the toolbar for users to view all products
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Products",
                Command = new Command(ShowProductsPage)
            });
            //Add a Settings tab for users to view the settings
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Settings",
                Command = new Command(ShowSettingsPage)
            });

            StackLayout layout = new StackLayout();
            layout.Children.Add(table);
            layout.Children.Add(btnSave);
            Content = layout;
		}

        /// <summary>
        /// Runs a command which pushes a new settings page
        /// </summary>
        private void ShowSettingsPage()
        {
            Navigation.PushAsync(new SettingsPage());
        }

        /// <summary>
        /// Runs a command which pushes a new products page
        /// </summary>
        private void ShowProductsPage()
        {
            Navigation.PushAsync(new ProductsPage());
        }
    }

}