using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Assign1CST209
{
    /// <summary>
    /// This class takes data from an Interactions object and creates a ViewCell for it to display on 
    /// InteractionsPage in a ListView.
    /// </summary>
    public class InteractionCell : ViewCell
    {
        public const int RowHeight = 75; //Set the row height for this viewcell
        /// <summary>
        /// Override the binding context to get access to the Interaction object. Create a 
        /// view that displays the customer name, comment, date, product name, and if the customer
        /// purchased the product.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            //if this InteractionCell still exists
            if ((Interactions)this.BindingContext != null)
            {
                //Get the interaction object from the binding context
                Interactions interaction = (Interactions)BindingContext;

                //Get the customer object from the interaction's customer ID
                Customers cust = App.Database.GetCustomer(interaction.CustomerID);
                Label lblName = new Label { FontAttributes = FontAttributes.Bold };
                //Display the customer's first and last name
                lblName.SetValue(Label.TextProperty, cust.FirstName + " " + cust.LastName);
                //Create label to display the date
                Label lblDate = new Label();
                //format the date to display full day, full month and day, year
                lblDate.SetBinding(Label.TextProperty, "Date", stringFormat: "{0:dddd, MMMM d, yyyy}");
                //Create a label to display the comment
                Label lblComments = new Label();
                lblComments.SetBinding(Label.TextProperty, "Comments");
                Label lblProduct = new Label();
                //Create a label to display the product name, getting the name from the database by using the product ID
                lblProduct.SetValue(Label.TextProperty, App.Database.GetProductName(interaction.ProductID));
                //Create a switch object to display whether the customer purchased the product
                Switch swtPurchased = new Switch();
                swtPurchased.SetBinding(Switch.IsToggledProperty, "Purchased");
                //Make sure users cant change the value of the switch
                swtPurchased.IsEnabled = false;
                Label lblPurchased = new Label { Text = "Purchased?" };

                //Create and format the stack layout, adding the objects above as children
                View = new StackLayout
                {
                    Spacing = 2,
                    Padding = 5,
                    Orientation = StackOrientation.Vertical,
                    Children = { lblName, new StackLayout{
                    Spacing = 150,
                    Orientation = StackOrientation.Horizontal,
                    Children = { lblDate, lblComments }
                }, new StackLayout{
                    Spacing = 230,
                    Orientation = StackOrientation.Horizontal,
                    Children = {lblProduct, new StackLayout{Orientation = StackOrientation.Horizontal, Children = {lblPurchased, swtPurchased} } }
                }
                }
                };

                //This will give users to swipe (touch and hold on android) to display a delete option, which they can then click to delete this viewcell and it's interaction
                MenuItem mi = new MenuItem { Text = "Delete", IsDestructive = true };
                mi.Clicked += (sender, e) =>
                {
                    //Delete the interaction from the database
                    App.Database.DeleteInteraction(interaction.ID);
                    //Get the listview from the parent
                    ListView parent = (ListView)this.Parent;
                    //Get the ItemsSource from the parent, which is an ObservableCollection
                    ObservableCollection<Interactions> list = (ObservableCollection<Interactions>)parent.ItemsSource;
                    //remove the object from the observable collection
                    list.Remove(this.BindingContext as Interactions);

                };
                ContextActions.Add(mi);
            }
        }
    }
}
