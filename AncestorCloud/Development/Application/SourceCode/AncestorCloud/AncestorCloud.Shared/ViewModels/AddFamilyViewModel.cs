using System;
using Cirrious.MvvmCross.ViewModels;

namespace AncestorCloud.Shared.ViewModels
{
	public class AddFamilyViewModel:BaseViewModel
	{


		#region close call
		public void Close()
		{
			this.Close (this);
		}
		#endregion


	}
}

