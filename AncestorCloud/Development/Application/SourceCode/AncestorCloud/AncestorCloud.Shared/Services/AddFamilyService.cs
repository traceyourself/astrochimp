using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using System.IO;
using System.Net;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using Newtonsoft.Json;



namespace AncestorCloud.Shared
{
	public class AddFamilyService : IAddFamilyService
	{

		private ILoader _loader;

		public AddFamilyService()
		{
			_loader = Mvx.Resolve<ILoader> ();

		}

		#region  implementation

		public async Task<ResponseModel<ResponseDataModel>> AddFamilyMember(AddFamilyModel model)
		{
			_loader.showLoader ();
			ResponseModel<ResponseDataModel> responseModel = new ResponseModel<ResponseDataModel>();
			//Hit service using HTTP Client
			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.ADD_PERSON_SESSION_ID] = model.BirthLocation;
				param[AppConstant.ADD_PERSON_INDIOGFN] = model.BirthLocation;
				param[AppConstant.ADD_PERSON_NAME] = model.BirthLocation;
				param[AppConstant.ADD_PERSON_GENDER] = model.BirthLocation;
				param[AppConstant.ADD_PERSON_BIRTHDATE] = model.BirthYear;
				param[AppConstant.ADD_PERSON_BIRTHPLACE] = model.BirthLocation;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.ADD_PEOPLE_SERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("add family response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseDataModel responsemodal = DataParser.GetAddMemberDetails (dict);

				if(responsemodal.Code.Equals("0")){

					param = new Dictionary<string, string>();

					param[AppConstant.ADD_PERSON_SESSION_ID] = model.BirthLocation;
					param[AppConstant.ADD_PERSON_INDIOGFN] = model.BirthLocation;
					param[AppConstant.ADD_RELATION_INDIOGFN] = model.BirthYear;
					param[AppConstant.ADD_RELATION_TYPE] = model.BirthLocation;

					url = WebServiceHelper.GetWebServiceURL(AppConstant.ADD_PEOPLE_SERVICE,param);

					Mvx.Trace(url);

					response = await client.GetAsync(url);

					String responserelation = response.Content.ReadAsStringAsync().Result;

					dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (responserelation);

					responsemodal = DataParser.GetAddMemberRelationDetails (dict);

					if(responsemodal.Code.Equals("0")){
						
					}

					responseModel = new ResponseModel<ResponseDataModel>();
					responseModel.Content = responsemodal;
					responsemodal.Status = ResponseStatus.OK;

				}


				return responseModel as ResponseModel<ResponseDataModel> ;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<ResponseDataModel> responsemodal = new ResponseModel<ResponseDataModel>();
				responsemodal.Status = ResponseStatus.Fail;

				return responseModel as ResponseModel<ResponseDataModel> ;
			}
			finally{
			
				_loader.hideLoader();
			}

		}
		#endregion

	}
}

