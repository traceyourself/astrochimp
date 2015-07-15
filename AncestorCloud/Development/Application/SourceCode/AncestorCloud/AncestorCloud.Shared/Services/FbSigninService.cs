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


		public FbSigninService()
		{
			_loader = Mvx.Resolve<ILoader> ();

		}

		public async Task<ResponseModel<LoginModel>> LinkFacebookLoginUser (User user, String sessionID)
		{
			_loader.showLoader ();

			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.LINKIDKEY] = user.UserID;
				param[AppConstant.LINKTYPEKEY] = AppConstant.KIN2_LINKTYPE;
				param[AppConstant.SESSIONID] = sessionID;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.USERLOGINSERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("--LinkFacebookUser response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;
					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
						//responsemodal.ResponseCode = dict[AppConstant.CODE];
					}
				}

				LoginModel login = new LoginModel();
				login.Value = sessionID;
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
			finally{

				_loader.hideLoader();
			}
		
		}


		#region SignIn Service


		public async Task<ResponseModel<LoginModel>> LinkFacebookSignUpUser (User user, String sessionID)
		{
			_loader.showLoader ();

			try   
			{
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
				param[AppConstant.SESSIONID] = sessionID;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.USERSIGNINSERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("--LinkFacebookUserSignup response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();

				LoginModel modal = new LoginModel();

				modal.Value = sessionID;
				modal.UserEmail = user.Email;

				modal = DataParser.GetSignUpDetails(modal,dict);

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;
					}
					else
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

