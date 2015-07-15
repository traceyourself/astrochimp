using System;

namespace AncestorCloud.Shared
{
	public class AlertConstant
	{
		#region GeneralConstants

		public const string INTERNET_ERROR="Network not available";
		public const string INTERNET_ERROR_MESSAGE = "Please check internet connection";
		public const string SUCCESS_RESPONSE_ALERT="Successfully Added";
		public const string SUCCESS_ALERT="Success";
		public const string SUCCESS_ERROR="Error";
		public const string SUCCESS_ERROR_MESSAGE="Failed to add, Please try Again...";

		#endregion

		#region AddFamilyAlert

		public const string NAME_ALERT="First Name Missing";
		public const string NAME_ALERT_MESSAGE="First Name is required, please enter a value for the field.";
		public const string GENDER_ALERT="Gender Missing";
		public const string GENDER_ALERT_MESSAGE="Gender is required, please select a value.";

		#endregion

		#region ContactAlert

		public const string CONTACT_SUCCESS="Success";
		public const string CONTACT_SUCCESS_MESSAGE="Contact added to Ancestor Cloud. Tap Ok to Select";
		public const string CONTACT_MATCH="Match";
		public const string CONTACT_MATCH_SUCCESS="No relation found with Ancestor Cloud. Do you want to invite him?";

		#endregion

		#region FacebookAlert

		public const string FB_SUCCESS="Success";
		public const string FB_SUCCESS_MESSAGE="Friend added to Ancestor Cloud. Tap Ok to Select";
		public const string FB_ERROR="Error";
		public const string FB_ERROR_MESSAGE="Unable to communicate with Ancestor Cloud";
		public const string FB_MATCH="Match";
		public const string FB_MATCH_MESSAGE="Do you want to add your friend to Ancestor Cloud";


		#endregion

		#region LoginAlert

		public const string LOGIN_ERROR="Login Error";
		public const string LOGIN_ERROR_MESSAGE="Invalid user signon username or password.";
		public const string LOGIN_EMAIL_ERROR="Email Missing";
		public const string LOGIN_EMAIL_MESSAGE="Email is required, please enter a value for the field.";
		public const string LOGIN_EMAIL_INVALID="Email Invalid";
		public const string LOGIN_EMAIL_INVALID_MESSAGE="Email you entered is not valid.";
		public const string LOGIN_PASSWORD_ERROR="Password Missing";
		public const string LOGIN_PASSWORD_ERROR_MESSAGE="Password is required, please enter a value for the field.";
		public const string LOGIN_RESPONSE_ERROR="ERROR";
		public const string LOGIN_RESPONSE_ERROR_MESSAGE="An error has occurred. Please try again.";
		public const string AUTO_LOGIN_RESPONSE_ERROR_MESSAGE="An error has occurred. Please login again.";

		#endregion

		#region MatchAlert

		public const string MATCH_ERROR="Matcher";
		public const string MATCH_ERROR_MESSAGE="Oops!! No match found. Try again";
		public const string MATCH_FIRST_PERSON_ERROR="Person not selected";
		public const string MATCH_FIRST_PERSON_MESSAGE="Please Select First Person to match.";
		public const string MATCH_SEC_PERSON_ERROR="Person not selected";
		public const string MATCH_SEC_PERSON_ERROR_MESSAGE="Please Select Second Person to match.";

		#endregion

		#region EditAlert

		public const string EDIT_SUCCESS="Success";
		public const string EDIT_SUCCESS_MESSAGE="Successfully Edited";
		public const string EDIT_ERROR="Error";
		public const string EDIT_ERROR_MESSAGE="Failed to edit, Please try Again...";

		#endregion

		#region ProfilePicAlert

		public const string PROFILE_PIC_SUCCESS="Success";
		public const string PROFILE_PIC_SUCCESS_MESSAGE="Profile pic uploaded successfully";
		public const string PROFILE_PIC_ERROR="Upload Error";
		public const string PROFILE_PIC_ERROR_MESSAGE="Profile pic upload unsuccessfull";

		#endregion

		#region SignUpAlert

		public const string SIGNUP_ERROR="SignUp Error";
		public const string SIGNUP_ERROR_MESSAGE="Error signup user";
		public const string SIGNUP_FIRSTNAME_ERROR="First Name Missing";
		public const string SIGNUP_FIRSTNAME_ERROR_MESSAGE="First Name is required, please enter a value for the field.";
		public const string SIGNUP_SECNAME_ERROR="Second Name Missing";
		public const string SIGNUP_SECNAME_ERROR_MESSAGE="Last Name is required, please enter a value for the field.";
		public const string SIGNUP_EMAIL_ERROR="Email Missing";
		public const string SIGNUP_EMAIL_MESSAGE="Email is required, please enter a value for the field.";
		public const string SIGNUP_EMAIL_INVALID="Email Invalid";
		public const string SIGNUP_EMAIL_INVALID_MESSAGE="Email you entered is not valid.";
		public const string SIGNUP_PASSWORD_ERROR="Password Missing";
		public const string SIGNUP_PASSWORD_ERROR_MESSAGE="Password is required, please enter a value for the field.";
		public const string SIGNUP_RESPONSE_ERROR="ERROR";
		public const string SIGNUP_RESPONSE_ERROR_MESSAGE="An error has occurred. Please try again.";

		#endregion



	}
}

