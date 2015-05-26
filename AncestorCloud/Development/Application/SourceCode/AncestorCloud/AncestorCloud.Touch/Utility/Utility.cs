using System;
using Foundation;

namespace AncestorCloud.Touch
{
	public static class Utility
	{
		 
		public static NSBundle LocalisedBundle()
		{
			var path = NSBundle.MainBundle.PathForResource("en", "lproj");
			NSBundle languageBundle = NSBundle.FromPath(path);
			return languageBundle;
		}
	}

//	public static class LocalizationExtensions
//	{
//		public static string t(this string translate)
//		{
//			return NSBundle.MainBundle.LocalizedString(translate, "", "");
//		}
//	}

}

