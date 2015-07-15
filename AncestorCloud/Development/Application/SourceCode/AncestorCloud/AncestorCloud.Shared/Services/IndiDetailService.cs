using System;
using Cirrious.CrossCore;
using System.Net.Http;
using ModernHttpClient;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public class IndiDetailService : IIndiDetailService
	{
		private readonly ILoader _loader;


		public IndiDetailService()
		{
			_loader = Mvx.Resolve<ILoader> ();

		}

		#region IIndiDetailService implementation

		public async System.Threading.Tasks.Task<ResponseModel<LoginModel>> GetIndiDetails (LoginModel login)
		{
			_loader.showLoader ();
			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();
				param[AppConstant.SESSIONID] = login.Value;
				param[AppConstant.INDIOGFN] = login.IndiOGFN;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.INDIVIDUAL_READ_SERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("GetIndiDetails response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;

						login = DataParser.GetIndiReadData(login,dict);

					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
						responsemodal.ResponseCode = dict[AppConstant.CODE].ToString();

					}
				}

				responsemodal.Content= login;

				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.Content= login;
				responsemodal.ResponseCode = "0";
				return responsemodal;
			}
			finally{
				_loader.hideLoader();
			}
		}
		#endregion


		#region family implementation
		public async System.Threading.Tasks.Task<ResponseModel<People>> GetIndiFamilyDetails (string ogfn,string sessionid)
		{
			_loader.showLoader ();
			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();
				param[AppConstant.SESSIONID] = sessionid;
				param[AppConstant.INDIOGFN] = ogfn;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.INDIVIDUAL_READ_SERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("GetIndiDetails response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<People> responsemodal = new ResponseModel<People>();

				People returnPeopleData = new People();

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;

						returnPeopleData = DataParser.GetIndiFamilyReadData(dict);

						returnPeopleData.IndiOgfn = ogfn; 


						Mvx.Trace(returnPeopleData.Name+" : "+returnPeopleData.Relation+" : "+returnPeopleData.FamOGFN);


					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
						responsemodal.ResponseCode = dict[AppConstant.CODE].ToString();
					}
				}

				responsemodal.Content= returnPeopleData;

				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				ResponseModel<People> responsemodal = new ResponseModel<People>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.Content= new People();
				responsemodal.ResponseCode = "0";
				return responsemodal;
			}
			finally{
				_loader.hideLoader();
			}
		}

		#endregion

	}
}

