using System;
using Cirrious.CrossCore;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AncestorCloud.Shared
{
	public class ContactLinkService : IContactLinkService
	{
		private readonly ILoader _loader;


		public ContactLinkService()
		{
			_loader = Mvx.Resolve<ILoader> ();

		}

		#region IContactLinkService implementation

		public async System.Threading.Tasks.Task<ResponseModel<People>> ContactRead (People contact)
		{
			_loader.showLoader ();

			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.EMAIL] = contact.Email;
				param[AppConstant.SESSIONID] = contact.SessionId;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.USEREADSERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("ContactRead response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<People> responsemodal = new ResponseModel<People>();

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;

						contact = DataParser.GetContactData(contact,AppConstant.VALUE,dict);

					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
						responsemodal.ResponseCode = (string) dict[AppConstant.CODE];
					}
				}

				responsemodal.Content =  contact;

				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);

				ResponseModel<People> responsemodal = new ResponseModel<People>();
				responsemodal.ResponseCode = "0";
				responsemodal.Status = ResponseStatus.Fail;

				return responsemodal;
			}
			finally{

				_loader.hideLoader();
			}
		}

		#endregion
	}
}

