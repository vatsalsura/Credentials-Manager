using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Security;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;
using System.Collections.ObjectModel;
using Windows.UI.Notifications;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Credentials_Manager
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class PasswordsPage : Page
	{
		private ObservableCollection<UserDetails> UserDataList;

		public PasswordsPage ()
		{
			this.InitializeComponent();
			this.Loaded += PasswordsPage_Loaded;

			//UserDataList = UserData.GetUserDetails();
			UserDataList = new ObservableCollection<UserDetails>();

			ApplicationDataContainer LocalState = ApplicationData.Current.LocalSettings;
			if (!(LocalState.Containers.ContainsKey("User Details")))
			{
				LocalState.CreateContainer("User Details", ApplicationDataCreateDisposition.Always);
				LocalState.Containers["User Details"].Values["Total Entries"] = 0;
			}

			int TotalEntries = Convert.ToInt16(LocalState.Containers["User Details"].Values["Total Entries"]);

			if (TotalEntries != 0)
			{
				while (TotalEntries != 0)
				{
					ApplicationDataCompositeValue OldEntry = (ApplicationDataCompositeValue) LocalState.Containers["User Details"].Values[TotalEntries.ToString()];
					UserDataList.Add(new UserDetails
					{
						EmailAddress = (string) OldEntry["EmailAddress"],
						UserName = (string) OldEntry["UserName"],
						Password = (string) OldEntry["Password"],
						Website = (string) OldEntry["Website"],
						Notes = (string) OldEntry["Notes"]
					});
					TotalEntries--;
				}
			}
		}

		private void PasswordsPage_Loaded (object sender, RoutedEventArgs e)
		{
			//FormStack.Visibility = Visibility.Collapsed;
		}

		private void AddNewItem_Click (object sender, RoutedEventArgs e)
		{
			ResetFormValues();
			FormStack.Visibility = Visibility.Visible;
		}

		private async void SaveButton_Click (object sender, RoutedEventArgs e)
		{
			FormStack.Visibility = Visibility.Collapsed;

			ApplicationDataContainer LocalState = ApplicationData.Current.LocalSettings;

			UserDataList.Add(new UserDetails
			{
				EmailAddress = EmailTextBox.Text,
				UserName = UserNameTextBox.Text,
				Password = PasswordTextBox.Text,
				Website = WebsiteTextBox.Text,
				Notes = NotesTextBox.Text
			});

			ApplicationDataCompositeValue NewData = new ApplicationDataCompositeValue();
			NewData["EmailAddress"] = EmailTextBox.Text;
			NewData["UserName"] = UserNameTextBox.Text;
			NewData["Password"] = PasswordTextBox.Text;
			NewData["Website"] = WebsiteTextBox.Text;
			NewData["Notes"] = NotesTextBox.Text;

			int TotalEntries = Convert.ToInt16(LocalState.Containers["User Details"].Values["Total Entries"]) + 1;

			LocalState.Containers["User Details"].Values[TotalEntries.ToString()] = NewData;

			LocalState.Containers["User Details"].Values["Total Entries"] = TotalEntries++;

			await ShowDialog();
		}

		private void ResetFormValues ()
		{
			EmailTextBox.Text = string.Empty;
			UserNameTextBox.Text = string.Empty;
			PasswordTextBox.Text = string.Empty;
			WebsiteTextBox.Text = string.Empty;
			NotesTextBox.Text = string.Empty;
		}

		private static async Task ShowDialog ()
		{
			var dialog = new MessageDialog("Form Saved.", "Successfull.");
			await dialog.ShowAsync();
		}

		private async void GridView_ItemClick (object sender, ItemClickEventArgs e)
		{
			var UserDataList = (UserDetails) e.ClickedItem;
			MessageDialog MD = new MessageDialog("Clicked Item E-Mail Address.", UserDataList.EmailAddress);
			await MD.ShowAsync();
		}
	}
}
