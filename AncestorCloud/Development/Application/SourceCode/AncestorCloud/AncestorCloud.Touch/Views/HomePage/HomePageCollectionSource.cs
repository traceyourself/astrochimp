using System;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;
using ObjCRuntime;
using CoreAnimation;
using System.Drawing;

namespace AncestorCloud.Touch
{
	public class HomePageCollectionSource : UICollectionViewSource
	{
        static readonly NSString collectionCellId = new NSString ("CollectionCell");

		readonly List<ICollectionView> collectionViewList;

		public HomePageCollectionSource (List<ICollectionView> list)
		{
			collectionViewList = list;
		}

		public override nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return collectionViewList.Count;
		}

		#region CollectionView

		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var normalCell = (CollectionCell)collectionView.DequeueReusableCell (collectionCellId, indexPath);

			var cell = collectionViewList [indexPath.Row];

			normalCell.Image = cell.Image;

			return normalCell;
	
		}

		#endregion

		#region CollectionCell

		public class CollectionCell : UICollectionViewCell
		{
			UIImageView imageView;

			[Export ("initWithFrame:")]
			public CollectionCell (CGRect frame) : base (frame)
			{
				BackgroundView = new UIView{BackgroundColor = UIColor.FromRGB(237,237,237)};
				Frame = new RectangleF (0f,0f ,240f, 240f);
				SelectedBackgroundView = new UIView{BackgroundColor = UIColor.FromRGB(237,237,237)};

				imageView = new UIImageView (UIImage.FromBundle ("21.png"));
				imageView.Frame = Frame;
				imageView.Center = ContentView.Center;
				imageView.ContentMode= UIViewContentMode.ScaleAspectFit;
				ContentView.AddSubview (imageView);
			}

			public UIImage Image {
				set {
					imageView.Image = value;
				}
			}

			[Export("custom")]
			void Custom()
			{
				// Put all your custom menu behavior code here
				//Console.WriteLine ("custom in the cell");
			}

			public override bool CanPerform (Selector action, NSObject withSender)
			{
				if (action == new Selector ("custom"))
					return true;
				else
					return false;
			}
		}

		#endregion

	}

}




