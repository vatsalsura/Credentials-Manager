using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Credentials_Manager
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class PasswordsPage : Page
	{
		public PasswordsPage ()
		{
			this.InitializeComponent();
			this.Loaded += PasswordsPage_Loaded;
		}

		private void PasswordsPage_Loaded (object sender, RoutedEventArgs e)
		{
			FormStack.Visibility = Visibility.Collapsed;
		}

		private void Add_Click (object sender, RoutedEventArgs e)
		{
			ResetFormValues();
			FormStack.Visibility = Visibility.Visible;
			DataStack.Visibility = Visibility.Collapsed;
		}

		private void ResetFormValues ()
		{
			EmailTextBox.Text = string.Empty;
			UserNameTextBox.Text = string.Empty;
			PasswordTextBox.Text = string.Empty;
			WebsiteTextBox.Text = string.Empty;
			NotesTextBox.Text = string.Empty;
		}

		private async void SaveButton_Click (object sender, RoutedEventArgs e)
		{
			FormStack.Visibility = Visibility.Collapsed;
			EmailData.Text = EmailTextBox.Text;
			UserNameData.Text = UserNameTextBox.Text;
			PasswordData.Text = PasswordTextBox.Text;
			WebsiteData.Text = WebsiteTextBox.Text;
			NotesData.Text = NotesTextBox.Text;
			DataStack.Visibility = Visibility.Visible;
			await ShowDialog();
		}

		private static async Task ShowDialog ()
		{
			var dialog = new MessageDialog("Form Saved.");
			await dialog.ShowAsync();
		}
	}
}
