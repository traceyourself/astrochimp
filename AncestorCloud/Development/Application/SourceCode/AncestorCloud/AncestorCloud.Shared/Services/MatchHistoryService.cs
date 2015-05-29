using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AncestorCloud.Shared
{
	public class MatchHistoryService : IMatchHistoryService
	{
		private ILoader _loader;
		private IDatabaseService _databaseService;
		private IIndiDetailService _indiDetailService;

		public MatchHistoryService()
		{
			_loader = Mvx.Resolve<ILoader> ();
			_databaseService = Mvx.Resolve<IDatabaseService> ();
			_indiDetailService = Mvx.Resolve<IIndiDetailService> ();
		}

		public async Task<ResponseModel<List<RelationshipFindResult>>> HistoryReadService(LoginModel model)
		{
			_loader.showLoader ();

			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.SESSIONID]=model.Value;

				//https://ws.onegreatfamily.com/v11.02/Individual.svc/RelationshipFindHistoryGet?sessionId=bvbushd0mlei3hcvuvcahba1

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.MATCHER_HISTORY_SERVICE,param);

				Mvx.Trace("Matcher history url :- "+url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				Mvx.Trace("history read response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);                      

				ResponseModel<List<RelationshipFindResult>> responsemodal = new ResponseModel<List<RelationshipFindResult>>();

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						List<RelationshipFindResult> dataList = DataParser.ReadDataHistory(dict);
						if(dataList.Count > 0){
							responsemodal.Content = await CheckPeopleData(dataList);
						}
						responsemodal.Status = ResponseStatus.OK;

					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
						responsemodal.Content = new List<RelationshipFindResult>();
					}
				}

				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<List<RelationshipFindResult>> responsemodal = new ResponseModel<List<RelationshipFindResult>>();
				responsemodal.Status = ResponseStatus.Fail;

				return responsemodal;
			}
			finally{
				_loader.hideLoader();
			}

		}

		#region check people data
		public async Task<List<RelationshipFindResult>> CheckPeopleData(List<RelationshipFindResult> dataList){

			LoginModel loginDetail = _databaseService.GetLoginDetails ();
			String useremail = ""+loginDetail.UserEmail;

			List<RelationshipFindResult> returnList = new List<RelationshipFindResult>();

			for(int i=0;i<dataList.Count;i++){
				RelationshipFindResult relationModel = dataList[i];

				// First Person validation
				if(_databaseService.IsMemberExists(""+relationModel.Indi1Ogfn,useremail) > 0){
					People people = _databaseService.GetMember (""+relationModel.Indi1Ogfn,useremail)[0];
					if(people != null){
						if(people.ProfilePicURL != null){
							if (people.ProfilePicURL.Length > 0) {
								relationModel.FirstPerson = people;
							}
						}else {
							loginDetail.IndiOGFN = ""+relationModel.Indi1Ogfn;
							ResponseModel<LoginModel> res = await _indiDetailService.GetIndiDetails(loginDetail);

							LoginModel result = res.Content;
							people.Name = result.Name;
							people.ProfilePicURL = GetAvatarUrl (result);

							relationModel.FirstPerson = people;
						}
					}
				}else {
					if(_databaseService.IsCelebExists(""+relationModel.Indi1Ogfn) > 0){
						Celebrity celeb = _databaseService.GetCelebrity (""+relationModel.Indi1Ogfn);
						People people = new People ();
						people.Name = celeb.GivenNames+" "+celeb.LastName;
						people.ProfilePicURL = celeb.Img;
						relationModel.FirstPerson = people;
					}else{
						People people = new People ();
						loginDetail.IndiOGFN = ""+relationModel.Indi1Ogfn;
						ResponseModel<LoginModel> res = await _indiDetailService.GetIndiDetails(loginDetail);

						LoginModel result = res.Content;
						people.Name = result.Name;
						people.ProfilePicURL = GetAvatarUrl (result);

						relationModel.FirstPerson = people;
					}
				}
				//=======


				//Second person Validation===
				if(_databaseService.IsMemberExists(""+relationModel.Indi2Ogfn,useremail) > 0){
					People people = _databaseService.GetMember (""+relationModel.Indi2Ogfn,useremail)[0];
					if(people != null){
						if(people.ProfilePicURL != null){
							if (people.ProfilePicURL.Length > 0) {
								relationModel.SecondPerson = people;
							} 
						} else {
							loginDetail.IndiOGFN = ""+relationModel.Indi2Ogfn;
							ResponseModel<LoginModel> res = await _indiDetailService.GetIndiDetails(loginDetail);

							LoginModel result = res.Content;
							people.Name = result.Name;
							people.ProfilePicURL = GetAvatarUrl (result);

							relationModel.SecondPerson = people;
						}
					}
				}else {
					if (_databaseService.IsCelebExists ("" + relationModel.Indi2Ogfn) > 0) {
						Celebrity celeb = _databaseService.GetCelebrity ("" + relationModel.Indi2Ogfn);
						People people = new People ();
						people.Name = celeb.GivenNames + " " + celeb.LastName;
						people.ProfilePicURL = celeb.Img;
						relationModel.SecondPerson = people;
					} else {
						People people = new People ();
						loginDetail.IndiOGFN = "" + relationModel.Indi2Ogfn;
						ResponseModel<LoginModel> res = await _indiDetailService.GetIndiDetails (loginDetail);

						LoginModel result = res.Content;
						people.Name = result.Name;
						people.ProfilePicURL = GetAvatarUrl (result);

						relationModel.SecondPerson = people;
					}
				}
				//==========

				/*if(i == 5){
					Mvx.Trace ("first at "+i+" : "+relationModel.Indi1Ogfn);
					Mvx.Trace ("second at "+i+" : "+relationModel.Indi2Ogfn);
				}*/
				returnList.Add (relationModel);
			}

			return returnList;
		}
		#endregion

		#region getAvatar Url
		public String GetAvatarUrl(LoginModel model){
			Dictionary <string,string> avatarParam = new Dictionary<string, string>();

			avatarParam[AppConstant.SESSIONID]=model.Value;
			avatarParam[AppConstant.AVATAR_OGFN]=model.AvatarOGFN;
			avatarParam[AppConstant.IMAGE_TYPE]=AppConstant.PNG;
			avatarParam[AppConstant.IMAGE_SIZE]="200"+"%2c"+"200";
			avatarParam[AppConstant.STACKTRACE]=AppConstant.TRUE;

			String AvatarUrl = WebServiceHelper.GetWebServiceURL(AppConstant.AVATAR_IMAGE_SERVICE,avatarParam);
			Mvx.Trace("Avatar URL : "+AvatarUrl);
			return AvatarUrl;
		}
		#endregion
	}
}

