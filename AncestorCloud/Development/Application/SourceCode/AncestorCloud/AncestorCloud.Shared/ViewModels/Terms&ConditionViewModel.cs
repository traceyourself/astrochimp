using System;

namespace AncestorCloud.Shared.ViewModels
{
	public class TermsandConditionViewModel : BaseViewModel
	{
		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

		public void ShowSignUpView()
		{
			ShowViewModel<SignUpViewModel> ();
			this.Close ();
		}
	}
}

