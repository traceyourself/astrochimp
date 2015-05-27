using System;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public interface IDatabaseService
	{
		void InsertUser(User user);
		void UpdateUser(User user);
		void DeleteUser(User user);
		User GetUser ();
		List<User> GetUsers (string relationFilter);

		void InsertRelative(People relative);
		void UpdateRelative(People relative);
		void DeleteRelative(People relative);
		void InsertRelatives(List<People> relatives);
		List<People> RelativeMatching(string relationFilter, string userID);
		List<People> GetFamily(User user);

		void InsertFBFriend (People relative);

		void InsertFamilyMember (People relative);
		int IsMemberExists (string filter, string userId);
		People GetFamilyMember (string indiogfn, string userId);

		void InsertContact(People contact);

		void InsertLoginDetails (LoginModel login);
		LoginModel GetLoginDetails ();

		List<Celebrity> GetCelebritiesData ();
		void StoreCelebrities (List<Celebrity> celebDataList);
		List<Celebrity> FilterCelebs (string filter);
		bool IsCelebStored ();

		void DropAllTables ();
		//int Count { get; }
		List<People> GetMember(string filter, string userId);
	}
}

