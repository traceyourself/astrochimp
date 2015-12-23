using System;
using Cirrious.CrossCore;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using ModernHttpClient;

namespace AncestorCloud.Shared
{
	public class PercentageService :IPercentageService
	{
		private readonly ILoader _loader;
		private readonly IDeveloperLoginService _developerLoginService;

		public PercentageService ()
		{
			_loader = Mvx.Resolve<ILoader> ();
			_developerLoginService = Mvx.Resolve<IDeveloperLoginService>();
		}

		public async Task<ResponseModel<LoginModel>> GetPercentComplete (LoginModel model)
		{	
			_loader.showLoader ();
			try
			{
				var loginResult=await _developerLoginService.DevelopeLogin();

				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.SESSIONID]=loginResult.Content;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.PERCENTAGE_COMPLETE_SERVICE,param);

				//String url = "https://wsdev.onegreatfamily.com/v11.02/User.svc/Signin?username="+email+"&Password="+password+"&DeveloperId="+developerId+"&DeveloperPassword="+developerPassword;
				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				string res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("Percentage Response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);


				if(dict.ContainsKey(AppConstant.MESSAGE))
				{
					if(dict[AppConstant.MESSAGE].Equals((AppConstant.SUCCESS)))
					{
				       model = DataParser.ReadPercentage(model,dict);

					}

				}

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.Status = ResponseStatus.OK;
				responsemodal.Content= model;
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



