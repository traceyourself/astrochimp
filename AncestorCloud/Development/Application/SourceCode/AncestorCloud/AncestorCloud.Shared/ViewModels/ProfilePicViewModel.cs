using System;
using Cirrious.CrossCore;

namespace AncestorCloud.Shared.ViewModels
{
	public class ProfilePicViewModel : BaseViewModel
	{

		public void ShowFamiyViewModel()
		{
			ShowViewModel<FlyOutViewModel> ();
		}



		#region close 
		public void Close()
		{
			this.Close (this);			
		}
		#endregion



		#region ProfilePic Path
		public string ProfilePicPath{ get; set;}
		#endregion


		#region Upload image
		public void UploadImage()
		{
			Mvx.Trace (""+ProfilePicPath);
		}
		#endregion
	}
}

