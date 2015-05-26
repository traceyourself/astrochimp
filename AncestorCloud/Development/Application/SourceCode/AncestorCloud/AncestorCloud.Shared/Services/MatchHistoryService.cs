using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AncestorCloud.Shared
{
	public class MatchHistoryService : IMatchHistoryService
	{
		private ILoader _loader;

		public MatchHistoryService()
		{
			_loader = Mvx.Resolve<ILoader> ();
		}

		public async Task<ResponseModel<List<RelationshipFindResult>>> HistoryReadService(LoginModel model)
		{
			_loader.showLoader ();

			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.SESSIONID]=model.Value;

				//https://ws.onegreatfamily.com/v11.02/Individual.svc/RelationshipFindHistoryGet?sessionId=bvbushd0mlei3hcvuvcahba1

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.MATCHER_HISTORY_SERVICE,param);

				Mvx.Trace("Matcher history url :- "+url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				Mvx.Trace("history read response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);                      

				ResponseModel<List<RelationshipFindResult>> responsemodal = new ResponseModel<List<RelationshipFindResult>>();

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Content = DataParser.ReadDataHistory(dict);
						responsemodal.Status = ResponseStatus.OK;
					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
						responsemodal.Content = new List<RelationshipFindResult>();
					}
				}

				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<List<RelationshipFindResult>> responsemodal = new ResponseModel<List<RelationshipFindResult>>();
				responsemodal.Status = ResponseStatus.Fail;

				return responsemodal;
			}
			finally{
				_loader.hideLoader();
			}

		}
	}
}

