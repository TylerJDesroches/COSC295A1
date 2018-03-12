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
    /// The SettingsPage contains only one button, which resets the database to default data
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
        /// <summary>
        /// Contains a label and a button, used to reset the database to default data
        /// </summary>
		public SettingsPage ()
		{
            //Create a stack layout to display the label and button
            StackLayout layout = new StackLayout { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand};
            //Create a label to explain to the user what the button does
            Label lblReset = new Label { Text = "Click this button to remove all data and restore to factory settings" };
            //Create a button to delete and reset all data in the database
            Button btnReset = new Button { Text = "Delete" };
            //When the button is clicked
            btnReset.Clicked += (sender, e) =>
            {
                //Call the ResetData function in the database
                App.Database.ResetData();
            };
            layout.Children.Add(lblReset);
            layout.Children.Add(btnReset);
            //Clear the content on CustomerPage's custList
            CustomerPage.CustList.Clear();
            //Set the layout to be the content
            Content = layout;
		}
	}
}