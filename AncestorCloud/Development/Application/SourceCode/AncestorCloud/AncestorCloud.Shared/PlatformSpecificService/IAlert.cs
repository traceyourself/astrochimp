using System;

namespace AncestorCloud.Shared
{
	public interface IAlert
	{
		void ShowAlert (string message, string title);

		void ShowAlertWithOk (string message, string title, AlertType alert);

		void ShowLogoutAlert (string message, string title);

	}
}

