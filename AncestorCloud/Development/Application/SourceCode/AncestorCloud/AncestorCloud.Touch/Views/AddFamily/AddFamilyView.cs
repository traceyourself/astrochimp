
using System;

using Foundation;
using UIKit;
using AncestorCloud.Shared.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public partial class AddFamilyView : BaseViewController
	{
		UIDatePicker picker;


		#region View Life Cycle Methods

		public AddFamilyView () : base ("AddFamilyView", null)
		{
		}

		public new AddFamilyViewModel ViewModel
		{
			get { return base.ViewModel as AddFamilyViewModel; }
			set { base.ViewModel = value; }
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			SetNavigationTitle ();
			BindSubViews ();
			GenderSegmentControlChanged (null);
		
			// Perform any additional setup after loading the view, typically from a nib.
		}

		#endregion


		#region Nav Bar Methods

		public void SetNavigationTitle()
		{
			this.Title="Add Family";

		}

		#endregion

		#region View Binding Methods

		void BindSubViews()
		{
			var set = this.CreateBindingSet<AddFamilyView, AddFamilyViewModel > ();
			set.Bind (FirstNameTextField).To (vm => vm.FirstName);
			set.Bind (MiddleNameTextFeild).To (vm => vm.MiddleName);
			set.Bind (LastNameTextField).To (vm => vm.LastName);
			set.Bind (BirthLocationTextField).To (vm => vm.BirthLocation);
			//set.Bind (Birthlabel).To (vm => vm.FamilyMember.DateOfBirth);
			set.Bind (GenderSegmentControl).For(l => l.SelectedSegment).To (vm => vm.Gender).WithConversion(new GenderTextConverter(),null).TwoWay();
			set.Bind (AddButton).To (vm => vm.AddPersonCommand);
			set.Apply ();
		}

		#endregion


		#region

		partial void GenderSegmentControlChanged (NSObject sender)
		{
			switch(GenderSegmentControl.SelectedSegment)
			{

			case 0:
				ViewModel.Gender = "Male";
				break;
					
			case 1:
				ViewModel.Gender = "Female";
				break;
			}
		}

		partial void AddButtonTapped (NSObject sender)
		{
			
		}

		partial void BirthYearButtonTaped (NSObject sender)
		{
			
		}
		#endregion

	}
}

