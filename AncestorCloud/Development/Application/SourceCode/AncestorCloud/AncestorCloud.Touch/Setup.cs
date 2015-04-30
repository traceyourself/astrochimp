using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using UIKit;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.CrossCore;

namespace AncestorCloud.Touch
{
	public class Setup : MvxTouchSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
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

		protected override IMvxTouchViewPresenter CreatePresenter()
		{
			return new MvxModalNavSupportTouchViewPresenter(ApplicationDelegate, Window);
		}

		protected override void InitializeFirstChance()
		{
			Mvx.RegisterSingleton<IAlert>(new IOSAlert());
			Mvx.RegisterSingleton<ILoader>(new Loader());
			Mvx.RegisterSingleton<IFileService>(new FileService());

			base.InitializeFirstChance();
		}
	}
}