using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assign1CST209
{
    /// <summary>
    /// The ProductCell is a ViewCell that will display information about a Product on the ProductsPage's ListView
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductCell : ViewCell
	{
        //Set the RowHeight of the cell
        public const int RowHeight = 75;
        /// <summary>
        /// Override the binding context to get access to the Product object. Create a 
        /// view that displays the product name, description, price, and interaction count.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            //Create a label for the product name
            Label lblName = new Label { FontAttributes = FontAttributes.Bold };
            //Bind the value of the product name to the label
            lblName.SetBinding(Label.TextProperty, "ProductName");
            //Create a label for the description
            Label lblDescription = new Label();
            //bind the value of the description to the label
            lblDescription.SetBinding(Label.TextProperty, "Description");
            //create a label to display the price
            Label lblPrice = new Label();
            //set the value of the price to the label, formatting it to display a "$" 
            lblPrice.SetBinding(Label.TextProperty, "Price", stringFormat: "{0:C2}");
            //Create a label to display the number of interactions that the product appears in, 
            //getting the number from the database function GetInteractionCount and passing in the ID of the binding context
            Label lblInterCount = new Label { Text = "#Interactions: " + App.Database.GetInteractionCount(((Products)this.BindingContext).ID) };

            //Create a stacklayout, setting the above labels as the children
            View = new StackLayout
            {
                Spacing = 2,
                Padding = 5,
                Orientation = StackOrientation.Vertical,
                Children = { lblName, new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = { lblDescription, lblPrice }
                    }, lblInterCount
                }
            };
        }
	}
}