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

//			if(EditButtonClicked!=null)
//			{
//				EditButtonClicked(sender);
//			}

			var messenger = Mvx.Resolve<IMvxMessenger> ();
			messenger.Publish (new MyTableCellTappedMessage (this,familyMember));
		}

		public void SetName()
		{	
			if (familyMember.Name == null)
				NameLabel.Text = familyMember.FirstName +" "+ familyMember.MiddleName +" "+ familyMember.LastName;
			else
				NameLabel.Text = familyMember.Name;

			this.DelayBind (() => {

				var set = this.CreateBindingSet<MyFamilyTableCell, People> ();
				set.Bind (BirthLabel).To (vm => vm.DateOfBirth);
				set.Bind(RelationLabel).To(vm => vm.Relation).WithConversion(new RelationshipTextConverter(familyMember.Gender),null);
				set.Apply ();
			});
		}
	}


}

