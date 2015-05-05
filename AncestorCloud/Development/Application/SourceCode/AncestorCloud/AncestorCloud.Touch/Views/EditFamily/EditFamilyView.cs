
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using AncestorCloud.Shared.ViewModels;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public partial class EditFamilyView : UIViewController
	{
		#region Globals

		public People FamilyMember{ get; set;}
		public Action<object> SaveButtonTappedClickedDelegate { get; set; }
		#endregion

		#region View Life Cycle Methods
		public EditFamilyView () : base ("EditFamilyView", null)
		{
		}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			BindSubview ();
		
			// Perform any additional setup after loading the view, typically from a nib.
		}

		#endregion
	
		#region View Load Methods

		void BindSubview()
		{
			if (FamilyMember == null)
				return;

			SetFirstName ();

			SetLastName ();

			SetMiddleName ();

			SetBirthLocation ();

			SetBirthYear ();

			SetGender ();
		}

		void SetGender()
		{
			if (FamilyMember.Gender == null)
				return;

			if (FamilyMember.Gender.Equals ("male") || FamilyMember.Gender.Equals ("Male")) 
			{
				GenderSegment.SelectedSegment = 0;
			}
			else 
			{
				GenderSegment.SelectedSegment = 1;
			}
		}
			
		void SetFirstName()
		{
			//For debugging purpose
			FirstNameTextField.Text = FamilyMember.Name;
			//TODO: Uncomment this line when data is live
			//FirstNameTextField.Text = FamilyMember.FirstName;
		}

		void SetLastName()
		{
			LastNameTextField.Text = FamilyMember.LastName;
		}

		void SetMiddleName()
		{
			MiddleNameTextField.Text = FamilyMember.MiddleName;
		}

		void SetBirthLocation()
		{
			BirthLocationField.Text = FamilyMember.BirthLocation;
		}

		void SetBirthYear()
		{
			
		}

		#endregion

		#region Button Tap Handlers

		partial void CrossButtonTapped (NSObject sender)
		{
			this.View.RemoveFromSuperview();
		}

		partial void SaveButtonTapped (NSObject sender)
		{
			//TODO: Hit a edit relationship service
			if(SaveButtonTappedClickedDelegate !=null)
			{   
				GetFamilyMemberData();
				
				SaveButtonTappedClickedDelegate(FamilyMember);
			}

			this.View.RemoveFromSuperview();
		}

		#endregion

		#region

		void GetFamilyMemberData()
		{
			FamilyMember.FirstName = FirstNameTextField.Text;
			FamilyMember.LastName = LastNameTextField.Text;
			FamilyMember.MiddleName = MiddleNameTextField.Text;
			FamilyMember.BirthLocation = BirthLocationField.Text;
			FamilyMember.Gender = GetGender ();
			//TODO: Get birthyear
		}

		private string GetGender ()
		{
			String gender= String.Empty;

			switch(GenderSegment.SelectedSegment)
			{

			case 0:
				gender = "Male";
				break;

			case 1:
				gender = "Female";
				break;
			}

			return gender;
		}

		#endregion
	}
}

