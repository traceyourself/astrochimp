using System;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public interface IDatabaseService
	{
		void InsertUser(User user);
		void UpdateUser(User user);
		void DeleteUser(User user);

		void InsertFamilyMember(People relative);
		void UpdateFamilyMember(People relative);
		void DeleteFamilyMember(People relative);

		List<People> RelativeMatching (string relationFilter);
		User GetUser (int id);
		List<User> GetUsers (string relationFilter);
		//int Count { get; }
	}
}

