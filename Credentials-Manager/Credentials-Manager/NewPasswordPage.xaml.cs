using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.Storage;
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
	public sealed partial class NewPasswordPage : Page
	{
		public NewPasswordPage ()
		{
			this.InitializeComponent();
		}

		private void submitButton_Click (object sender, RoutedEventArgs e)
		{
			if (newPasswordBox.Password.Equals(confirmPasswordBox.Password))
			{
				ApplicationData.Current.RoamingSettings.Values["masterPassword"] = newPasswordBox.Password;
				Frame.Navigate(typeof(MainPage));
			}
		}
	}
}
