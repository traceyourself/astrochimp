using System;
using Foundation;
using UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public partial class MyFamilyTableCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("MyFamilyTableCell", NSBundle.MainBundle);

		public static readonly NSString Key = new NSString ("MyFamilyTableCell");

		//readonly string[] tableItems;

		public Action<object> EditButtonClicked
		{ get; set; }

		public People familyMember{ get; set;}

//		public MyFamilyTableCell (IntPtr handle) : base (handle)
//		{
//		}

		public MyFamilyTableCell (IntPtr handle) : base (handle)
		{
			this.DelayBind (() => {

				var set = this.CreateBindingSet<MyFamilyTableCell, People> ();
				set.Bind (NameLabel).To (vm => vm.Name);
				//set.Bind(RelationLabel).To(vm => vm.Relation).WithConversion(new RelationshipTextConverter(),null);
				set.Apply ();
			});
		}
		public MyFamilyTableCell (string[] items) 
		{
			//tableItems = items;

		}



		public static MyFamilyTableCell Create ()
		{
			return (MyFamilyTableCell)Nib.Instantiate (null, null) [0];
		}

		partial void EditButtonTapped(NSObject sender)
		{
			System.Diagnostics.Debug.WriteLine ("Button Tapped");

//			if(EditButtonClicked!=null)
//			{
//				EditButtonClicked(sender);
//			}

			var messenger = Mvx.Resolve<IMvxMessenger> ();
			messenger.Publish (new MyTableCellTappedMessage (this,familyMember));
		}
	}


}

