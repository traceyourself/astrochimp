using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AncestorCloud.Shared
{
	public class UserReadService : IUserReadService
	{
		private ILoader _loader;

		public UserReadService()
		{
			_loader = Mvx.Resolve<ILoader> ();
		}

		public async Task<ResponseModel<LoginModel>> MakeUserReadService(LoginModel model)
		{
			_loader.showLoader ();
			//https://wsdev.onegreatfamily.com/v11.02/User.svc/Read?SessionId=s4zxi523e3hlgnhbgjh3hlm4
			try   
			{

				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				//String url = "https://wsdev.onegreatfamily.com/v11.02/User.svc/Read?SessionId=s4zxi523e3hlgnhbgjh3hlm4";

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.SESSIONID]=model.Value;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.USEREADSERVICE,param);

				//Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				//System.Diagnostics.Debug.WriteLine ("Login response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				model= DataParser.GetUserReadData(model,dict);

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.loginModal = model;
				responsemodal.Status = ResponseStatus.OK;

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
	}
}

