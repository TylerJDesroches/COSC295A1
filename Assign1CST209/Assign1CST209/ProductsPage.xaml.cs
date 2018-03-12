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
    /// The ProductsPage displays a information about all products in the
    /// database
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductsPage : ContentPage
	{
        /// <summary>
        /// Uses a ListView to display a list of ProductCells, which display information about a product's
        /// name, description, price, and the number of interactions they appear in. Users can only view the
        /// products, they cannot create, modify, or delete them.
        /// </summary>
		public ProductsPage ()
		{
            //Set the title
            Title = "Products";
            //Create a stack layout which will be used as the page's content
            StackLayout layout = new StackLayout { HorizontalOptions = LayoutOptions.CenterAndExpand };
            //Create a listview, using the database function GetAllProducts to get all products and set them to the ItemsSource
            //Uses the ProductCell as the ItemTemplate
            ListView listView = new ListView
            {
                ItemsSource = App.Database.GetAllProducts(),
                ItemTemplate = new DataTemplate(typeof(ProductCell)),
                RowHeight = ProductCell.RowHeight
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
            //Set the listview as the content
            Content = listView;
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