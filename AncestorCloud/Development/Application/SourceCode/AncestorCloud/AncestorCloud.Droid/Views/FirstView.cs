using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Android.Content.PM;

namespace AncestorCloud.Droid.Views
{
	[Activity(Label = "View for FirstViewModel", ScreenOrientation = ScreenOrientation.Portrait)]
	public class FirstView : BaseActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FirstView);
        }
    }
}