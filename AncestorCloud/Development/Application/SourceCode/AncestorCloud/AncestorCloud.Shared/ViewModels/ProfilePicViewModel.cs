using System;

namespace AncestorCloud.Shared.ViewModels
{
	public class ProfilePicViewModel : BaseViewModel
	{

		public void ShowFamiyViewModel()
		{
			ShowViewModel<FlyOutViewModel> ();
		}
	}
}

