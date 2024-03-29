﻿using System;
using Foundation;
using UIKit;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Touch
{
	public partial class CelebritiesCell : MvxTableViewCell
	{

//		public Action<object> AddCelbButtonTapped
//		{ get; set; }

		public static readonly UINib Nib = UINib.FromName ("CelebritiesCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("CelebritiesCell");

		public Celebrity familyMember{ get; set;}

		private readonly MvxImageViewLoader _imageViewLoader;

		public CelebritiesCell (IntPtr handle) : base (handle)
		{
			_imageViewLoader = new MvxImageViewLoader(() => this.CelebImageView);

			this.DelayBind (() => {

				var set = this.CreateBindingSet<CelebritiesCell, Celebrity> ();
				set.Bind (NameLabel).To (vm => vm.GivenNames);
				set.Bind(LastName).To (vm => vm.LastName);
				set.Bind(_imageViewLoader).To (vm => vm.Img);

				//set.Bind(OtherNameLabel).To(vm => vm.Relation).WithConversion(new RelationshipTextConverter(),null);
				set.Apply ();
				CelebImageView.Layer.CornerRadius=22f;
				CelebImageView.ClipsToBounds=true;
			});
		}

		public static CelebritiesCell Create ()
		{
			return (CelebritiesCell)Nib.Instantiate (null, null) [0];
		}


		partial void AddCelbButtonTapped (NSObject sender)
		{
			//System.Diagnostics.Debug.WriteLine("ADD BUTTON TAPED");


			var messenger = Mvx.Resolve<IMvxMessenger> ();
			messenger.Publish (new MyAddButtonTappedMessage (this,familyMember));
		}
			
	}
}

