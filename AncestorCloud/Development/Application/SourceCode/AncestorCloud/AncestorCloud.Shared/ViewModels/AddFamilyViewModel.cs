using System;
using Cirrious.MvvmCross.ViewModels;

namespace AncestorCloud.Shared.ViewModels
{
	public class AddFamilyViewModel:BaseViewModel
	{
		private People _familyMember;

		public People FamilyMember 
		{ 
			get { return _familyMember ?? new People(); }
			set
			{
				_familyMember = value;
				RaisePropertyChanged(() => FamilyMember);
			}
		
		}


		#region Commands

		private ACCommand _addPersonCommand;

		public ACCommand AddPersonCommand
		{
			get 
			{ 
				return this._addPersonCommand ?? (this._addPersonCommand = new ACCommand (this.AddPerson));
			}
		}

		#endregion

		#region close call
		public void Close()
		{
			this.Close (this);
		}
		#endregion


		public void ShowMyFamily()
		{
			ShowViewModel<MyFamilyViewModel> ();
		}

		private void AddPerson()
		{
			FamilyMember = FamilyMember;
		}

	}


}

