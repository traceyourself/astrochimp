
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
		private MvxSubscriptionToken changeFlyoutToken;
		IMvxMessenger _messenger;

//		string[] Tasks = {
//
//			"My Family",
//			"Matcher",
//			"Research Help",
//			"Log Out",
//		};

		#region LifeCycle Methods
		public FlyOutView () : base ("FlyOutView", null)
		{
			_messenger = Mvx.Resolve<IMvxMessenger>();
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Flyout ();
			AddMessengers ();

		}

		public override void ViewWillUnload ()
		{
			RemoveMessengers ();

			base.ViewWillUnload ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			CreateFlyoutView ();
		}

		#endregion
			

		public  void Flyout ()
		{
			this.NavigationController.NavigationBarHidden = true;
			Title = string.Empty;

			_navigation = new  FlyoutNavigationController();
			_navigation.Position = FlyOutNavigationPosition.Left;
			_navigation.View.Frame = UIScreen.MainScreen.Bounds;
			View.AddSubview (_navigation.View);
			this.AddChildViewController (_navigation);

			//this.NavigationItem.TitleView = new MyTitleView (this.Title);
			_navigation.NavigationTableView.BackgroundColor = UIColor.FromRGB (46, 58, 73);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (64,172,176);

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


			CreateFlyoutNavigation ();


		}

		private void CreateFlyoutNavigation()
		{

			var flyoutViewControllers = new List<UIViewController>();

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

				}
				_navigation.ViewControllers = flyoutViewControllers.ToArray();


			}
		}

		private void ReloadFlyoutNavigation()
		{

			var flyoutViewControllers = new List<UIViewController>();

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

				}
				_navigation.ViewControllers = flyoutViewControllers.ToArray();


			}
		}
		private void CreateFlyoutView()
		{
			//var flyoutViewControllers = new List<UIViewController>();

			var flyoutMenuElements = new Section();

			var homeViewModel = ViewModel as FlyOutViewModel;
			if (homeViewModel != null)
			{
				//create the ViewModels
				foreach (var viewModel in homeViewModel.MenuItems)
				{
					flyoutMenuElements.Add(new CustomStringElement(viewModel.Title,UIImage.FromBundle(viewModel.Image)));

				}

				//add the menu elements
								var rootElement = new RootElement("")
								{
									flyoutMenuElements
								};

				_navigation.NavigationRoot = rootElement;
			}

		}

		private void ReloadMenu()
		{
			
			ReloadFlyoutNavigation ();
			CreateFlyoutView ();

			//this.NavigationController.NavigationBarHidden = true;
		}
			
		private UIViewController CreateMenuItemController(MvxViewModelRequest viewModelRequest)
		{
			var controller = new UINavigationController();
			var screen = this.CreateViewControllerFor(viewModelRequest) as UIViewController;
			controller.PushViewController(screen, false);
			return controller;
		}


		#region Helper Methods

		void HideNavBar(bool boolValue)
		{
			if (this.NavigationController != null) {

				this.NavigationController.NavigationBarHidden = boolValue;
			}
		}


		void AddMessengers()
		{
			changeFlyoutToken = _messenger.SubscribeOnMainThread<ReloadFlyOutViewMessage>(message => this.ReloadMenu());
			navigationMenuToggleToken = _messenger.SubscribeOnMainThread<ToggleFlyoutMenuMessage>(message => _navigation.ToggleMenu());
			navigationBarHiddenToken = _messenger.SubscribeOnMainThread<NavigationBarHiddenMessage>(message => this.HideNavBar( message.NavigationBarHidden));
		}

		void RemoveMessengers()
		{
			_messenger.Unsubscribe<ReloadFlyOutViewMessage> (changeFlyoutToken);
			_messenger.Unsubscribe<ToggleFlyoutMenuMessage> (navigationMenuToggleToken);
			_messenger.Unsubscribe<NavigationBarHiddenMessage> (navigationBarHiddenToken);
		
		}

		#endregion

	}



}

