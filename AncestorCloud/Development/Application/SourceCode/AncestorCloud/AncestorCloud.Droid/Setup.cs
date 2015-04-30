
using Android.Content;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using AncestorCloud.Shared;
using Cirrious.CrossCore;

namespace AncestorCloud.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

		protected override void InitializeFirstChance()
		{
			Mvx.RegisterSingleton<IAlert>(new DroidAlerts());
			Mvx.RegisterSingleton<IAndroidService>(new IsAndroidService());
			Mvx.RegisterSingleton<ILoader>(new Loader());
			Mvx.RegisterSingleton<IFileService>(new FileService());
			base.InitializeFirstChance();
		}

    }
}