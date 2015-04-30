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
	}
}

