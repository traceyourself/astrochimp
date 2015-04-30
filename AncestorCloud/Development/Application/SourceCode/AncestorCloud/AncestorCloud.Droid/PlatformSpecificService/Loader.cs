using System;
using AncestorCloud.Shared;
using Android.App;

namespace AncestorCloud.Droid
{
	public class Loader : ILoader
	{
		ProgressDialog pd;

		#region ILoader implementation
		public void showLoader ()
		{	
			try{
				if(Utilities.CurrentActiveActivity != null && pd == null){
					pd = ProgressDialog.Show (Utilities.CurrentActiveActivity,"","Loading...");
				}
			}catch(Exception e){
				System.Diagnostics.Debug.WriteLine (e.Message);
			}
		}
		public void hideLoader ()
		{
			try{
				if(pd != null){
					pd.Dismiss ();
					pd = null;
				}
			}catch(Exception e){
				System.Diagnostics.Debug.WriteLine (e.Message);
			}
		}
		#endregion
		
	}
}

