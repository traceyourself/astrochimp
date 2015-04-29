
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public partial class FbFamilyCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("FbFamilyCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("FbFamilyCell");

		public FbFamilyCell (IntPtr handle) : base (handle)
		{
			this.DelayBind (() => {

				var set = this.CreateBindingSet<FbFamilyCell, People> ();
				set.Bind (NameLabel).To (vm => vm.Name);
				set.Bind(RelationLabel).To(vm => vm.Relation).WithConversion(new RelationshipTextConverter(),null);
				set.Apply ();
			});
		}

		public static FbFamilyCell Create ()
		{
			return (FbFamilyCell)Nib.Instantiate (null, null) [0];
		}
	}
}

