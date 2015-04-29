﻿using System;
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
			_connection.CreateTable<User>();
			_connection.CreateTable<People>();
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


		#endregion

		#region Helper Methods

		private int IsUserExist(string filter)
		{
			if (filter == null)
				throw new ArgumentNullException ("filter");
			
			int count =  _connection.Table<User>().Where(x => x.UserID.Contains(filter)).ToList().Count();
			return count;
		}

		private int IsRelativeExist(string filter)
		{
			if (filter == null)
				throw new ArgumentNullException ("filter");

			int count =  _connection.Table<People>().Where(x => x.UserID.Contains(filter)).ToList().Count();
			return count;
		}


		#endregion



		
	}
}

