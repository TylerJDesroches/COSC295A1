using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Assign1CST209
{
	public partial class App : Application
	{
        //Create a private static database reference, which will be accessed using the public static Database
        static SalesDatabase database;
        /// <summary>
        /// Creates getters and setters for the private database
        /// </summary>
        public static SalesDatabase Database
        {
            get
            {
                //If the database hasn't been instantiated
                if (database == null)
                {
                    //set the database to be equal to the path
                    database = new SalesDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("SalesSQLite.db3"));
                }
                return database;
            }
        }

        
        public App ()
		{
			InitializeComponent();
            //Instantiate the database
            database = Database;
            //Set the CustomerPage as the MainPage
            MainPage = new NavigationPage(new CustomerPage());
		}



        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}

    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
