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

		public async Task<ResponseModel<ResponseDataModel>> AddFamilyMember(People model)
		{
			_loader.showLoader ();
			ResponseModel<ResponseDataModel> responseModel = new ResponseModel<ResponseDataModel>();
			//Hit service using HTTP Client
			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.ADD_PERSON_SESSION_ID] = model.SessionId;
				//param[AppConstant.ADD_PERSON_INDIOGFN] = model.IndiOgfn;
				param[AppConstant.ADD_PERSON_NAME] = model.FirstName;
				param[AppConstant.ADD_PERSON_GENDER] = model.Gender;
				param[AppConstant.ADD_PERSON_BIRTHDATE] = model.DateOfBirth;
				param[AppConstant.ADD_PERSON_BIRTHPLACE] = model.BirthLocation;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.ADD_PEOPLE_SERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("add family response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseDataModel responsemodal = DataParser.GetAddMemberDetails (dict);

				if(responsemodal.Code.Equals("0")){

					responseModel = await AddFamilyMemberRelation(model,responsemodal.value);

				}else{
					responsemodal.Status = ResponseStatus.Fail;
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


		#region Add Relationship

		public async Task<ResponseModel<ResponseDataModel>> AddFamilyMemberRelation(People model,string indiogfn)
		{
			_loader.showLoader ();
			ResponseModel<ResponseDataModel> responseModel = new ResponseModel<ResponseDataModel>();
			//Hit service using HTTP Client
			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				string relationType = GetTypeDetail(model.Gender,model.Relation);

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.ADD_PERSON_SESSION_ID] = model.SessionId;
				param[AppConstant.ADD_PERSON_INDIOGFN] = model.IndiOgfn;
				param[AppConstant.ADD_RELATION_INDIOGFN] = indiogfn;
				param[AppConstant.ADD_RELATION_TYPE] = relationType;

				string url = WebServiceHelper.GetWebServiceURL(AppConstant.ADD_RELATION_SERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String responserelation = response.Content.ReadAsStringAsync().Result;

				Mvx.Trace("relation response : "+ responserelation);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (responserelation);

				ResponseDataModel responsemodal = DataParser.GetAddMemberRelationDetails (dict);

				if(responsemodal.Code.Equals("0")){
					responseModel = new ResponseModel<ResponseDataModel>();
					responseModel.Content = responsemodal;
					responsemodal.Status = ResponseStatus.OK;	
				}else{
					responsemodal.Status = ResponseStatus.Fail;
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

		private string GetTypeDetail(string gender,string relation)
		{
			string type = "";
			if(gender.Equals("Male")){
				if(relation.Equals("Siblings")){
					type = AppConstant.BROTHER_RELATIONSHIP;
				}else if(relation.Equals("Parents")){
					type = AppConstant.FATHER_RELATIONSHIP;
				}else if(relation.Equals("Grandparents")){
					type = AppConstant.UNKNOWN_RELATIONSHIP;
				}else if(relation.Equals("Great Grandparents")){
					type = AppConstant.UNKNOWN_RELATIONSHIP;
				}
			}else{
				if(relation.Equals("Siblings")){
					type = AppConstant.SISTER_RELATIONSHIP;
				}else if(relation.Equals("Parents")){
					type = AppConstant.MOTHER_RELATIONSHIP;
				}else if(relation.Equals("Grandparents")){
					type = AppConstant.UNKNOWN_RELATIONSHIP;
				}else if(relation.Equals("Great Grandparents")){
					type = AppConstant.UNKNOWN_RELATIONSHIP;
				}
			}
			return type;
		} 
	}
}

