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
						if(datamodal.value.Length > 0){
							string []OgfnArr = datamodal.value.Split(new char[]{','},100);
							for(int i=0;i<OgfnArr.Length;i++){
								bool doesItExists = Convert.ToBoolean (_databaseService.IsMemberExists(OgfnArr[i],loginModel.UserEmail));
								if(!doesItExists){
									ResponseModel<People> responseM = await _indiDetailService.GetIndiFamilyDetails(OgfnArr[i],loginModel.Value);
									if(responseM.Status == ResponseStatus.OK){
										FamilyMembers.Add(responseM.Content);
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

