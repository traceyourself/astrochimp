using System;
using Foundation;
using UIKit;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace AncestorCloud.Touch
{
	public partial class CelebritiesCell : MvxTableViewCell
	{

		public static readonly UINib Nib = UINib.FromName ("CelebritiesCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("CelebritiesCell");


		public CelebritiesCell (IntPtr handle) : base (handle)
		{
			this.DelayBind (() => {

				var set = this.CreateBindingSet<CelebritiesCell, Celebrity> ();
				set.Bind (NameLabel).To (vm => vm.GivenNames);
				set.Bind(LastName).To (vm => vm.LastName);

				//set.Bind(OtherNameLabel).To(vm => vm.Relation).WithConversion(new RelationshipTextConverter(),null);
				set.Apply ();
			});
		}

		public static CelebritiesCell Create ()
		{
			return (CelebritiesCell)Nib.Instantiate (null, null) [0];
		}
	}
}

