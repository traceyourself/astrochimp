using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Cirrious.CrossCore;

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

				family.Tag = AppConstant.FBTAGKEY;

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

			if(IsKeyExist (AppConstant.CONTACT_INFO, dict)){
				JObject contactObj = dict [AppConstant.CONTACT_INFO] as JObject;
				Dictionary<string,object> contactdict = contactObj.ToObject<Dictionary<string,object>> ();

				if (IsKeyExist (AppConstant.FIRSTNAMEKEY, contactdict))
					model.Name = GetData (AppConstant.FIRSTNAMEKEY, contactdict);

				if (IsKeyExist (AppConstant.LASTNAMEKEY, contactdict)) {
					String nn = model.Name;
					nn = nn+" "+GetData (AppConstant.LASTNAMEKEY, contactdict);
					model.Name = nn;
				}
			}

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

			if (IsKeyExist (AppConstant.AVATARINDIOGFN, dict))
				model.AvatarOGFN = GetData (AppConstant.AVATARINDIOGFN, dict);

			if (IsKeyExist (AppConstant.INDI_NAME, dict))
				model.Name = GetData (AppConstant.INDI_NAME, dict).Replace("/","");

			if (IsKeyExist (AppConstant.CHILDOGFNKEY, dict))
				model.FamOGFN = GetData (AppConstant.CHILDOGFNKEY, dict);
			

			return model;
		}

		public static People GetContactData(People model,string key , Dictionary<string,object> data)
		{
			if (ValidationClass.IsDataNull (data)) {
				Utility.Log ("In GetIndiData() data dictionary is null");
				return null;
			}

			JObject obj = data [key] as JObject;

			Dictionary<string,object> dict = obj.ToObject<Dictionary<string,object>> ();

			if (IsKeyExist (AppConstant.INDIOGFN, dict))
				model.IndiOgfn = GetData (AppConstant.INDIOGFN, dict);

			return model;
		}


		#endregion


		#region family member Indi read

		public static People GetIndiFamilyReadData(Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetIndiFamilyReadData() data dictionary is null");
				return null;
			}

			People peopleModel = new People ();

			if (IsKeyExist (AppConstant.Message, dataDic)) 
			{
				if (! GetData (AppConstant.Message, dataDic).Equals (AppConstant.SUCCESS)) 
				{
					return peopleModel;
				}
			}

			if (IsKeyExist (AppConstant.VALUE, dataDic))
				peopleModel = GetIndiFamilyData (peopleModel,AppConstant.VALUE,dataDic);

			return peopleModel;
		}

		private static People GetIndiFamilyData(People model,string key , Dictionary<string,object> data)
		{
			if (ValidationClass.IsDataNull (data)) {
				Utility.Log ("In GetIndiData() data dictionary is null");
				return null;
			}

			JObject obj = data [key] as JObject;

			Dictionary<string,object> dict = obj.ToObject<Dictionary<string,object>> ();

			if (IsKeyExist (AppConstant.INDIOGFN, dict))
				model.IndiOgfn = GetData (AppConstant.INDIOGFN, dict);

			if (IsKeyExist (AppConstant.INDI_GENDER, dict))
				model.Gender = GetData (AppConstant.INDI_GENDER, dict);

			if (IsKeyExist (AppConstant.INDI_NAME, dict))
				model.Name = GetData (AppConstant.INDI_NAME, dict);
			

			if (IsKeyExist (AppConstant.INDI_NAME2, dict)) {
				JObject nameObj = dict [AppConstant.INDI_NAME2] as JObject;

				Dictionary<string,object> nameDict = nameObj.ToObject<Dictionary<string,object>> ();

				if (IsKeyExist (AppConstant.INDI_GIVEN_NAME, nameDict))
					model.FirstName = GetData (AppConstant.INDI_GIVEN_NAME, nameDict);

				if (IsKeyExist (AppConstant.INDI_MIDDLE_NAME, nameDict))
					model.MiddleName = GetData (AppConstant.INDI_MIDDLE_NAME, nameDict);

				if (IsKeyExist (AppConstant.INDI_SURNAME, nameDict))
					model.LastName = GetData (AppConstant.INDI_SURNAME, nameDict);
			}


			return model;
		}

		#endregion


		#region get Family member response
		public static ResponseDataModel GetFamilyMembers(Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetFamilyMembers() data dictionary is null");
				return null;
			}
			ResponseDataModel modal = new ResponseDataModel ();

			if (IsKeyExist (AppConstant.CODE, dataDic))
				modal.Code = GetData (AppConstant.CODE,dataDic);

			if (IsKeyExist (AppConstant.Message, dataDic)) 
				modal.Message = GetData (AppConstant.Message, dataDic);

			if (IsKeyExist (AppConstant.VALUE, dataDic)) {
				JObject injob = dataDic [AppConstant.VALUE] as JObject;
				Dictionary<string, object> dataArray = injob.ToObject<Dictionary<string, object>> ();

				if(IsKeyExist(AppConstant.CHILDREN_OGFN,dataArray)){
					JArray inArr = dataArray [AppConstant.CHILDREN_OGFN] as JArray;
					List<string> childOgfnArr = inArr.ToObject<List<string>> ();
					string famOgfns = "";
					try{
						for(int i=0;i<childOgfnArr.Count;i++){
							if(i==0){
								famOgfns += childOgfnArr[i];
							}else{
								famOgfns += ","+childOgfnArr[i];
							}
						}
					}catch(Exception e){
						Mvx.Trace(e.StackTrace);
					}
					modal.value = famOgfns;
				}
			}

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


		#region Match History
		public static List<RelationshipFindResult> ReadDataHistory(Dictionary<string,object> dataDic)
		{
			if (ValidationClass.IsDataNull (dataDic)) {
				Utility.Log ("In GetUserReadData() data dictionary is null");
				return null;
			}

			List<RelationshipFindResult> resultModel = new List<RelationshipFindResult> ();

			if (IsKeyExist (AppConstant.Message, dataDic)) 
			{
				if (! GetData (AppConstant.Message, dataDic).Equals (AppConstant.SUCCESS)) 
				{
					return resultModel;
				}
			}

			if (IsKeyExist (AppConstant.VALUE, dataDic))
				resultModel = GetHistoryData (resultModel,AppConstant.VALUE,dataDic);

			return resultModel;
		}

		private static List<RelationshipFindResult> GetHistoryData(List<RelationshipFindResult> model,string key , Dictionary<string,object> data)
		{
			if (ValidationClass.IsDataNull (data)) {
				Utility.Log ("In GetHistoryData() data dictionary is null");
				return null;
			}

			JArray jar = data [key] as JArray;

			for(int i=0;i<jar.Count;i++){
			
				JObject obj = jar [i] as JObject;

				Dictionary<string,object> dict = obj.ToObject<Dictionary<string,object>> ();

				RelationshipFindResult inModel = new RelationshipFindResult ();

				if (IsKeyExist (AppConstant.HISTORY_DEGREES, dict))
					inModel.Degrees = int.Parse(GetData (AppConstant.HISTORY_DEGREES, dict));

				if (IsKeyExist (AppConstant.HISTORY_INDI_1_OGFN, dict))
					inModel.Indi1Ogfn = int.Parse(GetData (AppConstant.HISTORY_INDI_1_OGFN, dict));

				if (IsKeyExist (AppConstant.HISTORY_INDI_2_OGFN, dict))
					inModel.Indi2Ogfn = int.Parse(GetData (AppConstant.HISTORY_INDI_2_OGFN, dict));

				if (IsKeyExist (AppConstant.HISTORY_COMMON_INDI_OGFN, dict))
					inModel.CommonIndiOgfn = int.Parse(GetData (AppConstant.HISTORY_COMMON_INDI_OGFN, dict));

				if (IsKeyExist (AppConstant.HISTORY_FIRST_FIND_DATE, dict))
					inModel.FirstFindDate = GetData (AppConstant.HISTORY_FIRST_FIND_DATE, dict);

				if (IsKeyExist (AppConstant.HISTORY_LAST_FIND_DATE, dict))
					inModel.LastFindDate = GetData (AppConstant.HISTORY_LAST_FIND_DATE, dict);

				if (IsKeyExist (AppConstant.HISTORY_FOUND, dict))
					inModel.Found = bool.Parse(GetData (AppConstant.HISTORY_FOUND, dict));

				if (IsKeyExist (AppConstant.HISTORY_GROUP_OGFN, dict))
					inModel.GroupOgfn = int.Parse(GetData (AppConstant.HISTORY_GROUP_OGFN, dict));

				if (IsKeyExist (AppConstant.HISTORY_USEROGFN, dict))
					inModel.UserOgfn = int.Parse(GetData (AppConstant.HISTORY_USEROGFN, dict));

				if (IsKeyExist (AppConstant.History_DESC, dict))
					inModel.Description = GetData (AppConstant.History_DESC, dict);

				/*if (IsKeyExist (AppConstant.HISTORY_INDI_LIST1, dict))
					inModel.IndiList1 = GetData (AppConstant.HISTORY_INDI_LIST1, dict);

				if (IsKeyExist (AppConstant.HISTORY_INDI_LIST2, dict))
					inModel.IndiList2 = GetData (AppConstant.HISTORY_INDI_LIST2, dict);
				*/
				if (IsKeyExist (AppConstant.HISTORY_NOTFOUND_REASON, dict))
					inModel.NotFoundReason = GetData (AppConstant.HISTORY_NOTFOUND_REASON, dict);

				if (IsKeyExist (AppConstant.HISTORY_TYPE, dict))
					inModel.Type = GetData (AppConstant.HISTORY_TYPE, dict);

				model.Add (inModel);
			}

			return model;
		}
		#endregion

	}
}

