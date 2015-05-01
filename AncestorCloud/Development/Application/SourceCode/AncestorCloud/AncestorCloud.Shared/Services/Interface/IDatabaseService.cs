using System;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public interface IDatabaseService
	{
		void InsertUser(User user);
		void UpdateUser(User user);
		void DeleteUser(User user);
		User GetUser (int id);
		List<User> GetUsers (string relationFilter);

		void InsertRelative(People relative);
		void UpdateRelative(People relative);
		void DeleteRelative(People relative);
		void InsertRelatives(List<People> relatives);
		List<People> RelativeMatching (string relationFilter);
		List<People> GetFamily ();

		void InsertLoginDetails (LoginModel login);
		LoginModel GetLoginDetails ();

		List<Celebrity> GetCelebritiesData ();
		void StoreCelebrities (List<Celebrity> celebDataList);
		List<Celebrity> FilterCelebs (string filter);
		bool IsCelebStored ();

		void DropAllTables ();
		//int Count { get; }
	}
}

