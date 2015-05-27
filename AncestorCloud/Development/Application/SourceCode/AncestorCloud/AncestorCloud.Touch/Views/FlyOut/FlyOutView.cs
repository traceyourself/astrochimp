
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
using System.Net;
using System.IO;
using System.Text;
using AncestorCloud.Core;

namespace AncestorCloud.Touch
{
	public partial class FlyOutView : BaseViewController
	{
		#region Globals
		FlyoutNavigationController _navigation;
		private MvxSubscriptionToken navigationMenuToggleToken;
		private MvxSubscriptionToken navigationBarHiddenToken;
		private MvxSubscriptionToken changeFlyoutToken;
		private MvxSubscriptionToken ImageUploadedToken;
		IMvxMessenger _messenger;
		ProfileCellView profCell;
		//UITextView userNameMenu;
		string documentsPath,localFilename,localPath;
		#endregion

		#region LifeCycle Methods
		public FlyOutView () : base ("FlyOutView", null)
		{
			_messenger = Mvx.Resolve<IMvxMessenger>();
		}

		public new FlyOutViewModel ViewModel
		{
			get { return base.ViewModel as FlyOutViewModel; }
			set { base.ViewModel = value; }
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Flyout ();
			AddMessengers ();
			DownloadImage ();
			//SetProfilePic ();

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
			SetProfilePic ();
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
			_navigation.NavigationTableView.BackgroundColor = Themes.NavBarTableView();
			this.NavigationController.NavigationBar.BarTintColor = Themes.NavBarTintColor();

			this.NavigationItem.SetHidesBackButton (true, false);

		
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


			CreateFlyoutNavigation ();


		}

		private void CreateFlyoutNavigation()
		{

			var flyoutViewControllers = new List<UIViewController>();

			//var homeViewModel = ViewModel as FlyOutViewModel;
			if (ViewModel != null)
			{
				//create the ViewModels
				foreach (var viewModel in ViewModel.MenuItems)
				{
					var viewModelRequest = new MvxViewModelRequest
					{
						ViewModelType = viewModel.ViewModelType
					};

					if (viewModel.ViewModelType == typeof(TestViewModel)) {

						flyoutViewControllers.Add(null);
						continue;
					}

					flyoutViewControllers.Add(CreateMenuItemController(viewModelRequest));

				}
				_navigation.ViewControllers = flyoutViewControllers.ToArray();


			}
		}
			
		private void CreateFlyoutView()
		{
			//var flyoutViewControllers = new List<UIViewController>();

			var flyoutMenuElements = new Section();

			//var homeViewModel = ViewModel as FlyOutViewModel;
			if (ViewModel != null)
			{
				//create the ViewModels
				foreach (var viewModel in ViewModel.MenuItems)
				{
					if (viewModel.ViewModelType == typeof(TestViewModel)) {


						UIImageView view = new UIImageView (new RectangleF (0f, 0f, 280, (float)this.View.Frame.Size.Height-277));
						view.Image = UIImage.FromBundle (StringConstants.GREYPATCH);
						flyoutMenuElements.Add (new CustomViewElement("",view));
						continue;
					}

					if (viewModel.ViewModelType == typeof(ProfilePicViewModel)) {
						AppDelegate _delegate = (AppDelegate)UIApplication.SharedApplication.Delegate;

						UIImage image = _delegate.UIImageProfilePic ?? UIImage.FromBundle (StringConstants.PROFILEIMAGE);

						profCell = new ProfileCellView(viewModel.Title,image);

						flyoutMenuElements.Add (new CustomViewElement("",profCell.View));

						continue;
					}

					CustomCellView cell = new CustomCellView(viewModel.Title,viewModel.Image);
				
					flyoutMenuElements.Add(new CustomViewElement("",cell.View));



				}
					
				//add the menu elements
								var rootElement = new RootElement("")
								{
									flyoutMenuElements
								};
			
				//rootElement.
				_navigation.NavigationRoot = rootElement;
			}

		}

		private void ReloadMenu()
		{
			
			CreateFlyoutNavigation ();
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
			ImageUploadedToken = _messenger.SubscribeOnMainThread<ProfilePicUploadedMessage>(Message => this.SetProfilePic ());
			changeFlyoutToken = _messenger.SubscribeOnMainThread<ReloadFlyOutViewMessage>(message => this.ReloadMenu());
			navigationMenuToggleToken = _messenger.SubscribeOnMainThread<ToggleFlyoutMenuMessage>(message => _navigation.ToggleMenu());
			navigationBarHiddenToken = _messenger.SubscribeOnMainThread<NavigationBarHiddenMessage>(message => this.HideNavBar( message.NavigationBarHidden));
		}

		void RemoveMessengers()
		{
			_messenger.Unsubscribe<ProfilePicUploadedMessage> (ImageUploadedToken);
			_messenger.Unsubscribe<ReloadFlyOutViewMessage> (changeFlyoutToken);
			_messenger.Unsubscribe<ToggleFlyoutMenuMessage> (navigationMenuToggleToken);
			_messenger.Unsubscribe<NavigationBarHiddenMessage> (navigationBarHiddenToken);
		
		}


		void SetProfilePic()
		{
			if (profCell != null) {
				AppDelegate appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;

				profCell.ProfileImage = appDelegate.UIImageProfilePic;
			} 

		}

		#endregion

		public void DownloadImage()
		{
			LoginModel model= ViewModel.GetUserData ();
			//userNameMenu.Text = model.UserEmail;

			var webClient = new WebClient();

			webClient.DownloadDataCompleted += (s, e) => {

				try
				{
				
					var bytes = e.Result; // get the downloaded data
					documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					localFilename = "downloaded.png";
					localPath = Path.Combine (documentsPath, localFilename);
					File.WriteAllBytes (localPath, bytes); // writes to local storage 

					AppDelegate appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;

					appDelegate.UIImageProfilePic=  UIImage.FromFile (localPath);

					SetProfilePic ();
				}
				catch(Exception ex )
				{
					System.Diagnostics.Debug.WriteLine (ex.InnerException);
				}


			};
			if(model.AvatarURL == null || model.AvatarURL=="")
				return ;
			
			var url = new Uri ("" + model.AvatarURL);

			webClient.DownloadDataAsync(url);

		}

	}



}

