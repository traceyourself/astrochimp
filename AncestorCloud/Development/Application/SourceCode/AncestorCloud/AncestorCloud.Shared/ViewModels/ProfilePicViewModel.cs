using System;
using Cirrious.CrossCore;
using System.IO;

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
		public Stream ProfilePicStream{ get; set;}
		#endregion


		#region Upload image
		public void UploadImage()
		{
			Mvx.Trace ("Stream Length : "+ProfilePicStream.Length);
		}
		#endregion
	}
}

