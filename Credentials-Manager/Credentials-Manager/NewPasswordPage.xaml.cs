﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
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
		private static ApplicationDataContainer LocalState = ApplicationData.Current.LocalSettings;
		public NewPasswordPage ()
		{
			this.InitializeComponent();
		}

		private void submitButton_Click (object sender, RoutedEventArgs e)
		{
			//Check if both the passwords match or not
			if (NewPasswordBox.Password.Equals(ConfirmPasswordBox.Password))
			{
				LocalState.Containers["Master Password Details"].Values["Master Password"] = NewPasswordBox.Password;
				Frame.Navigate(typeof(LoginPage));
			}
		}
	}
}
