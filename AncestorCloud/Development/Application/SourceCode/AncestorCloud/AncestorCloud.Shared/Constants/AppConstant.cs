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

		#endregion

		#region Login Constants
		public const string CODE = "Code";
		public const string Message = "Message";
		public const string VALUE = "Value";
		#endregion

	}


}

