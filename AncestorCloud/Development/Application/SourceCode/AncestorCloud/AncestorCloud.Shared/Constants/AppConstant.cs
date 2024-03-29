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

	public enum AlertType
	{
		OK,
		OKCancelPermit,
		OKCancelSelect,
		OKCancelSelectContact,
		OKCancelSelectInvite,
		Logout
	}

	public enum ControllerType
	{
		None,
		Primary,
		Secondary
	}

	public enum FamilyType
	{
		Parent,
		GParent,
		GGParent
	}

	static public class AppConstant
	{

		public const string DEVELOPER_NOT_LOGIN_CODE = "27011";


		#region FB Constants
		public const string FBAPIKEY = "1427298800906536";//1428257997477283//591314537670509//1427298800906536
		public const string FBAPISECRETKEY = "bb85e6307b0ac87085e8cd8f250977e4";//141797ed935391946dd464c525495a0b//659bacca4a45654358bb632f5607eeb0//bb85e6307b0ac87085e8cd8f250977e4
		public const string FBSCOPE = "email,publish_actions,user_friends,user_relationships";
//		public const string FBSCOPE = "email,publish_actions,user_friends";
		public const string FRIENDKEY = "friend";

		public const string FBTAGKEY = "facebook";
		#endregion

		#region Contact Constants

		public const string CONTACTKEY = "DeviceContact";
		public const string METAGKEY = "Me";

		#endregion

		#region Google Analytics Constants

		public const string GATRACKINGID = "UA-62874372-1";

		#endregion


		#region Web service constants
		public const string DEVELOPERID = "AncestorCloud";
		public const string DEVELOPERPASSWORD = "492C4DD9-A129-4146-BAE9-D0D45FBC315C";
		public const string PRODUCTID = "COUSIN_USER";//COUSIN_USER//OGF_YEARLY_TRIAL7
		public const string DEVUSEREMAIL = "mikeyamadeo@gmail.com";
		public const string DEVUSERPASSWORD = "password";

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
		public const string STACKTRACE = "stacktrace";
		public const string TRUE = "true";
		public const string FILE_TYPE = "FileType";
		public const string PNG = "Png";

		public const string FAMILY_OGFN = "famogfn";
		public const string CHILDREN_OGFN = "ChildrenOgfns";

		public const string HUSBAND_OGFN = "HusbandOgfn";
		public const string WIFE_OGFN = "WifeOgfn";

		public const string MALE = "Male";
		public const string FEMALE = "Female";

		#endregion


		#region Webservice URL Constants

		public const string BASEURL = "https://ws.onegreatfamily.com/v11.02";//ws.onegreatfamily.com/v11.02//wsdev.onegreatfamily.com/v11.02
		public const string USEREADSERVICE = "User.svc/Read";
		public const string USERSIGNINSERVICE = "User.svc/Create";
		public const string USERLOGINSERVICE = "User.svc/Signin";
		public const string DEVELOPERLOGINSERVICE = "Developer.svc/Login";

		public const string ADD_PEOPLE_SERVICE = "Individual.svc/CreateUpdate";
		public const string ADD_RELATION_SERVICE = "Individual.svc/AddIndiAsRelation";
		public const string RELATIONSHIP_MATCH_SERVICE = "Individual.svc/RelationshipFind";
		public const string UPLOAD_MEDIA_SERVICE = "Individual.svc/MediaCreate";
		public const string INDIVIDUAL_READ_SERVICE = "Individual.svc/Read";
		public const string GROUP_CREATE_SERVICE = "Group.svc/Create";
		public const string FAMILY_CREATE_SERVICE = "Family.svc/Create";
		public const string MEDIA_LISTREAD_SERVICE = "Individual.svc/MediaListRead";
		public const string AVATAR_IMAGE_SERVICE = "Media.svc/AvatarRead";

		public const string FAMILY_READ_SERVICE = "Family.svc/Read";
		public const string MATCHER_HISTORY_SERVICE = "Individual.svc/RelationshipFindHistoryGet";

		public const string PERCENTAGE_COMPLETE_SERVICE = "User.svc/ProfileRead";

		#endregion

		#region Login Constants
		public const string CODE = "Code";
		public const string Message = "Message";
		public const string VALUE = "Value";
		#endregion

		#region User Read Data
		public const string INDIOGFN = "IndiOgfn";
		public const string OGFN = "Ogfn";
		public const string CONTACT_INFO = "ContactInfo";
		#endregion

		#region Indi Read Data
		public const string AVATARINDIOGFN = "AvatarId";
		public const string AVATARINDIOGFNKEY = "avatarOgfn";
		public const string IMAGETYPEKEY = "ImageType";
		public const string IMAGESIZEKEY = "ImageSize";

		public const string MEDIA_OGFN = "MediaOGFN";
		public const string AVATAR_OGFN = "avatarOgfn";
		public const string IMAGE_TYPE = "ImageType";
		public const string IMAGE_SIZE = "ImageSize";
		public const string IMAGE_WIDTH = "200";
		public const string IMAGE_HEIGHT = "200";

		public const string IMAGESIZE = "300%2c300";
		public const string CHILDOGFNKEY = "ChildFamilyOgfn";

		public const string BIRTH_DATE = "BirthDate";
		public const string BIRTH_PLACE = "BirthPlace";
		public const string INDI_GENDER = "Gender";
		public const string INDI_NAME = "Name";
		public const string INDI_GIVEN_NAME = "GivenName";
		public const string INDI_MIDDLE_NAME = "MiddleNames";
		public const string INDI_SURNAME = "Surname";
		public const string INDI_NAME2 = "Name2";

	
		#endregion


		#region SignUp service Constants
		public const string USERNAMEKEY = "username";
		public const string PASSWORDKEY = "Password";
		public const string DEVELOPERIDKEY = "DeveloperId";
		public const string DEVELOPERPASSWORDKEY = "DeveloperPassword";
		public const string FIRSTNAMEKEY = "FirstName";
		public const string LASTNAMEKEY = "LastName";
		public const string EMAILKEY = "EmailAddress";
		public const string PRODUCTIDKEY = "ProductId";
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
		public const string FAMOGFNKEY = "FamilyOgfn";
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

		public const string Sibling_comparison = "Sibling";
		public const string Parent_comparison = "Parent";
		public static readonly string GrandParent_comparison = "Grandparent";
		public static readonly string GreatGrandParent_comparison = "GreatGrandparent";
		public const string Father_comparison = "father";
		public const string Mother_comparison = "mother";

		public const string Mother_Reference = "mother_ref";
		public const string Father_Reference = "father_ref";
		public const string Grand_Mother_Mother_Reference = "grand_mother_mother_ref";
		public const string Grand_Father_Mother_Reference = "grand_father_mother_ref";
		public const string Grand_Mother_Father_Reference = "grand_mother_father_ref";
		public const string Grand_Father_Father_Reference = "grand_father_father_ref";

		#endregion

		#region Match Service

		public const string INDIOGFN1KEY= "IndiOgfn1";
		public const string INDIOGFN2KEY= "IndiOgfn2";
		public const string TYPEKEY= "Type";
		public const string MATCHTYPE = "ANY";

		#endregion

		#region Fb Link

		public const string LINKIDKEY= "linkId";
		public const string LINKTYPEKEY= "linkType";
		public const string LINKTYPE = "FACEBOOK_OGF";
		public const string KIN2_LINKTYPE = "FACEBOOK_KIN2";
		#endregion

		#region Media Upload 

		public const string FILENAMEKEY= "FileName";
		public const string FILENAME= "ProfilePic.jpg";
		public const string FILETYPEKEY = "FileType";
		public const string MEDIATYPEKEY = "MediaType";
		public const string AVATAR = "AVATAR";
		public const string FILETYPE = "jpg";
		public const string MEDIATITLEKEY = "Title";
		public const string MEDIATITLE = "ProfilePic";

		#endregion

		#region celeb const

		public const string CELEBBASEURL = "https://www.kin2.me/img/color/";
		public const string CELEBIMAGEEXTENSION = ".jpg";
		#endregion



		#region FlyoutConstant

		public const string MYFAMILY_TITLE="My Family";
		public const string MYFAMILY_ICON = "myfamily_icon.png";
		public const string COUSIN_TITLE="Cousin Match";
		public const string COUSIN_ICON="c.png";
		public const string RESEARCH_HELP_TITLE="Research Help";
		public const string RESEARCH_HELP_ICON="search_icon.png";
		public const string LOGOUT_TITLE="Log Out";
		public const string LOGOUT_ICON="logout_icon.png";
		#endregion

		#region Match history const
		public const string HISTORY_COMMON_INDI_OGFN = "CommonIndiOgfn";
		public const string HISTORY_DEGREES = "Degrees";
		public const string History_DESC = "Description";
		public const string HISTORY_FIRST_FIND_DATE = "FirstFindDate";
		public const string HISTORY_FOUND = "Found";
		public const string HISTORY_GROUP_OGFN = "GroupOgfn";
		public const string HISTORY_INDI_1_OGFN = "Indi1Ogfn";
		public const string HISTORY_INDI_2_OGFN = "Indi2Ogfn";
		public const string HISTORY_INDI_LIST1 = "IndiList1";
		public const string HISTORY_INDI_LIST2 = "IndiList2";
		public const string HISTORY_LAST_FIND_DATE = "LastFindDate";
		public const string HISTORY_NOTFOUND_REASON = "NotFoundReason";
		public const string HISTORY_TYPE = "Type";
		public const string HISTORY_USEROGFN = "UserOgfn";
		#endregion

		#region ProfilePercentage

		public const string PERCENTAGE_COMPLETE="PercentComplete";

		#endregion


		#region Refernce Constants

		public const string MOTHERS_PARENT="Mother's parents";
		public const string FATHERS_PARENT="Father's parents";

		public const string FATHERS_FATHERS_PARENT="Father's Father's Parents";
		public const string FATHERS_MOTHERS_PARENT="Father's Mother's Parents";
		public const string MOTHERS_FATHERS_PARENT="Mother's Father's Parents";
		public const string MOTHERS_MOTHERS_PARENT="Mother's Mother's Parents";
	
		#endregion

	}

}