
using System;

using Foundation;
using UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Drawing;

namespace AncestorCloud.Touch
{
	public partial class ResearchHelpView : BaseViewController
	{
		public ResearchHelpView () : base ("ResearchHelpView", null)
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
			base.ViewDidLoad ();
			SetNavigationbar ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void SetNavigationbar()
		{

			this.Title = "Research Help";

			this.NavigationController.NavigationBar.TintColor=UIColor.FromRGB(255,255,255);
		    this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (178, 45, 116);

			float width = (float)UIScreen.MainScreen.ApplicationFrame.Size.Width;


			if (width <= 320f) {
				this.NavigationItem.TitleView = new MyResearchTitleView (this.Title,new RectangleF(0,0,140,20));

			} else if (width >= 321f && width <=375) {
				this.NavigationItem.TitleView = new MyResearchTitleView (this.Title, new RectangleF (0, 0, 200, 20));
			} else {
				this.NavigationItem.TitleView = new MyResearchTitleView (this.Title, new RectangleF (0, 0, 230, 20));
			}
				

			UIImage image = UIImage.FromFile ("action_menu.png");

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

		}
	}
}

