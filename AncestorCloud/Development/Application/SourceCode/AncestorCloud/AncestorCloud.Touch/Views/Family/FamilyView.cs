
using System;
using Cirrious.MvvmCross.Touch.Views;
using Foundation;
using UIKit;
using FlyoutNavigation;
using MonoTouch.Dialog; 
using System.Linq;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Shared.ViewModels;
using AncestorCloud.Shared;
using System.Drawing;


namespace AncestorCloud.Touch
{

	public partial class FamilyView : BaseViewController
	{
 
		public FamilyView () : base ("FamilyView", null)
		{
			
		}
		public new FamilyViewModel ViewModel
		{
			get { return base.ViewModel as FamilyViewModel; }
			set { base.ViewModel = value; }
		}
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			App.controllerTypeRef = ControllerType.Primary;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			SetFamilyItem ();

			float constant = 0.88f;

			float width = (float)UIScreen.MainScreen.ApplicationFrame.Size.Width;

			if (width <= 320f)
				constant = 1.0f;

			if (width > 375f)
				constant = 0.80f;



			this.View.AddConstraint (NSLayoutConstraint.Create (this.ContentView, NSLayoutAttribute.Leading, 0, this.View, NSLayoutAttribute.Left, 1.0f, 0));

			this.View.AddConstraint (NSLayoutConstraint.Create (this.ContentView, NSLayoutAttribute.Trailing , 0, this.View, NSLayoutAttribute.Right, constant, 0));

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void SetFamilyItem()
		{
			
			this.Title = Utility.LocalisedBundle ().LocalizedString ("MyFamilyText", "");
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = Themes.TitleTextColor()});
			this.NavigationController.NavigationBar.BarTintColor = Themes.NavBarTintColor();
			this.NavigationItem.SetHidesBackButton (true, false);
			//this.NavigationItem.TitleView = new MyTitleView (this.Title,new RectangleF(0,0,150,20));

			float width = (float)UIScreen.MainScreen.ApplicationFrame.Size.Width;
		

			if (width <= 320f) {
				this.NavigationItem.TitleView = new MyTitleView (this.Title, new RectangleF (0, 0, 100, 20));

			} else if (width >= 321f && width <=375) {
				this.NavigationItem.TitleView = new MyTitleView (this.Title, new RectangleF (0, 0, 150, 20));
			} else {
				this.NavigationItem.TitleView = new MyTitleView (this.Title, new RectangleF (0, 0, 180, 20));
			}



			//view.Center = new CoreGraphics.CGPoint (this.View.Center.X, view.Center.Y);
	

			UIImage image = UIImage.FromFile (StringConstants.FLYOUTICON);

			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);


			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						{
							//message to show the menu
							var messenger = Mvx.Resolve<IMvxMessenger>();
							messenger.Publish(new ToggleFlyoutMenuMessage(this));
						}

					})
				, true);
			//this.NavigationController.NavigationBarHidden = true;

		}

		#region MatchTapped
		partial void AddFamilyButtonTapped (NSObject sender)
		{

			ViewModel.ShowEditViewModel();
			ViewModel.Close();
		}

		#endregion

		#region  HelpButton
		partial  void HelpButtonTaped (NSObject sender)
		{

			//Console.WriteLine("Button Tapped");
			ViewModel.ShowHelpViewModel();
			ViewModel.Close();

		}
		#endregion

	}
}

