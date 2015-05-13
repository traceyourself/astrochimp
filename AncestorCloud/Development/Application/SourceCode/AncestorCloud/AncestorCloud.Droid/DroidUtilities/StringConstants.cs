
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
		#region relation consts
		public static readonly string Brother_comparison = "brother";
		public static readonly string Sister_comparison = "sister";
		public static readonly string Father_comparison = "father";
		public static readonly string Mother_comparison = "mother";
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

	}
}

