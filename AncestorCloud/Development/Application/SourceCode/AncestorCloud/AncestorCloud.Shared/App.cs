using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {

		IDatabaseService _databaseService;

		public static bool IsAutoLogin { set; get;}

		public static ControllerType controllerTypeRef { set; get;}

		public static bool IsHomePageShown = false;

        public async override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

			_databaseService = Mvx.Resolve<IDatabaseService> ();

			User user = _databaseService.GetUser ();
			LoginModel loginModel = _databaseService.GetLoginDetails ();

			if ((user!=null && user.UserID != null) || (loginModel!=null && loginModel.UserEmail!=null))
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
			else 
			{
				RegisterAppStart<ViewModels.HomePageViewModel> ();

			}
		}

    }
}