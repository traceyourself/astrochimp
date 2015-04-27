using System;
using AncestorCloud.Shared.ViewModels;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared
{
	public class MenuViewModel : BaseViewModel 
	{
		private FlyOutViewModel.Section section;
		public FlyOutViewModel.Section Section
		{
			get { return this.section; }
			set
			{
				this.section = value;
				//this.Id = (int)this.section; this.RaisePropertyChanged(() => this.Section);
			}
		}

		public Type ViewModelType;

		public void Close()
		{
			//this.Close (this);


		}
	}
}

