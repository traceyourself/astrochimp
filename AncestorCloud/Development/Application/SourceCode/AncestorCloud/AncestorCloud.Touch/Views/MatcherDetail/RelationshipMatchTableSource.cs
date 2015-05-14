using System;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using Xamarin.Social.Services;
using Xamarin.Social;
using System.Collections.Generic;

namespace AncestorCloud.Touch
{

	public class RelationshipMatchTableSource :MvxTableViewSource
	{
		//readonly string cellIdentifier = "RelationshipMatchDetailCell";

		public RelationshipMatchTableSource(UITableView tableView): base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("RelationshipMatchDetailCell", NSBundle.MainBundle),
				RelationshipMatchDetailCell.Key);

		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			return tableView.DequeueReusableCell(RelationshipMatchDetailCell.Key, indexPath);
		}


	}

	public class RelationshipMatchTableDelegate : UITableViewDelegate
	{

//		TwitterService mTwitter=  new TwitterService {
//			ConsumerKey = "AUhsvThNimDhDYM6lNhaE3uZ1", //"Consumer key from https://dev.twitter.com/apps",
//			ConsumerSecret = "gHXfS0c91rYwT4BQG0gcOAd3KVFgES6ruOaN4ryl8HozLFzAyj" //"Consumer secret from https://dev.twitter.com/apps",
//			//CallbackUrl = new Uri ("Callback URL from https://dev.twitter.com/apps")
//		};

		readonly Twitter5Service mTwitter = new Twitter5Service ();

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			UIWindow window = UIApplication.SharedApplication.KeyWindow;

			UIViewController rootViewController = window.RootViewController;

			Item item = new Item {
				Text = "I'm sharing great things using AncestorCloud!",
//				Links = new List<Uri> {
//					new Uri ("http://xamarin.com"),
//				},
			};

			UIViewController vc = mTwitter.GetShareUI (item, shareResult => {
				rootViewController.DismissViewController (true, null);
			});
			rootViewController.PresentViewController (vc, true, null);
		}
	}
}

