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

		#region

		public async Task<ResponseModel<People>> EditFamilyMember(People model)
		{
			_loader.showLoader ();

			ResponseModel<People> responseModel = new ResponseModel<People>();
			//Hit service using HTTP Client
			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.ADD_PERSON_SESSION_ID] = model.SessionId;
				param[AppConstant.ADD_PERSON_INDIOGFN] = model.IndiOgfn;
				param[AppConstant.ADD_PERSON_NAME] = GetName(model);
				param[AppConstant.ADD_PERSON_GENDER] = model.Gender;
				param[AppConstant.ADD_PERSON_BIRTHDATE] = model.DateOfBirth;
				param[AppConstant.ADD_PERSON_BIRTHPLACE] = model.BirthLocation;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.ADD_PEOPLE_SERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("Edit family response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseDataModel responsemodal = DataParser.GetAddMemberDetails (dict);

				if(responsemodal.Code.Equals("0")){

					responseModel.Status = ResponseStatus.OK;
					responseModel.Content = model;

				}else{
					responseModel.Status = ResponseStatus.Fail;
				}

				responseModel.ResponseCode = responsemodal.Code;

				return responseModel as ResponseModel<People> ;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<ResponseDataModel> responsemodal = new ResponseModel<ResponseDataModel>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.ResponseCode = "0";

				return responseModel as ResponseModel<People> ;
			}
			finally{

				_loader.hideLoader();
			}
		}
		#endregion


		#region  implementation
		public async Task<ResponseModel<People>> AddFamilyMember(People model)
		{
			_loader.showLoader ();
			ResponseModel<People> responseModel = new ResponseModel<People>();
			//Hit service using HTTP Client
			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.ADD_PERSON_SESSION_ID] = model.SessionId;
				param[AppConstant.ADD_PERSON_NAME] = GetName(model);
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

					model.IndiOgfn = responsemodal.value;

					responseModel = await AddFamilyMemberRelation(model);

					if(responseModel.Status == ResponseStatus.Fail)
					{
						responsemodal.Status = ResponseStatus.Fail;
					}

				}else {
					responsemodal.Status = ResponseStatus.Fail;
					responseModel.Status = ResponseStatus.Fail;
					responseModel.ResponseCode = responsemodal.Code;
				}



				return responseModel as ResponseModel<People> ;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<People> responsemodal = new ResponseModel<People>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.ResponseCode = "0";

				return responsemodal as ResponseModel<People> ;
			}
			finally{
			
				_loader.hideLoader();
			}
		}
		#endregion


		#region Add Relationship
		public async Task<ResponseModel<People>> AddFamilyMemberRelation(People model)
		{
			_loader.showLoader ();
			ResponseModel<People> responseModel = new ResponseModel<People>();
			//Hit service using HTTP Client
			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				string relationType = GetTypeDetail(model.Gender,model.RelationType);

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.ADD_PERSON_SESSION_ID] = model.SessionId;
				param[AppConstant.ADD_PERSON_INDIOGFN] = model.LoggedinUserINDIOFGN;
				param[AppConstant.ADD_RELATION_INDIOGFN] = model.IndiOgfn;
				param[AppConstant.ADD_RELATION_TYPE] = relationType;
				param[AppConstant.FAMOGFNKEY] = model.LoggedinUserFAMOFGN;

				string url = WebServiceHelper.GetWebServiceURL(AppConstant.ADD_RELATION_SERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String responserelation = response.Content.ReadAsStringAsync().Result;

				Mvx.Trace("relation response : "+ responserelation);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (responserelation);

				ResponseDataModel responsemodal = DataParser.GetAddMemberRelationDetails (dict);

				if(responsemodal.Code.Equals("0")){
					responseModel.Status = ResponseStatus.OK;	
				}else{
					responseModel.Status = ResponseStatus.Fail;
				}

				responseModel.Content = model;
				responseModel.ResponseCode = responsemodal.Code;
				return responseModel as ResponseModel<People> ;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<ResponseDataModel> responsemodal = new ResponseModel<ResponseDataModel>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.ResponseCode = "0";
				return responseModel as ResponseModel<People> ;
			}
			finally{
				_loader.hideLoader();
			}
		}

		#endregion

		private string GetTypeDetail(string gender,string relation)
		{
			string type = "";

			if (gender == null)
				gender = String.Empty;

			if(gender.Equals("Male")){
				if(relation.Equals("Sibling")){
					type = AppConstant.BROTHER_RELATIONSHIP;
				}else if(relation.Equals("Parent")){
					type = AppConstant.FATHER_RELATIONSHIP;
				}else if(relation.Equals("Grandparent")){
					type = AppConstant.UNKNOWN_RELATIONSHIP;
				}else if(relation.Equals("Great Grandparent")){
					type = AppConstant.UNKNOWN_RELATIONSHIP;
				}
			}else{
				if(relation.Equals("Sibling")){
					type = AppConstant.SISTER_RELATIONSHIP;
				}else if(relation.Equals("Parent")){
					type = AppConstant.MOTHER_RELATIONSHIP;
				}else if(relation.Equals("Grandparent")){
					type = AppConstant.UNKNOWN_RELATIONSHIP;
				}else if(relation.Equals("Great Grandparent")){
					type = AppConstant.UNKNOWN_RELATIONSHIP;
				}
				else if(relation.ToLower().Equals("mother")){
					type = AppConstant.MOTHER_RELATIONSHIP;
				}else if(relation.ToLower().Equals("father")){
					type = AppConstant.FATHER_RELATIONSHIP;
				}else if(relation.ToLower().Equals("daughter")){
					type = AppConstant.DAUGHTER_RELATIONSHIP;
				}
				else if(relation.ToLower().Equals("son")){
					type = AppConstant.SON_RELATIONSHIP;
				}else if(relation.ToLower().Equals("sister")){
					type = AppConstant.SISTER_RELATIONSHIP;
				}else if(relation.ToLower().Equals("brother")){
					type = AppConstant.BROTHER_RELATIONSHIP;
				}
				else {
					type = AppConstant.UNKNOWN_RELATIONSHIP;
				}
			}
			return type;
		}

		#region Helper Methods
		string GetName(People member)
		{
			//return String.Format ("{0} \"{1}\" /{2}/", member.FirstName, member.MiddleName, member.LastName);

			return String.Format ("{0} {1} {2}", member.FirstName, member.MiddleName, member.LastName);
		}
		#endregion
	}
}

