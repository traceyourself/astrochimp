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

		public async Task<String> AddFamilyMember(AddFamilyModel model)
		{
			_loader.showLoader ();

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
					param[AppConstant.ADD_RELATION_INDIOGFN] = responsemodal.BirthYear;
					param[AppConstant.ADD_RELATION_TYPE] = model.BirthLocation;

					url = WebServiceHelper.GetWebServiceURL(AppConstant.ADD_PEOPLE_SERVICE,param);

					Mvx.Trace(url);

					var userReadResponse = await _userReadService.MakeUserReadService(modal);
					//ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
					//responsemodal.loginModal = modal;
					//responsemodal.Status = ResponseStatus.OK;
				}


				return userReadResponse as ResponseModel<LoginModel>;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
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

