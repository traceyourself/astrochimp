using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Json;

namespace AncestorCloud.Shared
{
	public class BaseViewModel : MvxViewModel
	{
		IDatabaseService _databaseService;

		public BaseViewModel()
		{
			_databaseService = Mvx.Resolve<IDatabaseService> ();
			Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
		}
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


		public void ClearDatabase()
		{
			//_databaseService.DropAllTables ();
		}
	}
}

