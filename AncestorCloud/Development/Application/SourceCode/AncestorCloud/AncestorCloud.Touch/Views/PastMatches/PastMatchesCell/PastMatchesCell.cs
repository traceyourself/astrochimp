
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public partial class PastMatchesCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("PastMatchesCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("PastMatchesCell");

		public PastMatchesCell (IntPtr handle) : base (handle)
		{
			
			
			this.DelayBind (() => {

				var set = this.CreateBindingSet<PastMatchesCell, People> ();
				set.Bind (MyNameLabel).To (vm => vm.Name);
				set.Bind(OtherNameLabel).To(vm => vm.Relation).WithConversion(new RelationshipTextConverter(),null);
				set.Apply ();
				SetImages();

		
			});
		}

		public static PastMatchesCell Create ()
		{
			return (PastMatchesCell)Nib.Instantiate (null, null) [0];
		}

		public void SetImages()
		{
			FirstImage.Layer.CornerRadius = 30f;
			FirstImage.ClipsToBounds = true;
			SecondImage.Layer.CornerRadius = 30f;
			SecondImage.ClipsToBounds = true;
			OtherImageView.Layer.CornerRadius = 30f;
			OtherImageView.ClipsToBounds = true;
			OtherImageView.BackgroundColor=UIColor.FromRGB(174,219,222);
		}
	}
}


