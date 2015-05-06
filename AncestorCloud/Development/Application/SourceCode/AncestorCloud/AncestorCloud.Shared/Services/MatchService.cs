using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AncestorCloud.Shared
{
	public class MatchService : IMatchService
	{
		private readonly ILoader _loader;

		public MatchService ()
		{
			_loader = Mvx.Resolve<ILoader> ();
		}

		public async Task<ResponseModel<RelationshipFindResult>> Match (string sessionID,string firstOGFN, string secOGFN)
		{
			_loader.showLoader ();
			try
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();
				param[AppConstant.SESSIONID]=sessionID;
				param[AppConstant.INDIOGFN1KEY]=firstOGFN;
				param[AppConstant.INDIOGFN2KEY]=secOGFN;
				param[AppConstant.TYPEKEY]=AppConstant.MATCHTYPE;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.RELATIONSHIP_MATCH_SERVICE,param);

				//String url = "https://wsdev.onegreatfamily.com/v11.02/User.svc/Signin?username="+email+"&Password="+password+"&DeveloperId="+developerId+"&DeveloperPassword="+developerPassword;
				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				string res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("Match response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				RelationshipFindResult result = new RelationshipFindResult();

				if(dict.ContainsKey("Value"))
					result = JsonConvert.DeserializeObject<RelationshipFindResult> (dict["Value"].ToString());

				//string str = dict["Value"].ToString();

				ResponseModel<RelationshipFindResult> responsemodal = new ResponseModel<RelationshipFindResult>();
				responsemodal.Status = ResponseStatus.OK;
				responsemodal.Content= result;
				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<RelationshipFindResult> responsemodal = new ResponseModel<RelationshipFindResult>();
				responsemodal.Status = ResponseStatus.Fail;
				return responsemodal;
			}
			finally{
				_loader.hideLoader();
			}
		}
	}
}

