using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace AncestorCloud.Droid.Views
{
    [Activity(Label = "View for FirstViewModel")]
	public class FirstView : BaseActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FirstView);
        }
    }
}