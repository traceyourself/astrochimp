using System;
using System.Linq;
using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.Sqlite;
using Cirrious.CrossCore;

namespace AncestorCloud.Shared
{
	public class DatabaseService : IDatabaseService
	{
		private readonly ISQLiteConnection _connection;

		public DatabaseService(ISQLiteConnectionFactory factory,IFileService _file)
		{
			string DbName = "database.db";

			if (!Mvx.CanResolve<IAndroidService> ()) {
				
				DbName = _file.GetDatabasePath (DbName);
			}

			_connection = factory.Create(DbName);

			CreateTables ();
		}

		public void CreateTables()
		{
			_connection.CreateTable<People>();
			CreateLoginTabel ();
			_connection.CreateTable<Celebrity> ();
			_connection.CreateTable<LoginModel>();
		}
		#region

		public void InsertContact(People contact)
		{
			if (contact == null)
				throw new ArgumentNullException ("contact");

			if (Convert.ToBoolean(IsContactExists(contact.Contact,contact.LoginUserLinkID)))
				_connection.Update (contact);
			else
				_connection.Insert(contact);
		}

	

		private void UpdateContact(People contact)
		{
			if (contact == null)
				throw new ArgumentNullException ("contact");

			//String query = "UPDATE People SET IndiOgfn='" + contact.IndiOgfn + "', FirstName='" + contact.FirstName + "', LastName='" + contact.LastName+"', MiddleName='" + contact.MiddleName+"' , DateOfBirth='" + contact.DateOfBirth+"' , Email='" + contact.Email+"' , Relation='" + contact.Relation+"' , LoginUserLinkID='" + contact.LoginUserLinkID+"' WHERE Contact='"+contact.Contact+"'";			
			String query = "UPDATE People SET IndiOgfn='" + contact.IndiOgfn + "' WHERE Contact='"+contact.Contact+"'";			
			_connection.Query<People> (query);
		}

			
		private int IsContactExists(string number, string userID)
		{
			int count =  _connection.Table<People>().Where(x => x.Contact.Contains(number) && x.LoginUserLinkID.Contains(userID)).ToList().Count();
			return count;
		}

		#endregion

			
		#region IDatabaseService implementation

		public void InsertUser (User user)
		{
			if (user == null)
				throw new ArgumentNullException ("user");

			if (Convert.ToBoolean(IsUserExist(user.UserID)))
				_connection.Update (user);
			else
				 _connection.Insert(user);
		}
		public void UpdateUser (User user)
		{
			if (user == null)
				throw new ArgumentNullException ("user");
			
			_connection.Update(user);
		}
		public void DeleteUser (User user)
		{
			if (user == null)
				throw new ArgumentNullException ("user");
			
			_connection.Delete(user);
		}


		public void InsertRelative (People relative)
		{
			if (relative == null)
				throw new ArgumentNullException ("relative");

			if (Convert.ToBoolean(IsRelativeExist(relative.UserID,relative.LoginUserLinkID)))
				_connection.Update (relative);
			else
			 _connection.Insert(relative);
		}


		public void InsertFBFriend (People relative)
		{
			if (relative == null)
				throw new ArgumentNullException ("relative");

			if (Convert.ToBoolean(IsFbFriendExist(relative.UserID,relative.LoginUserLinkID)))
				_connection.Update (relative);
			else
				_connection.Insert(relative);
		}


		public void InsertFamilyMember (People relative)
		{
			if (relative == null)
				throw new ArgumentNullException ("relative");

			if (Convert.ToBoolean (IsMemberExists (relative.IndiOgfn, relative.LoginUserLinkID)))
				return;

			_connection.Insert (relative);
		}

		public People GetFamilyMember (string indiogfn, string userId)
		{
			if (indiogfn == null)
				throw new ArgumentNullException ("indiogfn");

			if (userId == null)
				throw new ArgumentNullException ("userId");

			List<People> member =  (List<People>)_connection.Table<People> ().Where(x => x.IndiOgfn.Contains(indiogfn) && x.LoginUserLinkID.Contains(userId)).ToList();
			return member.Count > 0 ? member[member.Count - 1] : new People();
		}


		public void UpdateRelative (People relative)
		{
			if (relative == null)
				throw new ArgumentNullException ("relative");
			
			_connection.Update(relative);
		}
		public void DeleteRelative (People relative)
		{
			if (relative == null)
				throw new ArgumentNullException ("relative");
			
			_connection.Update(relative);
		}

		public User GetUser()
		{
			List<User> user=  (List<User>)_connection.Table<User> ().ToList();
			return user.Count > 0 ? user[user.Count - 1] : new User();
		}

		public List<User> GetUsers(string relationFilter)
		{
			if (relationFilter == null)
				throw new ArgumentNullException ("relationFilter");

			List<User> list = _connection.Table<User>().Where(x => x.UserID.Contains(relationFilter)).ToList();
			return list;
		}

		public List<People> RelativeMatching(string relationFilter, string userID)
		{
			if (relationFilter == null)
				throw new ArgumentNullException ("relationFilter");

			if (userID == null)
				throw new ArgumentNullException ("userID");
			
			List<People> list = _connection.Table<People>().Where(x => x.Relation.Contains(relationFilter) && x.LoginUserLinkID.Contains(userID)).ToList();
			return list;
		}


		public void InsertRelatives(List<People> relatives)
		{
			if (relatives == null)
				throw new ArgumentNullException ("relatives");

			_connection.InsertAll (relatives);
		}

		public List<People> GetFamily(User user)
		{
			List<People> list = _connection.Query<People> ("select * from People where Relation NOT LIKE '%friend%' AND Tag = '"+AppConstant.FBTAGKEY+"' AND LoginUserLinkID = '"+user.Email+"'");
			return list;
		}

		public void InsertLoginDetails (LoginModel login)
		{
			if (login == null)
				throw new ArgumentNullException ("login");
			try{
				if (Convert.ToBoolean(IsLoggedInUserExists(login.UserEmail)))
					UpdateLoginUser (login);
				else
					_connection.Insert (login);
			}catch(Exception e){
				System.Diagnostics.Debug.WriteLine (""+e.StackTrace);
			}
		}

		public LoginModel GetLoginDetails ()
		{
			List<LoginModel> login = (List<LoginModel>)_connection.Table<LoginModel> ().ToList();
			return login.Count > 0 ? login[login.Count - 1] : new LoginModel();
		}



		public List<Celebrity> GetCelebritiesData ()
		{
			List<Celebrity> list = (List<Celebrity>)_connection.Table<Celebrity> ().ToList();
			return list;
		}

		public void StoreCelebrities (List<Celebrity> celebDataList)
		{
			if (celebDataList == null)
				throw new ArgumentNullException ("celebDataList");
			
			_connection.InsertAll (celebDataList);
		}

		public List<Celebrity> FilterCelebs (string filter)
		{
			if (filter == null)
				throw new ArgumentNullException ("filter");

//			List<Celebrity> list = _connection.Table<Celebrity>().Where(x => x.GivenNames.Contains(filter) || x.LastName.Contains(filter)).ToList();
//			return list;
			filter = filter.ToLower().Trim();
			string words = filter.Replace(" ", "','");
			String query = "SELECT * FROM Celebrity WHERE GivenNames LIKE '%"+filter+"%' OR LastName LIKE '%"+filter+"%' OR LOWER(LastName) IN ('"+words+"') OR LOWER(GivenNames) IN ('"+words+"')";
			List<Celebrity> list = _connection.Query<Celebrity>(query);
			return list;
		}


		public void DropAllTables()
		{
//			DropTables ();
//			CreateTables ();
			//TODO : dropping Login table for now
			DroploginTable();
			CreateLoginTabel ();
		}

		private void DropTables()
		{
			DroploginTable ();
			_connection.DropTable<LoginModel> ();
			_connection.DropTable<People>();
			_connection.DropTable<Celebrity> ();
		}

		private void DroploginTable()
		{
			_connection.DropTable<User>();
		}

		private void CreateLoginTabel()
		{
			_connection.CreateTable<User>();
		}
		#endregion


		#region Helper Methods
		private void UpdateLoginUser(LoginModel modal)
		{
//			String query = "UPDATE LoginModel SET IndiOGFN='" + modal.IndiOGFN + "', OGFN='" + modal.OGFN + "', Value='" + modal.Value+"' WHERE UserEmail='"+modal.UserEmail+"'";
//			_connection.Query<LoginModel> (query);
			//_connection.Table<LoginModel>().Where(x => x.UserEmail.Contains(modal.UserEmail));

			List<LoginModel> login = _connection.Table<LoginModel> ().Where (x => x.UserEmail.Contains (modal.UserEmail)).ToList ();

			foreach (LoginModel l  in login) {
				modal.GroupOGFN = l.GroupOGFN;
				modal.FamOGFN = l.FamOGFN;
				_connection.Delete (l);
			}

			_connection.Insert (modal);

		}

		private int IsUserExist(string filter)
		{
			if (filter == null)
				throw new ArgumentNullException ("filter");
			
			int count =  _connection.Table<User>().Where(x => x.UserID.Contains(filter)).ToList().Count();
			return count;
		}

		private int IsLoggedInUserExists(string filter)
		{
			if (filter == null)
				throw new ArgumentNullException ("filter");

			int count =  _connection.Table<LoginModel>().Where(x => x.UserEmail.Contains(filter)).ToList().Count();
			return count;
		}

		private int IsRelativeExist(string filter, string userId)
		{
			int count =  _connection.Table<People>().Where(x => x.UserID.Contains(filter) && x.LoginUserLinkID.Contains(userId)).ToList().Count();
			return count;
		}


		private int IsFbFriendExist(string filter, string userId)
		{
			int count =  _connection.Table<People>().Where(x => x.UserID.Contains(filter) && x.LoginUserLinkID.Contains(userId)).ToList().Count();
			return count;
		}

		public bool IsCelebStored()
		{
			int count = _connection.Table <Celebrity>().ToList().Count();
			return Convert.ToBoolean( count);
		}

		public int IsMemberExists(string filter, string userId)
		{
			int count =  _connection.Table<People>().Where(x => x.IndiOgfn.Contains(filter) && x.LoginUserLinkID.Contains(userId)).ToList().Count();
			return count;
		}

		public List<People> GetMember(string filter, string userId)
		{
			List<People> result =  _connection.Table<People>().Where(x => x.IndiOgfn.Contains(filter) && x.LoginUserLinkID.Contains(userId)).ToList();
			return result;
		}
		#endregion
	}
}

