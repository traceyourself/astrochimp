﻿using System;
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

		public async Task<ResponseModel<LoginModel>> LinkFacebookUser (User user, String sessionID)
		{
			_loader.showLoader ();

			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.LINKIDKEY] = user.UserID;
				param[AppConstant.LINKTYPEKEY] = AppConstant.LINKTYPE;
				param[AppConstant.SESSIONID] = sessionID;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.USERLOGINSERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("LinkFacebookUser response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				LoginModel login = new LoginModel();

				login.Value = sessionID;
				login.UserEmail = user.Email;

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.Status = ResponseStatus.OK;
				responsemodal.Content= login;

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
	}
}
