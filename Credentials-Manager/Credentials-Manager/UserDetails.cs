using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Credentials_Manager
{
	[DataContract()]
	class UserDetails
	{
		[DataMember]
		public string EmailAddress { get; set; }
		[DataMember]
		public string UserName { get; set; }
		[DataMember]
		public string Password { get; set; }
		[DataMember]
		public string Website { get; set; }
		[DataMember]
		public string Notes { get; set; }
	}

	//class UserData
	//{
	//	public static ObservableCollection<UserDetails> UserDetailsList = new ObservableCollection<UserDetails>();
	//	public static ObservableCollection<UserDetails> GetUserDetails ()
	//	{

	//		UserDetailsList.Add(new UserDetails
	//		{
	//			EmailAddress = Path.GetRandomFileName(),
	//			UserName = Path.GetRandomFileName().Substring(0, 5),
	//			Password = Path.GetRandomFileName(),
	//			Website = Path.GetRandomFileName(),
	//			Notes = Path.GetRandomFileName()
	//		});

	//		UserDetailsList.Add(new UserDetails
	//		{
	//			EmailAddress = Path.GetRandomFileName(),
	//			UserName = Path.GetRandomFileName().Substring(0, 5),
	//			Password = Path.GetRandomFileName(),
	//			Website = Path.GetRandomFileName(),
	//			Notes = Path.GetRandomFileName()
	//		});

	//		return UserDetailsList;
	//	}
	//}
}
