using System;
using System.Threading.Tasks;
using System.Net.Http;
using ModernHttpClient;
using Cirrious.CrossCore;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AncestorCloud.Shared
{
	public class SignUpService : ISignUpService
	{

		private ILoader _loader;

		private readonly IDeveloperLoginService _developerLoginService;

		private readonly IUserReadService _userReadService;

		public SignUpService()
		{
			_loader = Mvx.Resolve<ILoader> ();
			_developerLoginService = Mvx.Resolve<IDeveloperLoginService>();
			_userReadService = Mvx.Resolve<IUserReadService> ();
		}

		#region ISignUpService implementation
		public async Task<ResponseModel<LoginModel>> SignUp (string FirstName,string LastName, string email, string password, string developerId, string developerPassword)
		{
			//_loader.showLoader ();

			try
			{
//				HttpClient client = new HttpClient(new NativeMessageHandler());
//				client.DefaultRequestHeaders.Add("Accept","application/json");
//
//				//== hit for getting session id
//				//email = "mikeyamadeo@gmail.com";
//				//password = "password";
//
//				Dictionary <string,string> param = new Dictionary<string, string>();
//				param[AppConstant.USERNAMEKEY]=AppConstant.DEVUSEREMAIL;
//				param[AppConstant.PASSWORDKEY]=AppConstant.DEVUSERPASSWORD;
//				param[AppConstant.DEVELOPERIDKEY]=developerId;
//				param[AppConstant.DEVELOPERPASSWORDKEY]=developerPassword;
//
//				String url = WebServiceHelper.GetWebServiceURL(AppConstant.USERLOGINSERVICE,param);
//
//				//String url = "https://wsdev.onegreatfamily.com/v11.02/User.svc/Signin?username="+email+"&Password="+password+"&DeveloperId="+developerId+"&DeveloperPassword="+developerPassword;
//				Mvx.Trace(url);
//
//				var response = await client.GetAsync(url);
//
//				string res = response.Content.ReadAsStringAsync().Result;
//
//				System.Diagnostics.Debug.WriteLine ("Login response : "+res);
//
//				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);
//
//				LoginModel modal = DataParser.GetLoginDetails (dict);
//
//				modal.UserEmail = email;
				//Mvx.Trace("Parced Values : "+modal.Code+" : "+modal.Message+" : "+modal.Value);

				//============>

				ResponseModel<String> data = await _developerLoginService.DevelopeLogin ();

				String sessionID = data.Content;

				LoginModel modal = new LoginModel();

				modal.UserEmail = email;

				modal.Value = sessionID;

				modal.Name = string.Format("{0} {1}",FirstName,LastName);

				//===========>

				//hit for sign up

				_loader.showLoader();

				HttpClient signupClient = new HttpClient(new NativeMessageHandler());
				signupClient.DefaultRequestHeaders.Add("Accept","application/json");

				//url = "https://wsdev.onegreatfamily.com/v11.02/User.svc/Create?SessionId="+modal.Value+"&EmailAddress="+email+"&Password="+password+"&FirstName="+name+"&LastName="+name+"&ProductId="+"Pro_id";

				Dictionary <string,string> paramDic = new Dictionary<string, string>();
				paramDic[AppConstant.SESSIONID]=modal.Value;
				paramDic[AppConstant.EMAILKEY]=email;
				paramDic[AppConstant.FIRSTNAMEKEY]=FirstName;
				paramDic[AppConstant.LASTNAMEKEY]=LastName;
				paramDic[AppConstant.PASSWORDKEY]=password;
				paramDic[AppConstant.PRODUCTIDKEY]=AppConstant.PRODUCTID;
				paramDic[AppConstant.DEVELOPERIDKEY]=developerId;
				paramDic[AppConstant.DEVELOPERPASSWORDKEY]=developerPassword;

				string signUpUrl = WebServiceHelper.GetWebServiceURL(AppConstant.USERSIGNINSERVICE,paramDic);

				var signupResponse = await signupClient.GetAsync (signUpUrl);

				String signupRes = signupResponse.Content.ReadAsStringAsync().Result;
				Mvx.Trace("2.) Sign up Url : "+signUpUrl);
				Mvx.Trace(" Sign up Response : "+signupRes );

				Dictionary <string,object> signUpDict = JsonConvert.DeserializeObject<Dictionary<string,object>> (signupRes);


				modal = DataParser.GetSignUpDetails(modal,signUpDict);

				if(signUpDict.ContainsKey(AppConstant.CODE))
				{

					ResponseModel<LoginModel> signupModel = new ResponseModel<LoginModel>();
					if(String.Equals(signUpDict[AppConstant.CODE].ToString(),AppConstant.USER_ALREADY_EXISTS_CODE))
					{
						signupModel.Status = ResponseStatus.Fail;
						signupModel.ResponseError = signUpDict[AppConstant.MESSAGE] as string;
						return signupModel;
					}
				}

				//===============================
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				//== hit for getting session id
				//email = "mikeyamadeo@gmail.com";
				//password = "password";

				Dictionary <string,string> param = new Dictionary<string, string>();
				param[AppConstant.USERNAMEKEY]=email;
				param[AppConstant.PASSWORDKEY]=password;
				param[AppConstant.DEVELOPERIDKEY]=developerId;
				param[AppConstant.DEVELOPERPASSWORDKEY]=developerPassword;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.USERLOGINSERVICE,param);

				//String url = "https://wsdev.onegreatfamily.com/v11.02/User.svc/Signin?username="+email+"&Password="+password+"&DeveloperId="+developerId+"&DeveloperPassword="+developerPassword;
				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				string res = response.Content.ReadAsStringAsync().Result;

				Mvx.Trace("3.) Login Url : "+url);
				Mvx.Trace(" Login response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);


				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();


				if(dict.ContainsKey(AppConstant.MESSAGE))
				{
					if(dict[AppConstant.MESSAGE].Equals((AppConstant.SUCCESS)))
					{
						modal.Value = dict[AppConstant.VALUE].ToString();

						ResponseModel<LoginModel> userdata = await _userReadService.MakeUserReadService (modal);

						if (userdata.Status == ResponseStatus.Fail)
						{
							responsemodal.Status = ResponseStatus.Fail;
						}
						else
						{
							modal = userdata.Content;

							responsemodal.Status = ResponseStatus.OK;
						}
						//ResponseDataModel _anchorResponse = await GetAnchor(modal,String.Format("{0} {1}",FirstName,LastName));

//						if(_anchorResponse == null)
//						{
//							responsemodal.Status = ResponseStatus.Fail;


//						}else
//						{
//							modal.IndiOGFN = _anchorResponse.value;
//							responsemodal.Status = ResponseStatus.OK;
//						}


					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
					}
				}

				responsemodal.Content= modal;

				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.ResponseError = ex.Message;
				return responsemodal;
			}
			finally{
				_loader.hideLoader();
			}

		}

		public async Task<ResponseDataModel> GetAnchor(LoginModel modal, string Name)
		{
			HttpClient anchorclient = new HttpClient(new NativeMessageHandler());

			anchorclient.DefaultRequestHeaders.Add("Accept","application/json");

			Dictionary <string,string> anchorparam = new Dictionary<string, string>();

			anchorparam[AppConstant.ADD_PERSON_SESSION_ID] = modal.Value;
			anchorparam [AppConstant.ADD_PERSON_NAME] = Name;

			String _url = WebServiceHelper.GetWebServiceURL(AppConstant.ADD_PEOPLE_SERVICE,anchorparam);

			Mvx.Trace(_url);

			var _response = await anchorclient.GetAsync(_url);

			String _res = _response.Content.ReadAsStringAsync().Result;

			System.Diagnostics.Debug.WriteLine ("add anchor response : "+_res);

			Dictionary <string,object> _dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (_res);


			ResponseDataModel _responsemodal = null;

			if (_dict.ContainsKey (AppConstant.MESSAGE)) {
				if (_dict [AppConstant.MESSAGE].Equals ((AppConstant.SUCCESS))) {
					_responsemodal = DataParser.GetAddMemberDetails (_dict);
				}
			}

			return _responsemodal;
		}

		#endregion


	}
}

