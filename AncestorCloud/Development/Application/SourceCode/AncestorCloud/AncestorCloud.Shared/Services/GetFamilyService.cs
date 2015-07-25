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
	public class GetFamilyService : IGetFamilyService
	{

		private ILoader _loader;
		private IDatabaseService _databaseService;
		private IIndiDetailService _indiDetailService;

		public GetFamilyService()
		{
			_loader = Mvx.Resolve<ILoader> ();
			_databaseService = Mvx.Resolve<IDatabaseService> ();
			_indiDetailService = Mvx.Resolve<IIndiDetailService> ();
		}


		#region call service for get Family
		public async Task<ResponseDataModel> MakeServiceCall(string sessionID, string FamOGFN)
		{
			_loader.showLoader ();

			ResponseDataModel datamodal = new ResponseDataModel();

			try{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.SESSIONID] = sessionID ;
				param[AppConstant.FAMILY_OGFN] = FamOGFN;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.FAMILY_READ_SERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				Mvx.Trace ("Get family response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				datamodal = DataParser.GetFamilyMembers(dict);
			}
			catch(Exception e) 
			{
				
			}
			finally{

				_loader.hideLoader ();
			}


			return datamodal;
		}
		#endregion

		#region

		public async Task<ResponseModel<List<People>>> GetFamilyMembers(LoginModel model)
		{
			_loader.showLoader ();

			ResponseModel<List<People>> responseModel = new ResponseModel<List<People>>();
			//Hit service using HTTP Client
			try   
			{
				ResponseDataModel datamodal = await MakeServiceCall(model.Value,model.FamOGFN);

				LoginModel loginModel = _databaseService.GetLoginDetails();

				if(datamodal.Code.Equals("0"))
				{
					List<People> FamilyMembers = await GetFamily(datamodal,loginModel);

					if(FamilyMembers != null)
					{
						List<People> grandParents = await GetGrandParents(FamilyMembers,loginModel);

						FamilyMembers.AddRange(grandParents);
					}

					if(FamilyMembers != null)
					{
						List<People> greatGrandParents = await GetGreatGrandParents(FamilyMembers,loginModel);

						FamilyMembers.AddRange(greatGrandParents);
					}

					responseModel.Status = ResponseStatus.OK;

					responseModel.Content = FamilyMembers;

				}else{
					responseModel.Status = ResponseStatus.Fail;
					responseModel.ResponseCode = datamodal.Code;
				}

				return responseModel;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<List<People>> responsemodel = new ResponseModel<List<People>>();
				responsemodel.Status = ResponseStatus.Fail;
				responsemodel.ResponseCode = "0";
				return responseModel;
			}
			finally{

				_loader.hideLoader();
			}
		}

		#endregion

		#region Get Family

		public async Task<List<People>> GetFamily(ResponseDataModel datamodal,LoginModel loginModel)
		{
			List<People> FamilyMembers = new List<People>();

			if(datamodal.Code.Equals("0")){
				try{
					if(datamodal.CHILD_OFGNS.Length > 0 ){
					
							string []OgfnArr = datamodal.CHILD_OFGNS.Split(new char[]{','},200);

							for(int i=0;i<OgfnArr.Length;i++)
							{
								bool doesItExists = Convert.ToBoolean (_databaseService.IsMemberExists(OgfnArr[i],loginModel.UserEmail));

								if(!doesItExists)
								{
									ResponseModel<People> responseM = await _indiDetailService.GetIndiFamilyDetails(OgfnArr[i],loginModel.Value);

									if(responseM.Status == ResponseStatus.OK){
										People p = responseM.Content;

										try{
											p.FirstName = p.FirstName ?? string.Empty;
											p.MiddleName = p.MiddleName ?? string.Empty;
											p.LastName = p.LastName ?? string.Empty;
											p.Name = p.Name ?? string.Empty;

											if(p.FirstName.Length > 0 || p.MiddleName.Length > 0 || p.LastName.Length > 0 || p.Name.Length > 0){
												p.LoginUserLinkID = loginModel.UserEmail;
												p.Relation = AppConstant.Sibling_comparison;
												
												_databaseService.InsertFamilyMember(p);
												FamilyMembers.Add(p);
											}
										}catch(Exception e)
										{
											Mvx.Trace(e.StackTrace);
										}
									}
								}else
								{
									People p = _databaseService.GetFamilyMember(OgfnArr[i],loginModel.UserEmail);
									p.Relation = AppConstant.Sibling_comparison;
									FamilyMembers.Add(p);
								}
							}
						

						if(datamodal.FATHER_OFGN != null)
						{
							if(datamodal.FATHER_OFGN.Length > 0)
							{
								int count = _databaseService.IsMemberExists(datamodal.FATHER_OFGN,loginModel.UserEmail);

								if(count == 0)
								{
									/*ResponseModel<People> responseM = await _indiDetailService.GetIndiFamilyDetails(datamodal.FATHER_OFGN,loginModel.Value);

									if(responseM.Status == ResponseStatus.OK){
										People p = responseM.Content;
										p.LoginUserLinkID = loginModel.UserEmail;
										p.Relation = AppConstant.Father_comparison;
										p.Gender = AppConstant.MALE;
										_databaseService.InsertFamilyMember(p);
										FamilyMembers.Add(p);

										//Mvx.Trace("Father : "+p.FirstName+" : "+p.Relation+" : "+p.FamOGFN);
									}*/

									People p = await FetchFather(datamodal,loginModel,false);
									if(p != null)
										FamilyMembers.Add(p);

								}else
								{

									People p = _databaseService.GetFamilyMember(datamodal.FATHER_OFGN,loginModel.UserEmail);
									if(p.FamOGFN == null || p.FamOGFN.Equals("0"))
									{
										People peop  = await FetchFather(datamodal,loginModel,true);
										if(peop != null)
											p = peop;
									}else{
										p.Relation = AppConstant.Father_comparison;
										p.Gender = AppConstant.MALE;
									}

									FamilyMembers.Add(p);

									/*People p = _databaseService.GetFamilyMember(datamodal.FATHER_OFGN,loginModel.UserEmail);
									p.Relation = AppConstant.Father_comparison;
									p.Gender = AppConstant.MALE;
									FamilyMembers.Add(p);*/
									//Mvx.Trace("Father : "+p.FirstName+" : "+p.Relation+" : "+p.FamOGFN);
								}		
							}

						}

						if(datamodal.MOTHER_OFGN != null)
						{
							if(datamodal.MOTHER_OFGN.Length > 0)
							{
								int count = _databaseService.IsMemberExists(datamodal.MOTHER_OFGN,loginModel.UserEmail);

								if(count == 0 )
								{
									/*ResponseModel<People> responseM = await _indiDetailService.GetIndiFamilyDetails(datamodal.MOTHER_OFGN,loginModel.Value);

									if(responseM.Status == ResponseStatus.OK){
										People p = responseM.Content;
										p.LoginUserLinkID = loginModel.UserEmail;
										p.Relation = AppConstant.Mother_comparison;
										p.Gender = AppConstant.FEMALE;
										_databaseService.InsertFamilyMember(p);
										FamilyMembers.Add(p);
										//Mvx.Trace("Mother : "+p.FirstName+" : "+p.Relation+" : "+p.FamOGFN);
									}*/

									People p = await FetchMother(datamodal,loginModel,false);
									if(p != null)
										FamilyMembers.Add(p);

								}else                                                                                         
								{
									People p = _databaseService.GetFamilyMember(datamodal.MOTHER_OFGN,loginModel.UserEmail);
									if(p.FamOGFN == null || p.FamOGFN.Equals("0"))
									{
										People peop  = await FetchMother(datamodal,loginModel,true);
										if(peop != null)
											p = peop;
									}else{
										p.Relation = AppConstant.Mother_comparison;
										p.Gender = AppConstant.FEMALE;
									}

									//if(p != null)
										FamilyMembers.Add(p);
								}		
							}
						}


					}
				}catch(Exception e){
					Mvx.Trace(e.StackTrace);
				}
			}

			return FamilyMembers;
		}
		#endregion

		#region fetch mother
		public async Task<People> FetchMother(ResponseDataModel datamodal,LoginModel loginModel,bool isExist)
		{
			ResponseModel<People> responseM = await _indiDetailService.GetIndiFamilyDetails(datamodal.MOTHER_OFGN,loginModel.Value);

			if (responseM.Status == ResponseStatus.OK) {
				People p = responseM.Content;
				p.LoginUserLinkID = loginModel.UserEmail;
				p.Relation = AppConstant.Mother_comparison;
				p.Gender = AppConstant.FEMALE;
				if (isExist) {
					_databaseService.UpdateRelative (p);
				} else {
					_databaseService.InsertFamilyMember (p);
				}

				return p;
				//Mvx.Trace("Mother : "+p.FirstName+" : "+p.Relation+" : "+p.FamOGFN);
			} else {
				return null;
			}
		}
		#endregion

		#region fetch father
		public async Task<People> FetchFather(ResponseDataModel datamodal,LoginModel loginModel,bool isExist)
		{
			ResponseModel<People> responseM = await _indiDetailService.GetIndiFamilyDetails(datamodal.FATHER_OFGN,loginModel.Value);

			if (responseM.Status == ResponseStatus.OK) {
			
				People p = responseM.Content;
				p.LoginUserLinkID = loginModel.UserEmail;
				p.Relation = AppConstant.Father_comparison;
				p.Gender = AppConstant.MALE;

				if (isExist) {
					_databaseService.UpdateRelative (p);
				} else {
					_databaseService.InsertFamilyMember (p);
				}

				return p;

			} else {
				return null;
			}
		}
		#endregion



		#region get GrandParents
		public async Task<List<People>> GetGrandParents(List<People> FamilyMembers, LoginModel model)
		{

			List<People> parents = new List<People>();

			foreach(People p in FamilyMembers)
			{
				if(p.Relation.Equals(AppConstant.Father_comparison))
				{
					ResponseDataModel datamodal = await MakeServiceCall(model.Value, p.FamOGFN);

					if(datamodal.Code.Equals("0"))
					{
						List<People> mparents = await FetchGrandParents(datamodal,model,AppConstant.GrandParent_comparison,AppConstant.Father_Reference,null);
						parents.AddRange (mparents);
					}
				}
				else if(p.Relation.Equals(AppConstant.Mother_comparison))
				{
					ResponseDataModel datamodal = await MakeServiceCall(model.Value, p.FamOGFN);

					if(datamodal.Code.Equals("0"))
					{
						List<People> mparents = await FetchGrandParents(datamodal,model,AppConstant.GrandParent_comparison,AppConstant.Mother_Reference,null);
						parents.AddRange (mparents);
					}
				}
				else if(p.Relation.Equals(AppConstant.Parent_comparison))
				{
					ResponseDataModel datamodal = await MakeServiceCall(model.Value, p.FamOGFN);

					if(datamodal.Code.Equals("0"))
					{
						List<People> mparents = await FetchGrandParents(datamodal,model,AppConstant.GrandParent_comparison,AppConstant.Parent_comparison,null);
						parents.AddRange (mparents);
					}
				}
			}

			return parents;
		}

		#endregion


		#region Fetch GrandParents

		public async  Task<List<People>> FetchGrandParents(ResponseDataModel datamodal, LoginModel loginModel, String relation, string relationShipRef,string genderRef)
		{
			List<People> grandParent = new List<People>();

			if(datamodal.FATHER_OFGN != null)
			{
				if(datamodal.FATHER_OFGN.Length > 0 && !datamodal.FATHER_OFGN.Equals("0"))
				{
					int count = _databaseService.IsMemberExists(datamodal.FATHER_OFGN,loginModel.UserEmail);

					if(count == 0)
					{
						People p = await FetchGrandFather(datamodal,loginModel,relation,relationShipRef,false,genderRef);
						if(p != null)
							grandParent.Add(p);
					}
					else
					{
						People p = _databaseService.GetFamilyMember(datamodal.FATHER_OFGN,loginModel.UserEmail);
						if (!relation.Equals(AppConstant.GreatGrandParent_comparison) && (p.FamOGFN == null || p.FamOGFN.Equals ("0"))) {
							
							People _p = await FetchGrandFather(datamodal,loginModel,relation,relationShipRef,true,genderRef);
							if(_p != null)
								grandParent.Add(_p);
						}else
						{
							p.Relation = relation;
							p.Gender = AppConstant.MALE;
							p.RelationReference = GerRelationShipRef(relationShipRef,p.Gender,relation,genderRef);
							grandParent.Add(p);
						}
					}		
				}
			}

			if(datamodal.MOTHER_OFGN != null)
			{
				if(datamodal.MOTHER_OFGN.Length > 0 && !datamodal.MOTHER_OFGN.Equals("0"))
				{
					int count = _databaseService.IsMemberExists(datamodal.MOTHER_OFGN,loginModel.UserEmail);

					if(count == 0)
					{
						People p = await FetchGrandMother(datamodal,loginModel,relation,relationShipRef,false,genderRef);
						if(p != null)
							grandParent.Add(p);
					}else
					{
						People p = _databaseService.GetFamilyMember(datamodal.MOTHER_OFGN,loginModel.UserEmail);
						if (!relation.Equals(AppConstant.GreatGrandParent_comparison) && (p.FamOGFN == null || p.FamOGFN.Equals ("0"))) {

							People _p = await FetchGrandMother (datamodal,loginModel,relation,relationShipRef,true,genderRef);
							if (_p != null)
								grandParent.Add (_p);
						} else {
							p.Relation = AppConstant.GrandParent_comparison;
							p.Gender = AppConstant.FEMALE;
							p.RelationReference = GerRelationShipRef (relationShipRef, p.Gender, relation,genderRef);
							grandParent.Add (p);
						}
					}		
				}
			}

			return grandParent;

		}

		#endregion


		#region Fetch Grand Parents


		async Task<People> FetchGrandFather( ResponseDataModel datamodal, LoginModel loginModel, String relation, string relationShipRef,bool isExist,string genderRef)
		{
			ResponseModel<People> responseM = await _indiDetailService.GetIndiFamilyDetails(datamodal.FATHER_OFGN,loginModel.Value);

			if(responseM.Status == ResponseStatus.OK){
				People p = responseM.Content;
				p.LoginUserLinkID = loginModel.UserEmail;
				p.Relation = relation;
				p.Gender = AppConstant.MALE;
				p.RelationReference = GerRelationShipRef(relationShipRef,p.Gender,relation,genderRef);

				if (isExist) {
					_databaseService.UpdateRelative (p);
				} else {
					_databaseService.InsertFamilyMember (p);
				}
				return p;
			}

			return null;
		}

		async Task<People> FetchGrandMother(ResponseDataModel datamodal, LoginModel loginModel, String relation, string relationShipRef,bool isExist, string genderRef)
		{
			ResponseModel<People> responseM = await _indiDetailService.GetIndiFamilyDetails(datamodal.MOTHER_OFGN,loginModel.Value);

			if(responseM.Status == ResponseStatus.OK){
				People p = responseM.Content;
				p.LoginUserLinkID = loginModel.UserEmail;
				p.Relation = relation;
				p.Gender = AppConstant.FEMALE;
				p.RelationReference = GerRelationShipRef(relationShipRef,p.Gender,relation,genderRef);
				if (isExist) {
					_databaseService.UpdateRelative (p);
				} else {
					_databaseService.InsertFamilyMember (p);
				}
				return p;
			}

			return null;
		}

		#endregion

		#region Get GreatGrandParents

		public async Task<List<People>> GetGreatGrandParents(List<People> FamilyMembers, LoginModel model)
		{
			List<People> list = new List<People> ();

			foreach(People p in FamilyMembers)
			{
				if(p.Relation.Equals(AppConstant.GrandParent_comparison))
				{
					ResponseDataModel datamodal = await MakeServiceCall(model.Value, p.FamOGFN);

					if(datamodal.Code.Equals("0"))
					{
						List<People> mlist = await FetchGrandParents (datamodal, model, AppConstant.GreatGrandParent_comparison,p.RelationReference,p.Gender);
						list.AddRange (mlist);
					}
				} 
			}
			return list;
		}

		#endregion

		string GerRelationShipRef(string reference, string gender, string relation,string parentGenderRef)
		{
			if (reference.Equals (AppConstant.Parent_comparison)) {
				if (gender.Equals (AppConstant.MALE))
					return AppConstant.Father_Reference;
				else if (gender.Equals (AppConstant.FEMALE))
					return AppConstant.Mother_Reference;
			}

			if (relation.Equals (AppConstant.GreatGrandParent_comparison)) {

				if (reference.Equals (AppConstant.Father_Reference)) {
					
					if (parentGenderRef.Equals (AppConstant.MALE))
						return AppConstant.Grand_Father_Father_Reference;
					
					else if (parentGenderRef.Equals (AppConstant.FEMALE))
						return AppConstant.Grand_Mother_Father_Reference;
				}
				else if (reference.Equals (AppConstant.Mother_Reference)) {
					
					if (parentGenderRef.Equals (AppConstant.MALE))
						return AppConstant.Grand_Father_Mother_Reference;
					
					else if (parentGenderRef.Equals (AppConstant.FEMALE))
						return AppConstant.Grand_Mother_Mother_Reference;
				}
			}

			return reference;
		}


	}
}