using System;
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
				Utility.Log ("In GetUserDetails() data dictionary is null");
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

		public static List<People> GetFbFamilyData(Dictionary<string,object> dataDic, User loginFbUser)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetFbFamilyData() data dictionary is null");
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

				family.LoginUserLinkID = loginFbUser.Email;

				familyList.Add(family);

			}
			return familyList;
		}

		public static List<People> GetFbFriendsData(Dictionary<string,object> dataDic, User loginFbUser)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetFbFriendsData() data dictionary is null");
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

				friend.LoginUserLinkID = loginFbUser.Email;

				friendList.Add(friend);

			}
			return friendList;
		}

		#endregion
		#region get login details
		public static LoginModel GetLoginDetails(Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetLoginDetails() data dictionary is null");
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

		public static LoginModel GetSignUpDetails(LoginModel modal,Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetSignUpDetails() data dictionary is null");
				return null;
			}

			 modal = modal ?? new LoginModel ();

			if (IsKeyExist (AppConstant.Message, dataDic))
				modal.Message = GetData (AppConstant.Message, dataDic);

			if (IsKeyExist (AppConstant.CODE, dataDic)) {
				if (!modal.Message.Equals (AppConstant.SUCCESS)) {
					//modal.OGFN = GetData (AppConstant.CODE, dataDic);
					//modal.IndiOGFN = GetData (AppConstant.VALUE, dataDic);

					return modal;
				}
				
			}
			if (IsKeyExist (AppConstant.VALUE, dataDic))
				modal.OGFN = GetData (AppConstant.VALUE, dataDic);

			if (IsKeyExist (AppConstant.VALUE, dataDic))
				modal.IndiOGFN = GetData (AppConstant.VALUE, dataDic);

			return modal;
		}
		#endregion

		#region User Read Data

		public static LoginModel GetUserReadData(LoginModel model, Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetUserReadData() data dictionary is null");
				return null;
			}

			model = model ?? new LoginModel ();

			if (IsKeyExist (AppConstant.Message, dataDic)) 
			{
				if (! GetData (AppConstant.Message, dataDic).Equals (AppConstant.SUCCESS)) 
				{
					return model;
				}
			}

			if (IsKeyExist (AppConstant.VALUE, dataDic))
				model = GetUserData (model,AppConstant.VALUE,dataDic);

			return model;
		}

		private static LoginModel GetUserData(LoginModel model,string key , Dictionary<string,object> data)
		{
			if (ValidationClass.IsDataNull (data)) {
				Utility.Log ("In GetUserData() data dictionary is null");
				return null;
			}

			JObject obj = data [key] as JObject;
			Dictionary<string,object> dict = obj.ToObject<Dictionary<string,object>> ();

			if (IsKeyExist (AppConstant.INDIOGFN, dict))
				model.IndiOGFN = GetData (AppConstant.INDIOGFN, dict);

			if (IsKeyExist (AppConstant.OGFN, dict))
				model.OGFN = GetData (AppConstant.OGFN, dict);

			return model;
		}


		#endregion


		#region Avatar parser

		public static LoginModel GetAvatarAvailabiltyData(Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetAvatarAvailabiltyData() data dictionary is null");
				return null;
			}

			LoginModel model = new LoginModel ();

			if (IsKeyExist (AppConstant.Message, dataDic)) 
			{
				if (! GetData (AppConstant.Message, dataDic).Equals (AppConstant.SUCCESS)) 
				{
					return model;
				}
			}

			if (IsKeyExist (AppConstant.VALUE, dataDic)) 
			{

				JArray array = dataDic [AppConstant.VALUE] as JArray;

				List<Dictionary<string, object>> dataArray = array.ToObject<List<Dictionary<string, object>>> ();

				string mediaOgfn = "";

				foreach(Dictionary<string,object> data in dataArray)
				{
					if (IsKeyExist (AppConstant.FILE_TYPE, data))
					{
						string filetype = GetData (AppConstant.FILE_TYPE, data);
						if(filetype.Contains("Png") || filetype.Contains("png") || filetype.Contains("Jpeg") || filetype.Contains("jpeg")){
							if(IsKeyExist (AppConstant.MEDIA_OGFN, data))
							{
								mediaOgfn = GetData (AppConstant.MEDIA_OGFN, data);
								break;
							}
						}
					}
				}
			
				model.AvatarOGFN = mediaOgfn;

				return model;
			}

			return model;
		}


		#endregion

	
		#region GetIndiData

		public static LoginModel GetIndiReadData(LoginModel model, Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetIndiReadData() data dictionary is null");
				return null;
			}

			model = model ?? new LoginModel ();

			if (IsKeyExist (AppConstant.Message, dataDic)) 
			{
				if (! GetData (AppConstant.Message, dataDic).Equals (AppConstant.SUCCESS)) 
				{
					return model;
				}
			}

			if (IsKeyExist (AppConstant.VALUE, dataDic))
				model = GetIndiData (model,AppConstant.VALUE,dataDic);

			return model;
		}
		private static LoginModel GetIndiData(LoginModel model,string key , Dictionary<string,object> data)
		{
			if (ValidationClass.IsDataNull (data)) {
				Utility.Log ("In GetIndiData() data dictionary is null");
				return null;
			}

			JObject obj = data [key] as JObject;

			Dictionary<string,object> dict = obj.ToObject<Dictionary<string,object>> ();

			if (IsKeyExist (AppConstant.INDIOGFN, dict))
				model.AvatarOGFN = GetData (AppConstant.AVATARINDIOGFN, dict);

			return model;
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

			if (data [key] == null)
				return "";
			
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

		#region get add member response
		public static ResponseDataModel GetAddMemberDetails(Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetAddMemberDetails() data dictionary is null");
				return null;
			}
			ResponseDataModel modal = new ResponseDataModel ();

			if (IsKeyExist (AppConstant.CODE, dataDic)) 
				modal.Code = GetData (AppConstant.CODE, dataDic);

			if (IsKeyExist (AppConstant.Message, dataDic))
				modal.Message = GetData (AppConstant.Message, dataDic);

			if (IsKeyExist (AppConstant.VALUE, dataDic))
				modal.value = GetData (AppConstant.VALUE, dataDic);

			return modal;
		}
		#endregion

		#region get add member relation response
		public static ResponseDataModel GetAddMemberRelationDetails(Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetAddMemberRelationDetails() data dictionary is null");
				return null;
			}
			ResponseDataModel modal = new ResponseDataModel ();

			if (IsKeyExist (AppConstant.CODE, dataDic)) 
				modal.Code = GetData (AppConstant.CODE, dataDic);

			if (IsKeyExist (AppConstant.Message, dataDic))
				modal.Message = GetData (AppConstant.Message, dataDic);

			return modal;
		}
		#endregion
	}
}

