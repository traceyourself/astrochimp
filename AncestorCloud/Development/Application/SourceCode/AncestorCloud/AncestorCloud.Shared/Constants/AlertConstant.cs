using System;

namespace AncestorCloud.Shared
{
	public class AlertConstant
	{
		#region GeneralConstants

		public const string INTERNET_ERROR="Network not available";
		public const string INTERNET_ERROR_MESSAGE = "Please check internet connection";
		public const string SUCCESS_RESPONSE_ALERT="Family member added successfully";
		public const string SUCCESS_ALERT="Success";
		public const string SUCCESS_ERROR="Error";
		public const string SUCCESS_ERROR_MESSAGE="Failed to add member, Please try Again";

		#endregion

		#region AddFamilyAlert

		public const string NAME_ALERT="First Name Missing";
		public const string NAME_ALERT_MESSAGE="First Name is required, Please enter a value for the field";
		public const string GENDER_ALERT="Gender Missing";
		public const string GENDER_ALERT_MESSAGE="Gender is required, please select a value.";

		#endregion

		#region ContactAlert

		public const string CONTACT_SUCCESS="Success";
		public const string CONTACT_SUCCESS_MESSAGE="Contact has been added successfully";
		public const string CONTACT_MATCH="Match";
		public const string CONTACT_MATCH_SUCCESS="No relation found with the User. Do you want to invite him?";

		#endregion

		#region FacebookAlert

		public const string FB_SUCCESS="Success";
		public const string FB_SUCCESS_MESSAGE="Friend has been added successfully. Please tap on Ok to Select";
		public const string FB_ERROR="Error";
		public const string FB_ERROR_MESSAGE="Unable to communicate with the Server. Please try again";
		public const string FB_MATCH="Match";
		public const string FB_MATCH_MESSAGE="Do you want to add your friend to Ancestor Cloud";


		#endregion

		#region LoginAlert

		public const string LOGIN_ERROR="Login Error";
		public const string LOGIN_ERROR_MESSAGE="Username or Password is incorrect";
		public const string LOGIN_EMAIL_ERROR="Email Missing";
		public const string LOGIN_EMAIL_MESSAGE="Please enter your Email Address.";
		public const string LOGIN_EMAIL_INVALID="Email Invalid";
		public const string LOGIN_EMAIL_INVALID_MESSAGE="Email Address is not valid.";
		public const string LOGIN_PASSWORD_ERROR="Password Missing";
		public const string LOGIN_PASSWORD_ERROR_MESSAGE="Please enter your password.";
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
		public const string EDIT_SUCCESS_MESSAGE="Family member has been updated successfully";
		public const string EDIT_ERROR="Error";
		public const string EDIT_ERROR_MESSAGE="Not able to update the profile.Please try Again.";

		#endregion

		#region ProfilePicAlert

		public const string PROFILE_PIC_SUCCESS="Success";
		public const string PROFILE_PIC_SUCCESS_MESSAGE="Profile pic has been uploaded successfully";
		public const string PROFILE_PIC_ERROR="Upload Error";
		public const string PROFILE_PIC_ERROR_MESSAGE="Not able to upload profile pic, please try again.";

		#endregion

		#region SignUpAlert

		public const string SIGNUP_ERROR="SignUp Error";
		public const string SIGNUP_ERROR_MESSAGE="An error has occurred. Please try again";
		public const string SIGNUP_FIRSTNAME_ERROR="First Name Missing";
		public const string SIGNUP_FIRSTNAME_ERROR_MESSAGE="Please enter your first name.";
		public const string SIGNUP_SECNAME_ERROR="Second Name Missing";
		public const string SIGNUP_SECNAME_ERROR_MESSAGE="Please enter your last name.";
		public const string SIGNUP_EMAIL_ERROR="Email Missing";
		public const string SIGNUP_EMAIL_MESSAGE="Please enter your email address.";
		public const string SIGNUP_EMAIL_INVALID="Email Invalid";
		public const string SIGNUP_EMAIL_INVALID_MESSAGE="Email address is not valid.";
		public const string SIGNUP_PASSWORD_ERROR="Password Missing";
		public const string SIGNUP_PASSWORD_ERROR_MESSAGE="Please enter your Password.";
		public const string SIGNUP_RESPONSE_ERROR="ERROR";
		public const string SIGNUP_RESPONSE_ERROR_MESSAGE="An error has occurred. Please try again.";

		#endregion



	}
}

