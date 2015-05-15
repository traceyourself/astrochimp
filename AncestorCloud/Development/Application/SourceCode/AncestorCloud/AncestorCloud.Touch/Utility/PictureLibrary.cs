using System;
using UIKit;

namespace AncestorCloud.Touch
{
	public class PictureLibrary :ICollectionView
	{
		public PictureLibrary ()
		{
		}

		public string Name{
			get{
				return "PictureLibrary";
			}
		}
		public UIImage Image{
			get{
				return UIImage.FromBundle("21.png");
			}
		}
	}
}



		

		

	