
using System;

using Foundation;
using UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;

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

			this.NavigationController.NavigationBar.TintColor=UIColor.FromRGB(255,255,255);
		this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (178, 45, 116);

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

