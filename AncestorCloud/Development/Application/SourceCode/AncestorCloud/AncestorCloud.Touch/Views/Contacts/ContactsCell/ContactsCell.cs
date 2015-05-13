using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public partial class ContactsCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("ContactsCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("ContactsCell");

		public ContactsCell (IntPtr handle) : base (handle)
		{
			this.DelayBind (() => {

				var set = this.CreateBindingSet<ContactsCell, People> ();
				set.Bind (NameLabel).To (vm => vm.FirstName);
				set.Apply ();
				//ContactImages.Layer.CornerRadius=22f;
				//ContactImages.ClipsToBounds=true;
			});
		}

		public static ContactsCell Create ()
		{
			return (ContactsCell)Nib.Instantiate (null, null) [0];
		}


	}
}
