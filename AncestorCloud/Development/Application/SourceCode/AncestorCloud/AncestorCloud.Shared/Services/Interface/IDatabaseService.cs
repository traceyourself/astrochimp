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

		List<People> RelativeMatching (string relationFilter);
		User GetUser (int id);
		List<User> GetUsers (string relationFilter);
		//int Count { get; }
	}
}

