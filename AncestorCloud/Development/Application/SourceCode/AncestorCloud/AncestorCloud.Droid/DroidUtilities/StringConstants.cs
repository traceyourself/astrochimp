
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AncestorCloud.Droid
{
	
	public class StringConstants
	{

		#region Twitter Constants
		public static readonly string TWITTER_KEY = "SD9KnCinDrqxJZ7eRTl6BbD77";
		public static readonly string TWITTER_SECRET = "unJjpf51B5Ad3Lxt5I1qPfQj8u1SYE4cdXzk2vTkFJOkaszfQQ";
		public static readonly string TWITTER_CALLBACK_URL = "callback://twitter";
		#endregion

		#region research help url
		public static readonly string RESEARCH_HELP_URL = "http://www.mocavo.com/";
		#endregion

		#region relation consts
		public static readonly string Brother_comparison = "brother";
		public static readonly string Sister_comparison = "sister";
		public static readonly string Father_comparison = "father";
		public static readonly string Mother_comparison = "mother";

		public static readonly string Sibling_comparison = "Sibling";
		public static readonly string Parent_comparison = "Parent";
		public static readonly string GrandParent_comparison = "Grandparent";
		public static readonly string GreatGrandParent_comparison = "GreatGrandparent";

		public static readonly string GrandFather_comparison = "grandfather";
		public static readonly string GrandMother_comparison = "grandmother";
		public static readonly string GreatGrandFather_comparison = "great grandfather";
		public static readonly string GreatGrandMother_comparison = "great grandmother";
		#endregion

		#region Relation constants for adding relation service
		public static readonly string FATHER_RELATIONSHIP = "FATHER_RELATIONSHIP";
		public static readonly string MOTHER_RELATIONSHIP = "MOTHER_RELATIONSHIP";
		public static readonly string HUSBAND_RELATIONSHIP = "HUSBAND_RELATIONSHIP";
		public static readonly string WIFE_RELATIONSHIP = "WIFE_RELATIONSHIP";
		public static readonly string CHILD_RELATIONSHIP = "CHILD_RELATIONSHIP";
		public static readonly string SIBLING_RELATIONSHIP = "SIBLING_RELATIONSHIP";
		public static readonly string DAUGHTER_RELATIONSHIP = "DAUGHTER_RELATIONSHIP";
		public static readonly string SON_RELATIONSHIP = "SON_RELATIONSHIP";
		public static readonly string SISTER_RELATIONSHIP = "SISTER_RELATIONSHIP";
		public static readonly string BROTHER_RELATIONSHIP = "BROTHER_RELATIONSHIP";
		public static readonly string UNKNOWN_RELATIONSHIP = "UNKNOWN_RELATIONSHIP";
		#endregion


		#region Uploads Pic Const
		public static readonly string DIRECTORY_NAME = "Ancestor Cloud";
		#endregion


		#region Family Consts
		public static readonly string MALE = "Male";
		public static readonly string FEMALE = "Female";
		#endregion


		#region sending message to contacts Constants
		public static readonly string SMS_TO = "smsto:";
		public static readonly string SMS_BODY = "sms_body";
		public static readonly string SMS_CONTENT = "Come join Cousin App and see how we're related!";
		#endregion

		#region FB constants
		public static readonly string GET_METHOD_TYPE = "GET";
		public static readonly string FB_GRAPH_ME_URL = "https://graph.facebook.com/me";
		public static readonly string FB_GRAPH_FAMILY_URL = "https://graph.facebook.com/me/family";
		public static readonly string FB_GRAPH_TAGGABLE_FRIENDS_URL = "https://graph.facebook.com/me/taggable_friends";
		#endregion

		public static readonly string DEGREE_SYMBOL = "º";

		#region picture const
		public static readonly string PHOTO_NAME = "myPhoto_";
		public static readonly string PHOTO_EXTENSION = ".jpg";
		#endregion

	}
}

