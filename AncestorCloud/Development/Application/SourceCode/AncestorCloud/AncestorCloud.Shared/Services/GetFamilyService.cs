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

		#region

		public async Task<ResponseModel<List<People>>> GetFamilyMembers(LoginModel model)
		{
			_loader.showLoader ();

			ResponseModel<List<People>> responseModel = new ResponseModel<List<People>>();
			//Hit service using HTTP Client
			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.SESSIONID] = model.Value ;
				param[AppConstant.FAMILY_OGFN] = model.FamOGFN;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.FAMILY_READ_SERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				Mvx.Trace ("Get family response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseDataModel datamodal = DataParser.GetFamilyMembers(dict);

				LoginModel loginModel = _databaseService.GetLoginDetails();
				List<People> FamilyMembers = new List<People>();

				if(datamodal.Code.Equals("0")){
					try{
						if(datamodal.CHILD_OFGNS.Length > 0){

							string []OgfnArr = datamodal.CHILD_OFGNS.Split(new char[]{','},200);

							for(int i=0;i<OgfnArr.Length;i++)
							{
								bool doesItExists = Convert.ToBoolean (_databaseService.IsMemberExists(OgfnArr[i],loginModel.UserEmail));

								if(!doesItExists)
								{
									ResponseModel<People> responseM = await _indiDetailService.GetIndiFamilyDetails(OgfnArr[i],loginModel.Value);
								
									if(responseM.Status == ResponseStatus.OK){
										People p = responseM.Content;

										p.LoginUserLinkID = loginModel.UserEmail;
										p.Relation = AppConstant.Sibling_comparison;
										_databaseService.InsertFamilyMember(p);
										FamilyMembers.Add(p);
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
										ResponseModel<People> responseM = await _indiDetailService.GetIndiFamilyDetails(datamodal.FATHER_OFGN,loginModel.Value);

										if(responseM.Status == ResponseStatus.OK){
											People p = responseM.Content;
											p.LoginUserLinkID = loginModel.UserEmail;
											p.Relation = AppConstant.Father_comparison;
											_databaseService.InsertFamilyMember(p);
											FamilyMembers.Add(p);
										}
									}else
									{
										People p = _databaseService.GetFamilyMember(datamodal.FATHER_OFGN,loginModel.UserEmail);
										p.Relation = AppConstant.Father_comparison;
										FamilyMembers.Add(p);
									}		
								}
							}

							if(datamodal.MOTHER_OFGN != null)
							{
								if(datamodal.MOTHER_OFGN.Length > 0)
								{
									int count = _databaseService.IsMemberExists(datamodal.MOTHER_OFGN,loginModel.UserEmail);

									if(count == 0)
									{
										ResponseModel<People> responseM = await _indiDetailService.GetIndiFamilyDetails(datamodal.MOTHER_OFGN,loginModel.Value);

										if(responseM.Status == ResponseStatus.OK){
											People p = responseM.Content;
											p.LoginUserLinkID = loginModel.UserEmail;
											p.Relation = AppConstant.Mother_comparison;
											_databaseService.InsertFamilyMember(p);
											FamilyMembers.Add(p);
										}
									}else
									{
										People p = _databaseService.GetFamilyMember(datamodal.MOTHER_OFGN,loginModel.UserEmail);
										p.Relation = AppConstant.Mother_comparison;
										FamilyMembers.Add(p);
									}		
								}
							}


						}
					}catch(Exception e){
						Mvx.Trace(e.StackTrace);
					}

					responseModel.Status = ResponseStatus.OK;

				}else{
					responseModel.Status = ResponseStatus.Fail;
				}

				responseModel.Content = FamilyMembers;




				return responseModel;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<List<People>> responsemodel = new ResponseModel<List<People>>();
				responsemodel.Status = ResponseStatus.Fail;

				return responseModel;
			}
			finally{

				_loader.hideLoader();
			}
		}

		#endregion

	}
}