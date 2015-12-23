using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AncestorCloud.Shared
{
	public class FbSigninService : IFbSigninService
	{
		private readonly ILoader _loader;
		private readonly IDeveloperLoginService _developerLoginService;

		public FbSigninService()
		{
			_loader = Mvx.Resolve<ILoader> ();
			_developerLoginService = Mvx.Resolve<IDeveloperLoginService>();
		}

		public async Task<ResponseModel<LoginModel>> LinkFacebookUser (User user)
		{
			try   
			{
				var loginResult=await _developerLoginService.DevelopeLogin();

				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.LINKIDKEY] = user.UserID;
				param[AppConstant.LINKTYPEKEY] = AppConstant.KIN2_LINKTYPE;
				param[AppConstant.SESSIONID] = loginResult.Content;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.USERLOGINSERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("--LinkFacebookUser response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();

				if(dict.ContainsKey(AppConstant.MESSAGE))
				{
					if(dict[AppConstant.MESSAGE].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;
					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
						responsemodal.ResponseCode = dict[AppConstant.CODE].ToString();
					}
				}

				LoginModel login = new LoginModel();
				login.UserEmail = user.Email;
				responsemodal.Content= login;

				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.ResponseCode = "0";

				return responsemodal;
			}
		}

		public async Task<ResponseModel<LoginModel>> LinkFacebookLoginUser (User user)
		{
			_loader.showLoader ();

			try   
			{
				var result = await LinkFacebookUser(user);
				if(result.Status!=ResponseStatus.OK){
			
					if(String.Equals(result.ResponseCode,AppConstant.USER_WAS_NOT_FOUND_CODE))
					{
						var signupResult = await LinkFacebookSignUpUser(user);
						var linkResult = await LinkFacebookUser(user);
						return linkResult;
					}
				}
				return result;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.ResponseCode = "0";

				return responsemodal;
			}
			finally{

				_loader.hideLoader();
			}
		
		}


		#region SignIn Service


		public async Task<ResponseModel<LoginModel>> LinkFacebookSignUpUser (User user)
		{
			_loader.showLoader ();

			try   
			{
				var loginResult=await _developerLoginService.DevelopeLogin();

				// Link Facebook User
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.EMAILKEY] = user.Email;
				param[AppConstant.PASSWORDKEY] = "";
				param[AppConstant.FIRSTNAMEKEY] = user.FirstName;
				param[AppConstant.LASTNAME] = user.LastName;
				param[AppConstant.PRODUCTIDKEY] = AppConstant.PRODUCTID;
				param[AppConstant.LINKIDKEY] = user.UserID;
				param[AppConstant.LINKTYPEKEY] = AppConstant.KIN2_LINKTYPE;
				param[AppConstant.SESSIONID] = loginResult.Content;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.USERSIGNINSERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("--LinkFacebookUserSignup response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();

				LoginModel modal = new LoginModel();

				modal.UserEmail = user.Email;

				modal = DataParser.GetSignUpDetails(modal,dict);

				if(dict.ContainsKey(AppConstant.MESSAGE))
				{
					if(dict[AppConstant.MESSAGE].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;
					}
					else
					{
						if(!String.Equals(dict[AppConstant.CODE].ToString(),AppConstant.USER_ALREADY_EXISTS_CODE))
						{
							responsemodal.Status = ResponseStatus.Fail;
							responsemodal.ResponseError = dict[AppConstant.MESSAGE] as string;
						}
					}
				}

				responsemodal.Content= modal;

				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
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

