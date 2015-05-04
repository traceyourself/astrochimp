using System;
using Foundation;
using UIKit;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared.ViewModels;
using System.Drawing;

namespace AncestorCloud.Touch
{
	public partial class ContactsView : BaseViewController
	{
		public ContactsView () : base ("ContactsView", null)
		{
		}

		public new ContactsViewModel ViewModel
		{
			get { return base.ViewModel as ContactsViewModel; }
			set { base.ViewModel = value; }
		}


		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			SetTableView ();

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void SetTableView()
		{

			this.Title = "Contacts";
			var source = new ContactsTableSource (ContactsTableView);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			ContactsTableView.Source = source;

			//this.NavigationItem.TitleView = new MyPastMatchTitleView (this.Title,new RectangleF(0,0,150,20));

			var set = this.CreateBindingSet<ContactsView , ContactsViewModel> ();
			set.Bind (source).To (vm => vm.ContactsList);
			//set.Bind (NextButton).To (vm => vm.NextButtonCommand);
			set.Apply ();
			//this.NavigationController.NavigationBarHidden = true;

		}
	}
}

