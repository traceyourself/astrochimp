using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using UIKit;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.CrossCore;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
using System;
using Cirrious.MvvmCross.Binding.BindingContext;

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
			Mvx.RegisterSingleton<IContactService>(new IOSContactService());
			Mvx.RegisterSingleton<ISMSService>(new IOSSMSService());
			Mvx.RegisterSingleton<IReachabilityService>(new IOSReachabilityService());

			base.InitializeFirstChance();
		}

		protected override void FillBindingNames(Cirrious.MvvmCross.Binding.BindingContext.IMvxBindingNameRegistry registry)
		{
			// use these to register default binding names
			//registry.AddOrOverwrite<NicerBinaryEdit>(be => be.MyCount);

			base.FillBindingNames(registry);
		}

		protected override void FillTargetFactories(Cirrious.MvvmCross.Binding.Bindings.Target.Construction.IMvxTargetBindingFactoryRegistry registry)
		{
			
			registry.RegisterCustomBindingFactory<UIButton>("Title",button => new MvxUIButtonTitleTargetBinding(button));
			//registry.RegisterFactory(new MvxSimplePropertyInfoTargetBindingFactory(typeof(String),typeof(UIButton),"Title"));

			base.FillTargetFactories(registry);
		}


	}
}