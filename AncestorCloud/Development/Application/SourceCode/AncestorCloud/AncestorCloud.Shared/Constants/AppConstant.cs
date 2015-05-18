using System;

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
		OKCancelSelect
	}

	static public class AppConstant
	{
		#region FB Constants
		public const string FBAPIKEY = "591314537670509";//1428257997477283//591314537670509
		public const string FBAPISECRETKEY = "659bacca4a45654358bb632f5607eeb0";//141797ed935391946dd464c525495a0b//659bacca4a45654358bb632f5607eeb0
		public const string FBSCOPE = "email,publish_actions,user_birthday,user_friends,user_relationships";
		public const string FRIENDKEY = "friend";
		#endregion

		#region Google Analytics Constants

		public const string GATRACKINGID = "UA-62874372-1";

		#endregion


		#region Web service constants
		public const string DEVELOPERID = "AncestorCloud";
		public const string DEVELOPERPASSWORD = "492C4DD9-A129-4146-BAE9-D0D45FBC315C";
		public const string PRODUCTID = "OGF_YEARLY_TRIAL7";
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
		#endregion


		#region Webservice URL Constants

		public const string BASEURL = "https://wsdev.onegreatfamily.com/v11.02";
		public const string USEREADSERVICE = "User.svc/Read";
		public const string USERSIGNINSERVICE = "User.svc/Create";
		public const string USERLOGINSERVICE = "User.svc/Signin";
		public const string DEVELOPERLOGINSERVICE = "Developer.svc/Login";

		public const string ADD_PEOPLE_SERVICE = "Individual.svc/CreateUpdate";
		public const string ADD_RELATION_SERVICE = "Individual.svc/AddIndiAsRelation";
		public const string RELATIONSHIP_MATCH_SERVICE = "Individual.svc/RelationshipFind";
		public const string UPLOAD_MEDIA_SERVICE = "Individual.svc/MediaAdd";
		public const string INDIVIDUAL_READ_SERVICE = "Individual.svc/Read";
		public const string GROUP_CREATE_SERVICE = "Group.svc/Create";
		public const string FAMILY_CREATE_SERVICE = "Family.svc/Create";

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

		#region Indi Read Data
		public const string AVATARINDIOGFN = "AvatarId";
		public const string AVATARINDIOGFNKEY = "avatarOgfn";
		public const string IMAGETYPEKEY = "ImageType";
		public const string IMAGESIZEKEY = "ImageSize";
		public const string IMAGESIZE = "100%2c100";
		public const string CHILDOGFNKEY = "ChildFamilyOgfn";


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

		#region Match Service

		public const string INDIOGFN1KEY= "IndiOgfn1";
		public const string INDIOGFN2KEY= "IndiOgfn2";
		public const string TYPEKEY= "Type";
		public const string MATCHTYPE = "DLA";

		#endregion

		#region Fb Link

		public const string LINKIDKEY= "linkId";
		public const string LINKTYPEKEY= "linkType";
		public const string LINKTYPE = "FACEBOOK_OGF";
		#endregion

		#region Media Upload 

		public const string FILENAMEKEY= "FileName";
		public const string FILENAME= "ProfilePic.jpg";
		public const string FILETYPEKEY = "FileType";
		public const string FILETYPE = "jpg";
		public const string MEDIATITLEKEY = "Title";
		public const string MEDIATITLE = "ProfilePic";
	
		#endregion

		#region

		public const string CELEBBASEURL = "https://www.kin2.me/img/color/";
		public const string CELEBIMAGEEXTENSION = ".jpg";

		#endregion

	}

}