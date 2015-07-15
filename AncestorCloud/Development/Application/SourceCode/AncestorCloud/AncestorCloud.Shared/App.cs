using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {

		IDatabaseService _databaseService;

		public static bool IsAutoLogin { set; get;}
		public static bool IsHomePageShown = false;

        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

			_databaseService = Mvx.Resolve<IDatabaseService> ();

			User user = _databaseService.GetUser ();

			if (user.UserID == null)
			{
				RegisterAppStart<ViewModels.HomePageViewModel> ();
			}
			else 
			{
				IsAutoLogin = true;

				if(Mvx.CanResolve<IAndroidService>())
				{
					RegisterAppStart<ViewModels.FamilyViewModel> ();
				}
				else
				{
					RegisterAppStart<ViewModels.FlyOutViewModel> ();  // IOS
				}
			}
		}

    }
}