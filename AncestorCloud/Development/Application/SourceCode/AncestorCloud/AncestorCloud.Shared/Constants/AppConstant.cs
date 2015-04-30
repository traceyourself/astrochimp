﻿using System;

namespace AncestorCloud.Shared
{
	public enum MediaType
	{
		Photo,
		Video
	}

	public enum ResponseStatus
	{
		OK,
		Fail,
		AuthorisationRequired
	}

	static public class AppConstant
	{
		#region FB Constants
		public const string FBAPIKEY = "591314537670509";
		public const string FBAPISECRETKEY = "659bacca4a45654358bb632f5607eeb0";
		public const string FBSCOPE = "publish_stream,email,publish_actions,user_birthday,user_friends,user_relationships,friends_relationships";

		public const string FRIENDKEY = "friend";
		#endregion


		#region Web service constants
		public const string DEVELOPERID = "AncestorCloud";
		public const string DEVELOPERPASSWORD = "492C4DD9-A129-4146-BAE9-D0D45FBC315C";

		public const string ID = "id";
		public const string BIRTHDAY = "birthday";
		public const string EMAIL = "email";
		public const string FIRSTNAME = "first_name";
		public const string LASTNAME = "last_name";
		public const string GENDER = "gender";
		public const string NAME = "name";
		public const string FBLINK = "link";
		public const string UPDATEDTIME = "updated_time";
		public const string RELATIONSHIP = "relationship";
		public const string DATAKEY = "data";
		public const string PICTURE = "picture";
		public const string URL = "url";
		public const string SESSIONID = "SessionId";
		public const string SUCCESS = "Success";
		#endregion


		#region Webservice URL Constants
		public const string BASEURL = "https://wsdev.onegreatfamily.com/v11.02";
		public const string USEREADSERVICE = "User.svc/Read";
		public const string ADD_PEOPLE_SERVICE = "Individual.svc/CreateUpdate";
		public const string ADD_RELATION_SERVICE = "Individual.svc/AddIndiAsRelation";

		#endregion

		#region Login Constants
		public const string CODE = "Code";
		public const string Message = "Message";
		public const string VALUE = "Value";
		#endregion

		#region User Read Data
		public const string INDIOGFN = "IndiOgfn";
		public const string OGFN = "Ogfn";

		#endregion

		#region ADD PERSON CONSTS
		public const string ADD_PERSON_SESSION_ID = "SessionId";
		public const string ADD_PERSON_INDIOGFN = "IndiOgfn";
		public const string ADD_PERSON_NAME = "Name";
		public const string ADD_PERSON_GENDER = "Gender";
		public const string ADD_PERSON_BIRTHDATE = "BirthDate";
		public const string ADD_PERSON_BIRTHPLACE = "BirthPlace";
		#endregion
	
		#region ADD PERSON Relation
		public const string ADD_RELATION_INDIOGFN = "RelatedIndiOgfn";
		public const string ADD_RELATION_TYPE = "RelationshipType";
		public const string FATHER_RELATIONSHIP = "FATHER_RELATIONSHIP";
		public const string MOTHER_RELATIONSHIP = "MOTHER_RELATIONSHIP";
		public const string HUSBAND_RELATIONSHIP = "HUSBAND_RELATIONSHIP";
		public const string WIFE_RELATIONSHIP = "WIFE_RELATIONSHIP";
		public const string CHILD_RELATIONSHIP = "CHILD_RELATIONSHIP";
		public const string SIBLING_RELATIONSHIP = "SIBLING_RELATIONSHIP";
		public const string DAUGHTER_RELATIONSHIP = "DAUGHTER_RELATIONSHIP";
		public const string SON_RELATIONSHIP = "SON_RELATIONSHIP";
		public const string SISTER_RELATIONSHIP = "SISTER_RELATIONSHIP";
		public const string BROTHER_RELATIONSHIP = "BROTHER_RELATIONSHIP";
		public const string UNKNOWN_RELATIONSHIP = "UNKNOWN_RELATIONSHIP";
		#endregion



	}

}