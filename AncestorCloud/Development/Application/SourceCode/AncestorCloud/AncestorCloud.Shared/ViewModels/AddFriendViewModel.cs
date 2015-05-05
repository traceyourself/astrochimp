using System;
using Cirrious.MvvmCross.ViewModels;

namespace AncestorCloud.Shared.ViewModels
{
	public class AddFriendViewModel:BaseViewModel
	{

		public void ShowCelebrities()
		{
			ShowViewModel<CelebritiesViewModel> ();
		}

		public void  ShowFacebookFriend()
		{
			ShowViewModel<FacebookFriendViewModel> ();
		}

		public void ShowContacts()
		{
			ShowViewModel<ContactsViewModel> ();
		}


		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion
	}
}

