using System;
using System.Linq;
using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace AncestorCloud.Shared
{
	public class DatabaseService : IDatabaseService
	{
		private readonly ISQLiteConnection _connection;

		public DatabaseService(ISQLiteConnectionFactory factory)
		{
			_connection = factory.Create("database.db");

			CreateTables ();
		}

		public void CreateTables()
		{
			_connection.CreateTable<User>();
			_connection.CreateTable<People>();
			_connection.CreateTable<LoginModel>();
			_connection.CreateTable<Celebrity> ();
		}
			
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

			if (Convert.ToBoolean(IsRelativeExist(relative.UserID)))
				_connection.Update (relative);
			else
			 _connection.Insert(relative);
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

		public User GetUser(int id)
		{
			User user=  (User)_connection.Table<User> ().Where (x => x.Id.Equals (id));
			return user;
		}

		public List<User> GetUsers(string relationFilter)
		{
			if (relationFilter == null)
				throw new ArgumentNullException ("relationFilter");

			List<User> list = _connection.Table<User>().Where(x => x.UserID.Contains(relationFilter)).ToList();
			return list;
		}

		public List<People> RelativeMatching(string relationFilter)
		{
			if (relationFilter == null)
				throw new ArgumentNullException ("relationFilter");
			
			List<People> list = _connection.Table<People>().Where(x => x.Relation.Contains(relationFilter)).ToList();
			return list;
		}


		public void InsertRelatives(List<People> relatives)
		{
			if (relatives == null)
				throw new ArgumentNullException ("relatives");

			_connection.InsertAll (relatives);
		}

		public List<People> GetFamily()
		{
			List<People> list = _connection.Query<People> ("select * from People where Relation NOT LIKE '%friend%'");
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
			return login [0];
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

			List<Celebrity> list = _connection.Table<Celebrity>().Where(x => x.GivenNames.Contains(filter) || x.LastName.Contains(filter)).ToList();
			return list;
		}


		public void DropAllTables()
		{
			DropTables ();
			CreateTables ();
		}

		private void DropTables()
		{
			_connection.DropTable<LoginModel> ();
			_connection.DropTable<User>();
			_connection.DropTable<People>();
			_connection.DropTable<Celebrity> ();
		}

		#endregion

		#region Helper Methods


		private void UpdateLoginUser(LoginModel modal)
		{
			String query = "UPDATE LoginModel SET IndiOGFN='" + modal.IndiOGFN + "', OGFN='" + modal.OGFN + "', Value='" + modal.Value+"' WHERE UserEmail='"+modal.UserEmail+"'";
			_connection.Query<LoginModel> (query);

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

		private int IsRelativeExist(string filter)
		{
			if (filter == null)
				throw new ArgumentNullException ("filter");

			int count =  _connection.Table<People>().Where(x => x.UserID.Contains(filter)).ToList().Count();
			return count;
		}


		public bool IsCelebStored()
		{
			int count = _connection.Table <Celebrity>().ToList().Count();
			return Convert.ToBoolean( count);
		}

		#endregion



		
	}
}

