
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using AncestorCloud.Shared.ViewModels;
using AncestorCloud.Shared;
using System.Collections.Generic;
using System.Drawing;
using CoreGraphics;
using Microsoft.Scripting.Utils;

namespace AncestorCloud.Touch
{
	public partial class EditFamilyView : UIViewController
	{
		

		#region Globals

		public People FamilyMember{ get; set;}
		public Action<object> SaveButtonTappedClickedDelegate { get; set; }

	
		PickerModel picker_model;

		UIView PickerContainer;

		UIPickerView picker;

		#endregion

		#region View Life Cycle Methods
		public EditFamilyView () : base ("EditFamilyView", null)
		{
		}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			CreatePicker ();

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
			FirstNameTextField.Text = FamilyMember.FirstName;
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
			SetBirthButtonText (FamilyMember.DateOfBirth);

			int index = picker_model.values.FindIndex(f => f.ToString().Equals(FamilyMember.DateOfBirth) );

			if (index <= 0)
				index = 0;
			
			picker.Select(index,0,false);

		}

		void SetBirthButtonText(string title)
		{
			PickerButtonTapped.SetTitle (title, UIControlState.Normal);
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
			FamilyMember.DateOfBirth = PickerButtonTapped.TitleLabel.Text;
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
		 
		 void PickerButtonTappedEvent (object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine ("PickerButtonTapped");

			DataItem (sender);
	
		}
		#endregion


		public void DataItem(object sender)
		{
			UIButton button = (UIButton)sender;
			ShowHidePicker(new RectangleF( 0f,(float)this.View.Frame.Height-(float)PickerContainer.Frame.Height,(float) this.View.Frame.Size.Width,(float) picker.Frame.Size.Height + 20f));

		}

		#region Create Picker

		void CreatePicker()
		{
			
			List<Object> state_list = new List<Object> ();

			for (int i = 1900; i < 2015; i++) {

				state_list.Add (i);
			}

			picker_model = new PickerModel (state_list);
			picker_model.PickerChanged += PickerValueChanged;

			picker =  new UIPickerView ();
			picker.DataSource = picker_model;
			picker.Delegate = picker_model;
			picker.ShowSelectionIndicator = true;

			UIToolbar toolbar = new UIToolbar ();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit ();

			picker.Frame = new RectangleF (0f,(float)toolbar.Frame.Height + 1f,(float) this.View.Frame.Size.Width,(float) picker.Frame.Height);
		

			PickerContainer = new  UIView(new RectangleF( 0,(float)this.View.Frame.Height+1f,(float) this.View.Frame.Size.Width,(float) picker.Frame.Size.Height + 20f));


			UIBarButtonItem doneButton = new UIBarButtonItem("Done",UIBarButtonItemStyle.Done, (s, e) =>
				{
					
					ShowHidePicker(new RectangleF( 0,(float)this.View.Frame.Height+1f,(float) this.View.Frame.Size.Width,(float) picker.Frame.Size.Height + 20f));

						
				});
				
			UIBarButtonItem flexibutton = new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace);

			toolbar.SetItems (new UIBarButtonItem[]{flexibutton,doneButton},true);

			PickerContainer.AddSubview (toolbar);

			PickerContainer.AddSubview (picker);

			PickerContainer.BackgroundColor = UIColor.White;

			this.View.AddSubview (PickerContainer);
		}

		#endregion

		#region PickerDelegate Methdos

		void PickerValueChanged(object sender, PickerChangedEventArgs e)
		{
			SetBirthButtonText (e.SelectedValue.ToString ());
		}

		#endregion


		void ShowHidePicker(CGRect frame)
		{
			UIView.Animate (0.3,
				() => {
					PickerContainer.Frame = frame;

				});
		}


	}
}

