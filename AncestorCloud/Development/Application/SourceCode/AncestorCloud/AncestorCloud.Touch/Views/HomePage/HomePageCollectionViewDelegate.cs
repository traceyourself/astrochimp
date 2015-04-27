using System;
using UIKit;
using System.Drawing;
using System.Collections.Generic;

namespace AncestorCloud.Touch
{
	public class HomePageCollectionViewDelegate : UICollectionViewDelegate
	{
		UIPageControl pageObject;
		UIScrollView scrollView;
		float numpages ;
			
			public HomePageCollectionViewDelegate(UIPageControl pageObj, List<ICollectionView> list)
		{
			this.pageObject = pageObj;
			numpages = list.Count;
			pageObject.Pages = (nint)numpages;
			pageObject.CurrentPage = 0;
		}


		public override void DecelerationEnded (UIScrollView scrollView)
		{

			var pageSize= scrollView.Bounds.Size.Width;
			var page = Math.Floor((((scrollView.ContentOffset.X)-pageSize/2.0)/pageSize)+1);

			if(page >= numpages)
			{
				page = numpages-1;
			}
			else if (page<0)
			{
				page=0;
			}

			pageObject.CurrentPage = (int) page;
			System.Diagnostics.Debug.WriteLine(""+page);
				
//					if (page >= Float(numpages)) {
//						page = Float(numpages) - 1;
//					} else if (page < 0) {
//						page = 0;
//					}
//
//						pageControl.currentPage = Int(page)
//						println(pageControl.currentPage)
		}




			
	}
}

