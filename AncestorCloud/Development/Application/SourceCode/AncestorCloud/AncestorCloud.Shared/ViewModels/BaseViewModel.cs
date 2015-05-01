using System;
using Cirrious.MvvmCross.ViewModels;

namespace AncestorCloud.Shared
{
	public class BaseViewModel : MvxViewModel
	{
		private string title = string.Empty;
    	/// <summary>
		/// Gets or sets the name of the menu
		/// </summary>
		public string Title
		{
			get { return this.title; }
			set { this.title = value; this.RaisePropertyChanged(() => this.Title); }
		
		}

		private string image = string.Empty;

		public string Image
		{
			get { return this.image; }
			set { this.image = value; this.RaisePropertyChanged(() => this.Image); }

		}
	}
}

