
using System;

using Foundation;
using UIKit;
using FlyoutNavigation;
using MonoTouch.Dialog;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Views;
using AncestorCloud.Shared.ViewModels;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Shared;
using System.Drawing;
using System.Linq;

namespace AncestorCloud.Touch
{
	public partial class FlyOutView : BaseViewController
	{
		FlyoutNavigationController _navigation;
		private MvxSubscriptionToken navigationMenuToggleToken;
		private MvxSubscriptionToken navigationBarHiddenToken;
		string[] Tasks = {

			"My Family",
			"Matcher",
			"Research Help",
			"Log Out",
		};
		public FlyOutView () : base ("FlyOutView", null)
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
			Flyout ();

//			var messenger = Mvx.Resolve<IMvxMessenger>();
//			navigationBarHiddenToken = messenger.SubscribeOnMainThread<NavigationBarHiddenMessage>(message => NavigationController.NavigationBarHidden = message.NavigationBarHidden);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

//		public override void ViewWillDisappear (bool animated)
//		{
//
//			if (!NavigationController.ViewControllers.Contains (this)) {
//				var messenger = Mvx.Resolve<IMvxMessenger> ();
//				messenger.Publish (new NavigationBarHiddenMessage (this, true)); 
//			}
//			base.ViewWillDisappear (animated);
//		}

		public  void Flyout ()
		{

			Title="";
			_navigation = new  FlyoutNavigationController();
			_navigation.Position = FlyOutNavigationPosition.Left;
			_navigation.View.Frame = UIScreen.MainScreen.Bounds;
			View.AddSubview (_navigation.View);
			this.AddChildViewController (_navigation);
			this.NavigationItem.TitleView = new MyTitleView ();
			_navigation.NavigationTableView.BackgroundColor = UIColor.FromRGB (46, 58, 73);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (178, 45, 116);
			this.NavigationController.NavigationBarHidden = false;
			this.NavigationItem.SetHidesBackButton (true, false);

		
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


			var flyoutViewControllers = new List<UIViewController>();


			var flyoutMenuElements = new Section();




			var homeViewModel = ViewModel as FlyOutViewModel;
			if (homeViewModel != null)
			{
				//create the ViewModels
				foreach (var viewModel in homeViewModel.MenuItems)
				{
					var viewModelRequest = new MvxViewModelRequest
					{
						ViewModelType = viewModel.ViewModelType
					};

					flyoutViewControllers.Add(CreateMenuItemController(viewModelRequest));
					flyoutMenuElements.Add(new CustomStringElement(viewModel.Title));

				}
				_navigation.ViewControllers = flyoutViewControllers.ToArray();

				//add the menu elements
				var rootElement = new RootElement("")
				{
					flyoutMenuElements
				};
					
				_navigation.NavigationRoot = rootElement;
			}

			//Listen to messages to toggle the menu and hide MvvmCrosses navigation bar
			var _messenger = Mvx.Resolve<IMvxMessenger>();
			navigationMenuToggleToken = _messenger.SubscribeOnMainThread<ToggleFlyoutMenuMessage>(message => _navigation.ToggleMenu());
			navigationBarHiddenToken = _messenger.SubscribeOnMainThread<NavigationBarHiddenMessage>(message => NavigationController.NavigationBarHidden = message.NavigationBarHidden);
		}



		private UIViewController CreateMenuItemController(MvxViewModelRequest viewModelRequest)
		{
			var controller = new UINavigationController();
			var screen = this.CreateViewControllerFor(viewModelRequest) as UIViewController;
			controller.PushViewController(screen, false);
			return controller;
		}

	}



}

