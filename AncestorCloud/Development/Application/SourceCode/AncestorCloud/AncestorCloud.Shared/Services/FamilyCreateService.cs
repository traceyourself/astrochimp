using System;
using Cirrious.CrossCore;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public class FamilyCreateService :IFamilyCreateService
	{
		private readonly ILoader _loader;

		public FamilyCreateService()
		{
			_loader = Mvx.Resolve<ILoader> ();
		}

		public async Task<ResponseModel<LoginModel>> CreateFamily(LoginModel model)
		{
			_loader.showLoader ();

			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");
				client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type","application/xml");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.SESSIONID] = model.Value;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.FAMILY_CREATE_SERVICE,param);

				Mvx.Trace(url);

				string content = "<Fam xmlns=\"http://schemas.datacontract.org/2004/07/OGF.WS.Data\">\n\t<GroupOgfn>"+model.GroupOGFN+"</GroupOgfn>\n\t<Ogfn>"+model.OGFN+"</Ogfn>\n</Fam>";

				//System.IO.MemoryStream mStream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));

				var response = await client.PostAsync(url, new StringContent(
					content,
					Encoding.UTF8,
					"application/xml"));

				String res = response.Content.ReadAsStringAsync().Result;
				//System.Diagnostics.Debug.WriteLine ("4.) CreateFamily url : "+url);
				//System.Diagnostics.Debug.WriteLine ("4.) CreateFamily content : "+content);
				System.Diagnostics.Debug.WriteLine (" CreateFamily response : "+res+ "response.EnsureSuccessStatusCode(); " +response.EnsureSuccessStatusCode());

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;
						model.FamOGFN = dict[AppConstant.VALUE].ToString();
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

