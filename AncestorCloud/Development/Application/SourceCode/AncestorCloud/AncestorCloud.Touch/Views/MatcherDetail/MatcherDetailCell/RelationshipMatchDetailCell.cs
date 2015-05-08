using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public partial class RelationshipMatchDetailCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("RelationshipMatchDetailCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("RelationshipMatchDetailCell");

		public RelationshipMatchDetailCell (IntPtr handle) : base (handle)
		{
			this.DelayBind (() => {

				var set = this.CreateBindingSet<RelationshipMatchDetailCell, RelationshipFindResult > ();
				set.Bind (NameLabel).To (vm => vm.CommonResult.Name);
				set.Bind (MatchDegreeLabel).To(vm => vm.Degrees);//.WithConversion(new DegreeConverter(),null);
				set.Apply ();
			});
		}

		public static RelationshipMatchDetailCell Create ()
		{
			return (RelationshipMatchDetailCell)Nib.Instantiate (null, null) [0];
		}
	}
}