
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using AncestorCloud.Shared.ViewModels;
using AncestorCloud.Shared;
using System.Collections.Generic;

namespace AncestorCloud.Touch
{
	public partial class EditFamilyView : UIViewController
	{
		#region Globals

		public People FamilyMember{ get; set;}
		public Action<object> SaveButtonTappedClickedDelegate { get; set; }

	
		PickerModel picker_model;

		UIPickerView picker;

		#endregion

		#region View Life Cycle Methods
		public EditFamilyView () : base ("EditFamilyView", null)
		{
		}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			BindSubview ();

			PickerButtonTapped.TouchUpInside += PickerButtonTappedEvent;
		
			// Perform any additional setup after loading the view, typically from a nib.
		}

		#endregion
	
		#region View Load Methods

		void BindSubview()
		{
			if (FamilyMember == null)
				return;

			SetEditLabel ();

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

		void SetEditLabel()
		{
			//For debugging purpose
			EditLabel.Text = FamilyMember.Name;
//			EditLabel.Text = FamilyMember.LastName;
//			EditLabel.Text = FamilyMember.Relation;
			//TODO: Uncomment this line when data is live
			//FirstNameTextField.Text = FamilyMember.FirstName;
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

		#region picker
		 
		async void PickerButtonTappedEvent (object sender, EventArgs e)
		{

			System.Diagnostics.Debug.WriteLine ("PickerButtonTapped");

			DataItem ();
	
		}
		#endregion


		public void DataItem()
		{
	

			List<Object> state_list= new List<Object> ();
			state_list.Add ("ACT");
			state_list.Add ("NSW");
			state_list.Add ("NT");
			state_list.Add ("QLD");
			state_list.Add ("SA");
			state_list.Add ("TAS");
			state_list.Add ("VIC");
			state_list.Add ("WA");
			picker_model = new PickerModel (state_list);

			picker =  new UIPickerView ();
			picker.Model = picker_model;
			picker.ShowSelectionIndicator = true;

			UIToolbar toolbar = new UIToolbar ();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit ();

			UIBarButtonItem doneButton = new UIBarButtonItem("Done",UIBarButtonItemStyle.Done,(s,e) =>
				{
					foreach (UIView view in this.View.Subviews) 
					{
						if (view.IsFirstResponder)
						{
							UITextField textview = (UITextField)view;
							//textview.Text = picker_model.values[picker.SelectedRowInComponent (0)].ToString ();
							textview.ResignFirstResponder ();
						}
					}

				});
			toolbar.SetItems (new UIBarButtonItem[]{doneButton},true);

			this.View.AddSubview (picker);

		



		}
	}
}

