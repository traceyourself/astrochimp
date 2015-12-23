using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace AncestorCloud.Shared
{
	public class GroupCreateService : IGroupCreateService
	{
		private readonly ILoader _loader;
		private readonly IDeveloperLoginService _developerLoginService;

		public GroupCreateService()
		{
			_loader = Mvx.Resolve<ILoader> ();
			_developerLoginService = Mvx.Resolve<IDeveloperLoginService>();
		}
		public async Task<ResponseModel<LoginModel>> CreateGroup(LoginModel model)
		{
			_loader.showLoader ();

			try   
			{
				var loginResult=await _developerLoginService.DevelopeLogin();

				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");
				client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type","application/xml");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.SESSIONID] = loginResult.Content;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.GROUP_CREATE_SERVICE,param);

				string content = "<Group xmlns=\"http://schemas.datacontract.org/2004/07/OGF.WS.Data\">\n\t<Name>" + model.IndiOGFN +"</Name>\n\t<Password>"+model.IndiOGFN+"</Password>\n</Group>";

				//Mvx.Trace("Create Group Url : "+url);
				//Mvx.Trace("Create Group Content : "+content);
			
				var response = await client.PostAsync(url, new StringContent(
					content,
					Encoding.UTF8,
					"application/xml"));

				String res = response.Content.ReadAsStringAsync().Result;
				//System.Diagnostics.Debug.WriteLine (" Create Group response : "+res+ "response.EnsureSuccessStatusCode(); " +response.EnsureSuccessStatusCode());

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();

				if(dict.ContainsKey(AppConstant.MESSAGE))
				{
					if(dict[AppConstant.MESSAGE].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;
						model.GroupOGFN = dict[AppConstant.VALUE].ToString();
						responsemodal.Content = model;
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
				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
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

