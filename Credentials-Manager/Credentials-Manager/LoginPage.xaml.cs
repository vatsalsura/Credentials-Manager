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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Credentials_Manager
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class LoginPage : Page
	{
		private static ApplicationDataContainer LocalState = ApplicationData.Current.LocalSettings;

		public LoginPage ()
		{
			this.InitializeComponent();
		}

		private void loginButton_Click (object sender, RoutedEventArgs e)
		{
			//Check if the entered password matches the master password
			if (LocalState.Containers["Master Password Details"].Values["Master Password"].Equals(PasswordTextBox.Password))
			{
				Frame.Navigate(typeof(MainPage));
			}
		}
	}
}
