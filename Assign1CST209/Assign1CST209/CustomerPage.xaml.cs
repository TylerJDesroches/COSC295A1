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
    /// The CustomerPage will display a ListView of CustCells, which contain information
    /// about the customer's name and phone number. Users have the option to create or delete
    /// customers, and they can view interaction information about the customer by tapping on their
    /// ViewCell. There is a toolbar which users can use to view a list of products or go to the
    /// settings page.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomerPage : ContentPage
	{
        //this private static list is used to display a list of all customers
        static ObservableCollection<Customers> custList;
        /// <summary>
        /// CustList is used to create a getter and setter for custList
        /// </summary>
        public static ObservableCollection<Customers> CustList
        {
            get
            {
                //If the custList isn't instantiated
                if (custList == null)
                {
                    //set it to be a new ObservableCollection of Customers
                    custList = new ObservableCollection<Customers>();
                }
                return custList;
            }
        }
        /// <summary>
        /// The CustomerPage displays a list of all customers. Users have the ability to
        /// create customer by using a "New Customer" button at the bottom of the page.
        /// They can also delete customers by swiping (or holding on Android) the Customer
        /// and tapping the delete option
        /// </summary>
        public CustomerPage ()
		{
            //instantiate the custList
            custList = CustList;
            //Set the default data (for testing)
            //App.Database.ResetData();
            //get a list of all customers from the database
            List<Customers> customers = App.Database.GetAllCustomers();
            //loop through each customer, adding them to the Observable Collection
            foreach (Customers customer in customers)
            {
                custList.Add(customer);
            }
            //Create a listview, using the Observable Collection as the ItemsSource and CustCell as the ItemTemplate
            ListView listView = new ListView
            {
                ItemsSource = custList,
                ItemTemplate = new DataTemplate(typeof(CustCell)),
                RowHeight = CustCell.RowHeight,
                HeightRequest = 100
            };
            //Create a button which will bring the user to a new page to create a new customer
            Button btnNew = new Button()
            {
                Text = "Add New Customer"
            };
            //Have the user go to a new page to fill out a form
            btnNew.Clicked += (sender, e) => { Navigation.PushAsync(new NewCustPage()); };
            //When the user clicks on a Customer, they are shown a page with all of that customer's interactions
            listView.ItemTapped += (sender, e) => { Navigation.PushAsync(new InteractionsPage((Customers)(e.Item))); };
            //Create a stacklayout and add the listview and button
            StackLayout layout = new StackLayout { HorizontalOptions = LayoutOptions.Center };
            layout.Children.Add(listView);
            layout.Children.Add(btnNew);

            //Add a Products tab to the toolbar for users to view all products
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Products",
                Command = new Command(ShowProductsPage)
            });
            //Add a Settings tab for users to view the settings
            ToolbarItems.Add(new ToolbarItem {
                Text = "Settings", Command = new Command(ShowSettingsPage)
            });
            
            //Set the title to Customers;
            Title = "Customers";
            //Set the content to layout
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