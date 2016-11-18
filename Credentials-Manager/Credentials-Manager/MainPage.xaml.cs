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
	public sealed partial class MainPage : Page
	{
		private static ApplicationDataContainer LocalState = ApplicationData.Current.LocalSettings;
		private ObservableCollection<UserDetails> UserDataList;
		private UserDetails ClickedItem;
		private int IndexOfSelectedItem;
		private string IndexOfCurrentEntry;
		private int TotalEntries;
		//private int TotalEntries = Convert.ToInt16(LocalState.Containers["User Details"].Values["Total Entries"]);

		public MainPage ()
		{
			this.InitializeComponent();
			this.Loaded += PasswordsPage_Loaded;

			//UserDataList = UserData.GetUserDetails();
			UserDataList = new ObservableCollection<UserDetails>();

			if (!(LocalState.Containers.ContainsKey("User Details")))
			{
				LocalState.CreateContainer("User Details", ApplicationDataCreateDisposition.Always);
				LocalState.Containers["User Details"].Values["Total Entries"] = 0;
			}

			IndexOfSelectedItem = -1;
			IndexOfCurrentEntry = (-1).ToString();
			TotalEntries = Convert.ToInt16(LocalState.Containers["User Details"].Values["Total Entries"]);
			FillList();
		}

		/// <summary>
		/// This method fills the list UserDataList with data if any exists
		/// </summary>
		private void FillList ()
		{
			int CurrentEntry = TotalEntries;

			if (CurrentEntry != 0)
			{
				while (CurrentEntry != 0)
				{
					ApplicationDataCompositeValue OldEntry = (ApplicationDataCompositeValue) LocalState.Containers["User Details"].Values[CurrentEntry.ToString()];
					if (OldEntry == null)
					{
						CurrentEntry--;
						continue;
					}
					UserDataList.Add(new UserDetails
					{
						Index = CurrentEntry,
						EmailAddress = (string) OldEntry["EmailAddress"],
						UserName = (string) OldEntry["UserName"],
						Password = (string) OldEntry["Password"],
						Website = (string) OldEntry["Website"],
						Notes = (string) OldEntry["Notes"]
					});
					CurrentEntry--;
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

			int NextEntry = AddToList();

			StoreToMemory(NextEntry);

			await ShowDialog();
		}

		/// <summary>
		/// This methos stores new data into secondary memory
		/// </summary>
		/// <param name="NextEntry">Next key name to be stored in memory</param>
		private void StoreToMemory (int NextEntry)
		{
			ApplicationDataCompositeValue NewData = new ApplicationDataCompositeValue();
			NewData["EmailAddress"] = EmailTextBox.Text;
			NewData["UserName"] = UserNameTextBox.Text;
			NewData["Password"] = PasswordTextBox.Text;
			NewData["Website"] = WebsiteTextBox.Text;
			NewData["Notes"] = NotesTextBox.Text;

			LocalState.Containers["User Details"].Values[NextEntry.ToString()] = NewData;

			LocalState.Containers["User Details"].Values["Total Entries"] = NextEntry;
		}

		/// <summary>
		/// This method adds new data entry into UserDataList
		/// </summary>
		/// <returns>Next key name to be stored in memory</returns>
		private int AddToList ()
		{
			int NextEntry = TotalEntries + 1;

			UserDataList.Add(new UserDetails
			{
				Index = NextEntry,
				EmailAddress = EmailTextBox.Text,
				UserName = UserNameTextBox.Text,
				Password = PasswordTextBox.Text,
				Website = WebsiteTextBox.Text,
				Notes = NotesTextBox.Text
			});
			return NextEntry;
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

		private void GridView_ItemClick (object sender, ItemClickEventArgs e)
		{
			ClickedItem = (UserDetails) e.ClickedItem;
			IndexOfSelectedItem = UserDataList.IndexOf(ClickedItem);
			IndexOfCurrentEntry = UserDataList.ElementAt(IndexOfSelectedItem).Index.ToString();
			//UserDetails ud = UserDataList.ElementAt(IndexOfSelectedItem);
			//MessageDialog MD = new MessageDialog("Clicked Item E-Mail Address.", ClickedItem.EmailAddress);
			//await MD.ShowAsync();
		}

		private void DeleteButton_Click (object sender, RoutedEventArgs e)
		{
			UserDataList.RemoveAt(IndexOfSelectedItem);
			LocalState.Containers["User Details"].Values[IndexOfCurrentEntry] = null;
		}

		private void EditButton_Click (object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(EditPage), IndexOfCurrentEntry);
		}
	}
}
