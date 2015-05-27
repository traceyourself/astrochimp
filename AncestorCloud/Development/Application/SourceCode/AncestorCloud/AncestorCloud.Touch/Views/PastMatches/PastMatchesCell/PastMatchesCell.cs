
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

				MvxImageViewLoader _imageViewLoader = new MvxImageViewLoader(() => this.FirstImage);
				MvxImageViewLoader _secImageViewLoader = new MvxImageViewLoader(() => this.SecondImage);

				var set = this.CreateBindingSet<PastMatchesCell, RelationshipFindResult> ();
				set.Bind (MyNameLabel).To (vm => vm.FirstPerson.Name);//WithConversion(new RelationshipTextConverter(),null);
				set.Bind(OtherNameLabel).To(vm => vm.SecondPerson.Name);//WithConversion(new RelationshipTextConverter(),null);
				set.Bind(_imageViewLoader).To (vm => vm.FirstPerson.ProfilePicURL);
				set.Bind(_secImageViewLoader).To (vm=> vm.SecondPerson.ProfilePicURL);
				set.Bind(DegreeLabel).To (vm =>vm.Degrees).WithConversion(new DegreeConverter(),null);
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


