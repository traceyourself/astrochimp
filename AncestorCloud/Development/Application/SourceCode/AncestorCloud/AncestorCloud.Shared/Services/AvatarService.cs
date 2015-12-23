using System;
using Cirrious.CrossCore;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AncestorCloud.Shared
{
	public class AvatarService : IAvatarService
	{
		private readonly ILoader _loader;
		private readonly IDeveloperLoginService _developerLoginService;

		public AvatarService()
		{
			_loader = Mvx.Resolve<ILoader> ();
			_developerLoginService = Mvx.Resolve<IDeveloperLoginService>();
		}

		#region IAvatarService implementation
		public async System.Threading.Tasks.Task<ResponseModel<LoginModel>> GetIndiAvatar (LoginModel model)
		{
			try   
			{
				var loginResult=await _developerLoginService.DevelopeLogin();

				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.SESSIONID]=loginResult.Content;
				param[AppConstant.AVATAR_OGFN]=model.AvatarOGFN;
				param[AppConstant.IMAGE_TYPE]=AppConstant.PNG;
				param[AppConstant.IMAGE_SIZE]="200"+"%2c"+"200";
				param[AppConstant.STACKTRACE]=AppConstant.TRUE;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.AVATAR_IMAGE_SERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("GetIndiAvatar response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();

				if(dict.ContainsKey(AppConstant.MESSAGE))
				{
					if(dict[AppConstant.MESSAGE].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;
						//login= DataParser.GetIndiReadData(login,dict);
					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
						responsemodal.ResponseCode = dict[AppConstant.CODE].ToString();
					}
				}

				responsemodal.Content= model;

				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.ResponseCode = "0";
				responsemodal.Content= model;
				return responsemodal;
			}
			finally{
				_loader.hideLoader();
			}
		}
		#endregion
	}
}

