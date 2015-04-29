using System;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public interface IDatabaseService
	{
		void InsertUser(User user);
		void UpdateUser(User user);
		void DeleteUser(User user);

		void InsertRelative(People relative);
		void UpdateRelative(People relative);
		void DeleteRelative(People relative);

		void InsertRelatives(List<People> relatives);

		void InsertLoginDetails (LoginModel login);

		LoginModel GetLoginDetails ();

		List<People> RelativeMatching (string relationFilter);
		User GetUser (int id);
		List<People> GetFamily ();
		List<User> GetUsers (string relationFilter);
		//int Count { get; }
	}
}

