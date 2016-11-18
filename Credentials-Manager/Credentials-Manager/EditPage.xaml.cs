using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Credentials_Manager
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class EditPage : Page
	{
		private static ApplicationDataContainer LocalState = ApplicationData.Current.LocalSettings;
		private static string IndexOfCurrentEntry;

		public EditPage ()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo (NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			IndexOfCurrentEntry = (string) e.Parameter;

			ApplicationDataCompositeValue EditEntry = (ApplicationDataCompositeValue) LocalState.Containers["User Details"].Values[IndexOfCurrentEntry];

			EmailTextBox.Text = (string) EditEntry["EmailAddress"];
			UserNameTextBox.Text = (string) EditEntry["UserName"];
			PasswordTextBox.Text = (string) EditEntry["Password"];
			WebsiteTextBox.Text = (string) EditEntry["Website"];
			NotesTextBox.Text = (string) EditEntry["Notes"];
		}

		private void SaveButton_Click (object sender, RoutedEventArgs e)
		{
			ApplicationDataCompositeValue EditEntry = new ApplicationDataCompositeValue();
			EditEntry["EmailAddress"] = EmailTextBox.Text;
			EditEntry["UserName"] = UserNameTextBox.Text;
			EditEntry["Password"] = PasswordTextBox.Text;
			EditEntry["Website"] = WebsiteTextBox.Text;
			EditEntry["Notes"] = NotesTextBox.Text;
			LocalState.Containers["User Details"].Values[IndexOfCurrentEntry] = EditEntry;
			Frame.Navigate(typeof(MainPage));
		}

		private void CancelButton_Click (object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainPage));
		}
	}
}
