using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assign1CST209
{
    /// <summary>
    /// The InteractionsPage displays information about a user's interactions with a customer. It features
    /// a ListView of InteractionCells, allowing the user to create, delete, and edit interactions.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InteractionsPage : ContentPage
	{
        /// <summary>
        /// The InteractionsPage takes in a customer object, which is used to get interaction info and
        /// display it to the user. Users are able to create new interactions from a table, delete interactions
        /// by swiping (holding on Andriod) an interaction and pressing delete, and editing by tapping an
        /// existing interaction and modifying the populated data in the table.
        /// </summary>
        /// <param name="customer">The customer whose interactions will be displayed</param>
		public InteractionsPage (Customers customer)
		{
            //Set the title
            Title = "Interactions";
            //Create a stack layout which will be used as the page's content
            StackLayout layout = new StackLayout { HorizontalOptions = LayoutOptions.CenterAndExpand };
            //Get a list of all the interactions for the selected Customer
            List<Interactions> interactions = App.Database.GetAllInteractions(customer.ID);
            //Create an OberservableCollection to store all of the interactions in
            ObservableCollection<Interactions> interactionList = new ObservableCollection<Interactions>();
            //Loop through the interaction list and add them to the ObservableCollection
            foreach (Interactions interaction in interactions)
            {
                interactionList.Add(interaction);
            }
            //Create a listview, using the observable collection above as the ItemsSource and InteractionCell as the ItemTemplate
            ListView listView = new ListView
            {
                ItemsSource = interactionList,
                ItemTemplate = new DataTemplate(typeof(InteractionCell)),
                RowHeight = InteractionCell.RowHeight,
                HeightRequest = 1400
            };
            //Create the cells that will be used in the table for creating or updating interactions
            EntryCell eComment = new EntryCell { Label = "Comment:"};
            //Format the DatePicker to display "full day, full month #day, year"
            DatePicker dpDate = new DatePicker { Date = DateTime.Now, Format = "dddd, MMMM d, yyyy" };
            Label lblProduct = new Label { Text = "Product:" };
            //populate the picker by using the database function GetAllProducts and displaying the name of the products
            Picker pkrProduct = new Picker { ItemsSource = App.Database.GetAllProducts(), ItemDisplayBinding = new Binding("ProductName"), WidthRequest = 500  };
            SwitchCell swtPurchase = new SwitchCell { Text = "Purchased?" };
            //This button will be used to add a new interaction or update an existing one
            Button btnAdd = new Button { Text = "Add" };
            
            //Create a table to display the form for creating or updating interactions
            TableView table = new TableView
            {
                Intent = TableIntent.Form,
                VerticalOptions = LayoutOptions.End,
                Root = new TableRoot()
                {

                    new TableSection()
                    {
                        new ViewCell{View = dpDate},
                        eComment,
                        new ViewCell { View = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lblProduct, pkrProduct } } },
                        swtPurchase,
                        new ViewCell {View = new StackLayout { Children = {btnAdd}}}
                    }
                }
            };
            //When the add button is clicked
            btnAdd.Clicked += (sender, e) =>
            {
                //Make sure the user has filled in the comment and product
                if (eComment.Text == "" || pkrProduct.SelectedItem == null)
                {
                    //Notify the user
                    DisplayAlert("Invalid Entry", "Please make sure all fields are filled in", "Okay");
                }
                else
                {
                    //If the user has selected an item from the listview
                    if (listView.SelectedItem != null)
                    {
                        //update the properties of the selected interaction
                        ((Interactions)listView.SelectedItem).Date = Convert.ToDateTime(dpDate.Date);
                        ((Interactions)listView.SelectedItem).Comments = eComment.Text;
                        ((Interactions)listView.SelectedItem).ProductID = ((Products)pkrProduct.SelectedItem).ID;
                        ((Interactions)listView.SelectedItem).Purchased = swtPurchase.On;
                        //Call OnPropertyChanged on all properties to update information in the listview
                        ((Interactions)listView.SelectedItem).OnPropertyChanged("Date");
                        ((Interactions)listView.SelectedItem).OnPropertyChanged("Comments");
                        ((Interactions)listView.SelectedItem).OnPropertyChanged("ProductID");
                        ((Interactions)listView.SelectedItem).OnPropertyChanged("Purchased");
                        //set SelectedItem to null to deselect from the listview
                        listView.SelectedItem = null;

                    }
                    else //the user didn't select an interaction from the listview
                    {
                        //make a new interaction
                        Interactions newInteraction = new Interactions();
                        //Apply new data to interaction
                        newInteraction.Date = Convert.ToDateTime(dpDate.Date);
                        newInteraction.Comments = eComment.Text;
                        newInteraction.CustomerID = customer.ID;
                        newInteraction.ProductID = ((Products)pkrProduct.SelectedItem).ID;
                        newInteraction.Purchased = swtPurchase.On;
                        //Save interaction in database
                        App.Database.SaveInteraction(newInteraction);
                        //Add the interaction to the ObservableCollection
                        interactionList.Add(newInteraction);
                    }
                    //Reset all fields in the table to the default values
                    dpDate.Date = DateTime.Now;
                    eComment.Text = null;
                    pkrProduct.SelectedItem = null;
                    swtPurchase.On = false;
                }
            };
            //When the user selects an interaction from the listview
            listView.ItemSelected += (sender, e) =>
            {
                //make sure that an interaction is actually selected
                if (listView.SelectedItem != null)
                {
                    //populate the table below with information about the interaction that has been selected
                    Interactions selInteraction = (Interactions)e.SelectedItem;
                    dpDate.Date = selInteraction.Date;
                    eComment.Text = selInteraction.Comments;
                    pkrProduct.SelectedIndex = selInteraction.ProductID - 1;
                    swtPurchase.On = selInteraction.Purchased;
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

            layout.Children.Add(listView);
            layout.Children.Add(table);
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