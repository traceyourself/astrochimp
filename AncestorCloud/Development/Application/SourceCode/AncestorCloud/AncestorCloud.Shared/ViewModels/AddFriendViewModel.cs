using System;
using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;

namespace AncestorCloud.Shared.ViewModels
{
	public class AddFriendViewModel:BaseViewModel
	{
		#region ViewModel Navigation Calls
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

		#region

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

		#region Commands

		private ACCommand _closeCommand;

		public ICommand CloseCommand
		{
			get
			{
				return this._closeCommand ?? (this._closeCommand = new ACCommand(this.Close));
			}
		}

		#endregion
	}
}

