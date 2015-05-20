using System;
using UIKit;

namespace AncestorCloud.Touch
{
	public class PictureLibrary :ICollectionView
	{
		string ImageName { get; set;}
		public PictureLibrary (string name)
		{
			ImageName = name;
		}

		public string Name{
			get{
				return "PictureLibrary";
			}
		}
		public UIImage Image{
			get{
				return UIImage.FromBundle(ImageName);

			}
		}
	}
}



		

		

	