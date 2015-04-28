using System;
using AncestorCloud.Shared;
using UIKit;

namespace AncestorCloud.Touch
{
	public class Loader : ILoader
	{
		#region ILoader implementation
		public void showLoader ()
		{
			AppDelegate _delagate = (AppDelegate)UIApplication.SharedApplication.Delegate;

			_delagate.ShowActivityLoader ();
		}
		public void hideLoader ()
		{
			AppDelegate _delagate = (AppDelegate)UIApplication.SharedApplication.Delegate;

			_delagate.HideActivityLoader ();
		}
		#endregion
		
	}
}

