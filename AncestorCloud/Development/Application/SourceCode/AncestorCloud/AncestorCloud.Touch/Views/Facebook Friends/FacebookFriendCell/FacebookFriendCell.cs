using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public partial class FacebookFriendCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("FacebookFriendCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("FacebookFriendCell");

		private readonly MvxImageViewLoader _imageViewLoader;

		public FacebookFriendCell (IntPtr handle) : base (handle)
		{
			_imageViewLoader = new MvxImageViewLoader(() => this.ProfilePic);

			this.DelayBind (() => {

				var set = this.CreateBindingSet<FacebookFriendCell, People> ();
				set.Bind (NameLabel).To (vm => vm.Name);
				set.Bind(_imageViewLoader).To (vm => vm.ProfilePicURL);
				set.Apply ();
				ProfilePic.Layer.CornerRadius=22f;
				ProfilePic.ClipsToBounds=true;
			});
		}

		public static FacebookFriendCell Create ()
		{
			return (FacebookFriendCell)Nib.Instantiate (null, null) [0];
		}
	}
}


