using System;
using Cirrious.CrossCore;
using ModernHttpClient;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public class FBFriendLinkService : IFBFriendLinkService
	{
		private readonly ILoader _loader;


		public FBFriendLinkService()
		{
			_loader = Mvx.Resolve<ILoader> ();

		}

		public async Task<ResponseModel<People>> FbFriendRead (People friend)
		{
			_loader.showLoader ();

			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.LINKIDKEY] = friend.UserID;
				param[AppConstant.LINKTYPEKEY] = AppConstant.LINKTYPE;
				param[AppConstant.SESSIONID] = friend.SessionId;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.INDIVIDUAL_READ_SERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("FbFriendRead response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<People> responsemodal = new ResponseModel<People>();

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;

						friend.IndiOgfn = dict[AppConstant.VALUE].ToString();
					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
					}
				}
					
				responsemodal.Content =  friend;

				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);

				ResponseModel<People> responsemodal = new ResponseModel<People>();

				responsemodal.Status = ResponseStatus.Fail;

				return responsemodal;
			}
			finally{

				_loader.hideLoader();
			}

		}

	}
}

