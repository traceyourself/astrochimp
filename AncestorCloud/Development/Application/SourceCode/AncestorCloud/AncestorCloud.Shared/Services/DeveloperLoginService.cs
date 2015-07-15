using System;
using System.Threading.Tasks;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Newtonsoft.Json;

namespace AncestorCloud.Shared
{
	public class DeveloperLoginService : IDeveloperLoginService
	{
		private readonly ILoader _loader;


		public DeveloperLoginService()
		{
			_loader = Mvx.Resolve<ILoader> ();

		}

		public async Task<ResponseModel<String>> DevelopeLogin ()
		{
			_loader.showLoader ();

			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.DEVELOPERIDKEY] = AppConstant.DEVELOPERID;
				param[AppConstant.PASSWORDKEY] = AppConstant.DEVELOPERPASSWORD;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.DEVELOPERLOGINSERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("1.) Develope Login response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<String> responsemodal = new ResponseModel<String>();

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;
						responsemodal.Content= dict[AppConstant.VALUE].ToString();
					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
						responsemodal.ResponseCode = dict[AppConstant.CODE].ToString();
					}
				}
					
				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				ResponseModel<String> responsemodal = new ResponseModel<String>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.ResponseCode = "0";

				return responsemodal;
			}
			finally{

				_loader.hideLoader();
			}
		}
	}
}

