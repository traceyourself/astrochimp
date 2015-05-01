
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public partial class CelbsCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("CelbsCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("CelbsCell");

		public CelbsCell (IntPtr handle) : base (handle)
		{
			this.DelayBind (() => {

				var set = this.CreateBindingSet<CelbsCell, People> ();
				set.Bind (MyNameLabel).To (vm => vm.Name);
				set.Bind(OtherNameLabel).To(vm => vm.Relation).WithConversion(new RelationshipTextConverter(),null);
				set.Apply ();
			});
		}

		public static CelbsCell Create ()
		{
			return (CelbsCell)Nib.Instantiate (null, null) [0];
		}
	}
}


