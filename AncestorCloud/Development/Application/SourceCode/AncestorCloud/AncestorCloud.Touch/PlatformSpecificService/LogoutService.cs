using System;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public class LogoutService : ILogoutService
	{
		public void Logout()
		{
			Facebook.LoginKit.LoginManager manager = new Facebook.LoginKit.LoginManager();
			manager.LogOut();
		}
	}
}

