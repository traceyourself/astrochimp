using System;
using Foundation;
using UIKit;
using AncestorCloud.Shared.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared;
using System.Collections.Generic;
using System.Drawing;
using CoreGraphics;
using Microsoft.Scripting.Utils;

namespace AncestorCloud.Touch
{
	public partial class AddFamilyView : BaseViewController
	{

		CGRect preFrame;
		
		public People FamilyMember{ get; set;}
		PickerModel picker_model;

		UIView PickerContainer;

		UIPickerView picker;


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

			CreatePicker ();

			BindSubViews ();

			GenderSegmentControlChanged (null);

			_RefenceSegmentControl (null);

			PickerButtonTapped.TouchUpInside += PickerButtonTappedEvent;

			base.OnKeyboardChanged += OnKeyboardChanged;

		}


		#endregion

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			preFrame = container.Frame;
		}


		#region Nav Bar Methods

		public void SetNavigationTitle()
		{
			this.Title = Utility.LocalisedBundle ().LocalizedString ("AddFamilyText", "");

			if ((ViewModel.AddType == Utility.LocalisedBundle ().LocalizedString ("GrandparentSectionFooter", "")) ||
				(ViewModel.AddType == Utility.LocalisedBundle ().LocalizedString ("GreatGrandparentSectionFooter", ""))) {
				RefernceLabel.Hidden = false;
				RefernceSegmentControl.Hidden = false;
			} else {
				RefernceLabel.Hidden = true;
				RefernceSegmentControl.Hidden = true;
			}

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
			//set.Bind (PickerButtonTapped).For( b => b.TitleLabel.Text).To(vm => vm.BirthDate);
			set.Bind(PickerLabel).To(vm => vm.BirthDate).TwoWay();
			//TODO: birth year
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
				ViewModel.Gender = Utility.LocalisedBundle().LocalizedString("MaleText","");
				break;
					
			case 1:
				ViewModel.Gender = Utility.LocalisedBundle().LocalizedString("FemaleText","");
				break;
			}
		}

		partial void _RefenceSegmentControl (NSObject sender)
		{
			switch(RefernceSegmentControl.SelectedSegment)
			{

			case 0:
				ViewModel.ReferenceType=AppConstant.Father_Reference;;
				break;

			case 1:
				ViewModel.ReferenceType=AppConstant.Mother_Reference;;
				break;
			}
		}


		partial void AddButtonTapped (NSObject sender)
		{
			
		}

		void PickerButtonTappedEvent (object sender, EventArgs e)
		{

			DataItem (sender);

			View.EndEditing (true);

		}
		#endregion

		public void DataItem(object sender)
		{
			//UIButton button = (UIButton)sender;
			ShowHidePicker(new RectangleF( 0f,(float)UIScreen.MainScreen.ApplicationFrame.Size.Height - 44-(float)PickerContainer.Frame.Height,(float) this.View.Frame.Size.Width,(float) picker.Frame.Size.Height + 20f));

		}

		void CreatePicker()
		{
			int currentYear = DateTime.Now.Year;

			List<Object> state_list = new List<Object> ();

			for (int i = 1900; i <= currentYear; i++) {

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


			PickerContainer = new  UIView(new RectangleF( 0,(float)UIScreen.MainScreen.ApplicationFrame.Size.Height - 44,(float) this.View.Frame.Size.Width,(float) picker.Frame.Size.Height + 20f));


			UIBarButtonItem doneButton = new UIBarButtonItem("Done",UIBarButtonItemStyle.Done, (s, e) =>
				{

					ShowHidePicker(new RectangleF( 0,(float)UIScreen.MainScreen.ApplicationFrame.Size.Height - 44,(float) this.View.Frame.Size.Width,(float) picker.Frame.Size.Height + 20f));


				});

			UIBarButtonItem flexibutton = new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace);

			toolbar.SetItems (new UIBarButtonItem[]{flexibutton,doneButton},true);

			PickerContainer.AddSubview (toolbar);

			PickerContainer.AddSubview (picker);

			PickerContainer.BackgroundColor = UIColor.White;

			this.View.AddSubview (PickerContainer);
		}


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

		void SetBirthButtonText(string title)
		{
//			pickerl.SetTitle (title, UIControlState.Normal);
			PickerLabel.Text= title;
			ViewModel.BirthDate = title;
		}

		#region KeyBoard Handler

		public virtual bool HandlesKeyboardNotifications
		{
			get { return true; }
		}

		public  void OnKeyboardChanged (object sender,OnKeyboardChangedArgs args)
		{

			UITextField current;
			if (FirstNameTextField.IsFirstResponder) {
				current = FirstNameTextField;
			} else if (MiddleNameTextFeild.IsFirstResponder) {
				current = MiddleNameTextFeild;
			} else if (LastNameTextField.IsFirstResponder) {
				current = LastNameTextField;
			} else {
				current = BirthLocationTextField;
			}


			var point = this.View.ConvertRectToView (current.Frame, this.View.Superview);

			var frame = container.Frame;

			if (frame.Size.Height  - args.Frame.Size.Height > point.Y + 50)
				return;

			if (args.visible)
				frame.Y -= point.Y + 50 - (frame.Size.Height - args.Frame.Size.Height);
			else
				frame = preFrame;

			container.Frame = frame;
		}
		#endregion

	}
}

