﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace AncestorCloud.Shared
{
	public static class DataParser
	{

		#region FaceBook Data
		public static User GetUserDetails(Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetProductModel() data dictionary is null");
				return null;
			}

			User user = new User ();

			if (IsKeyExist (AppConstant.ID, dataDic)) 
				user.UserID = GetData (AppConstant.ID, dataDic);
			
			if (IsKeyExist (AppConstant.BIRTHDAY, dataDic))
				user.DateOfBirth = GetData (AppConstant.BIRTHDAY, dataDic);

			if (IsKeyExist (AppConstant.NAME, dataDic))
				user.Name = GetData (AppConstant.NAME, dataDic);

			if (IsKeyExist (AppConstant.EMAIL, dataDic))
				user.Email = GetData (AppConstant.EMAIL, dataDic);

			if (IsKeyExist (AppConstant.GENDER, dataDic))
				user.Gender = GetData (AppConstant.GENDER, dataDic);

			if (IsKeyExist (AppConstant.FIRSTNAME, dataDic))
				user.FirstName = GetData (AppConstant.FIRSTNAME, dataDic);

			if (IsKeyExist (AppConstant.LASTNAME, dataDic))
				user.LastName = GetData (AppConstant.LASTNAME, dataDic);

			if (IsKeyExist (AppConstant.FBLINK, dataDic))
				user.FbLink = GetData (AppConstant.FBLINK, dataDic);

			if (IsKeyExist (AppConstant.UPDATEDTIME, dataDic))
				user.UpdatedTime = GetData (AppConstant.UPDATEDTIME, dataDic);



			return user;
		}

		public static List<People> GetFbFamilyData(Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetProductModel() data dictionary is null");
				return null;
			}

			List<People> familyList = new List<People>();

			JArray array = dataDic ["data"] as JArray;

			List<Dictionary<string, object>> dataArray = array.ToObject<List<Dictionary<string, object>>> ();

			foreach(Dictionary<string,object> data in dataArray)
			{
				People family = new People();

				if (IsKeyExist (AppConstant.ID, data))
					family.UserID = GetData (AppConstant.ID, data);

				if (IsKeyExist (AppConstant.NAME, data))
					family.Name = GetData (AppConstant.NAME, data);

				if (IsKeyExist (AppConstant.RELATIONSHIP, data))
					family.Relation = GetData (AppConstant.RELATIONSHIP, data);

				family.IsSelected = false;

				familyList.Add(family);

			}
			return familyList;
		}

		public static List<People> GetFbFriendsData(Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetProductModel() data dictionary is null");
				return null;
			}

			List<People> friendList = new List<People>();

			JArray array = dataDic [AppConstant.DATAKEY] as JArray;

			List<Dictionary<string, object>> dataArray = array.ToObject<List<Dictionary<string, object>>> ();

			foreach(Dictionary<string,object> data in dataArray)
			{
				People friend = new People();

				if (IsKeyExist (AppConstant.ID, data))
					friend.UserID = GetData (AppConstant.ID, data);

				if (IsKeyExist (AppConstant.NAME, data))
					friend.Name = GetData (AppConstant.NAME, data);

				friend.Relation = AppConstant.FRIENDKEY;

				if (IsKeyExist (AppConstant.PICTURE, data))
					friend.ProfilePicURL = GetPicUrl (AppConstant.PICTURE, data);

				friend.IsSelected = false;

				friendList.Add(friend);

			}
			return friendList;
		}

		#endregion
		#region get login details
		public static LoginModel GetLoginDetails(Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In LoginModel() data dictionary is null");
				return null;
			}
			LoginModel modal = new LoginModel ();

			if (IsKeyExist (AppConstant.CODE, dataDic)) 
				modal.Code = GetData (AppConstant.CODE, dataDic);

			if (IsKeyExist (AppConstant.Message, dataDic))
				modal.Message = GetData (AppConstant.Message, dataDic);

			if (IsKeyExist (AppConstant.VALUE, dataDic))
				modal.Value = GetData (AppConstant.VALUE, dataDic);

			return modal;
		}
		#endregion


		#region get SignUp details

		public static LoginModel GetSignUpDetails(Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In LoginModel() data dictionary is null");
				return null;
			}

			LoginModel modal = new LoginModel ();

			if (IsKeyExist (AppConstant.CODE, dataDic)) 
				modal.Code = GetData (AppConstant.CODE, dataDic);

			if (IsKeyExist (AppConstant.Message, dataDic))
				modal.Message = GetData (AppConstant.Message, dataDic);

			if (IsKeyExist (AppConstant.VALUE, dataDic))
				modal.Value = GetData (AppConstant.VALUE, dataDic);

			return modal;
		}
		#endregion


		#region helper Methods

		private static bool IsKeyExist(string key , Dictionary<string,object> data)
		{
			if (key == null || key.Trim ().Length == 0 || data == null)
				return false;

			if (data.ContainsKey (key))
				return true;

			return false;
		}

		private static string GetData(string key , Dictionary<string,object> data)
		{
			if (key == null || key.Trim ().Length == 0 || data == null)
				return null;

			string dataString = data [key].ToString();

			if (dataString == null || dataString.Trim ().Length == 0)
			return "";		else
				return dataString.Trim ();
		}

		private static string GetPicUrl(string key , Dictionary<string,object> data)
		{
			if (key == null || key.Trim ().Length == 0 || data == null)
				return null;

			JObject obj = data [key] as JObject;
			Dictionary<string,object> dict = obj.ToObject<Dictionary<string,object>> ();

			Dictionary<string,object> subDict = new Dictionary<string, object>();
			if (IsKeyExist (AppConstant.DATAKEY, dict)) {
				JObject objt = dict [AppConstant.DATAKEY] as JObject;
				subDict =  objt.ToObject<Dictionary<string,object>> ();
			}

			string picURL = "";
			if (IsKeyExist (AppConstant.URL, subDict))
				picURL = subDict[AppConstant.URL].ToString() ;

			return picURL ;
		}

		#endregion
	}
}

