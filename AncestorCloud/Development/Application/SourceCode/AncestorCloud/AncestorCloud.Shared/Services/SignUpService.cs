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

		public SignUpService()
		{
			_loader = Mvx.Resolve<ILoader> ();
		}

		#region ISignUpService implementation
		public async Task<ResponseModel<LoginModel>> SignUp (string name, string email, string password, string developerId, string developerPassword)
		{
			_loader.showLoader ();

			try
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				//== hit for getting session id
				//email = "mikeyamadeo@gmail.com";
				//password = "password";

				Dictionary <string,string> param = new Dictionary<string, string>();
				param[AppConstant.USERNAMEKEY]=AppConstant.DEVUSEREMAIL;
				param[AppConstant.PASSWORDKEY]=AppConstant.DEVUSERPASSWORD;
				param[AppConstant.DEVELOPERIDKEY]=developerId;
				param[AppConstant.DEVELOPERPASSWORDKEY]=developerPassword;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.USERLOGINSERVICE,param);

				//String url = "https://wsdev.onegreatfamily.com/v11.02/User.svc/Signin?username="+email+"&Password="+password+"&DeveloperId="+developerId+"&DeveloperPassword="+developerPassword;
				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("Login response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				LoginModel modal = DataParser.GetLoginDetails (dict);

				modal.UserEmail = email;
				//Mvx.Trace("Parced Values : "+modal.Code+" : "+modal.Message+" : "+modal.Value);

				//== 

				//hit for sign up


				HttpClient signupClient = new HttpClient(new NativeMessageHandler());
				signupClient.DefaultRequestHeaders.Add("Accept","application/json");

				//url = "https://wsdev.onegreatfamily.com/v11.02/User.svc/Create?SessionId="+modal.Value+"&EmailAddress="+email+"&Password="+password+"&FirstName="+name+"&LastName="+name+"&ProductId="+"Pro_id";

				Dictionary <string,string> paramDic = new Dictionary<string, string>();
				paramDic[AppConstant.SESSIONID]=modal.Value;
				paramDic[AppConstant.EMAILKEY]=email;
				paramDic[AppConstant.FIRSTNAMEKEY]=name;
				paramDic[AppConstant.LASTNAMEKEY]=name;
				paramDic[AppConstant.PRODUCTIDKEY]=AppConstant.PRODUCTID;
				paramDic[AppConstant.DEVELOPERIDKEY]=developerId;
				paramDic[AppConstant.DEVELOPERPASSWORDKEY]=developerPassword;

				string signUpUrl = WebServiceHelper.GetWebServiceURL(AppConstant.USERSIGNINSERVICE,paramDic);

				var signupResponse = await client.GetAsync (signUpUrl);

				String signupRes = signupResponse.Content.ReadAsStringAsync().Result;

				Mvx.Trace("sign up : "+signupRes );
				Dictionary <string,object> signUpDict = JsonConvert.DeserializeObject<Dictionary<string,object>> (signupRes);

				modal = DataParser.GetSignUpDetails(modal,signUpDict);

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.Status = ResponseStatus.OK;
				responsemodal.Content= modal;
				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.Status = ResponseStatus.Fail;
				return responsemodal;
			}
			finally{
				_loader.hideLoader();
			}

		}
		#endregion


	}
}

