using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using System.Drawing;

namespace AncestorCloud.Touch
{
	public partial class BaseViewController : MvxViewController
	{
		public BaseViewController (String nibName, NSBundle bundle) : base (nibName, null)
		{
			
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			try{
				base.ViewDidLoad ();
			}
			catch (Exception e) {

				System.Diagnostics.Debug.WriteLine (e.InnerException);
			}

			SetExtentedLayout();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		#region Set Extended Layout Method
		 
		private void SetExtentedLayout()
		{
			this.EdgesForExtendedLayout = UIRectEdge.None;
		}

		#endregion


		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			NSNotificationCenter.DefaultCenter.RemoveObserver(UIKeyboard.WillHideNotification);
			NSNotificationCenter.DefaultCenter.RemoveObserver(UIKeyboard.WillShowNotification);
		}



         #region  Show/Hide KeyBoard

		protected UIView ViewToCenterOnKeyboardShown;

		public virtual bool HandlesKeyboardNotifications()
		{
			return false;
		}


		private void OnKeyboardNotification (NSNotification notification)
		{
			if (IsViewLoaded) {

				//Check if the keyboard is becoming visible
				bool visible = notification.Name == UIKeyboard.WillShowNotification;

				//Start an animation, using values from the keyboard
				UIView.BeginAnimations ("AnimateForKeyboard");
				UIView.SetAnimationBeginsFromCurrentState (true);
				UIView.SetAnimationDuration (UIKeyboard.AnimationDurationFromNotification (notification));
				UIView.SetAnimationCurve ((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification (notification));

				//Pass the notification, calculating keyboard height, etc.
				bool landscape = InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight;
				if (visible) {
					var keyboardFrame = UIKeyboard.FrameEndFromNotification (notification);

					OnKeyboardChanged (visible, landscape ? keyboardFrame.Width : keyboardFrame.Height);
				} else {
					var keyboardFrame = UIKeyboard.FrameBeginFromNotification (notification);

					OnKeyboardChanged (visible, landscape ? keyboardFrame.Width : keyboardFrame.Height);
				}

				//Commit the animation
				UIView.CommitAnimations ();	
			}


		}

		public void OnKeyboardChanged(bool visible, nfloat height)
		{
			
		}
		#endregion


	}
}

