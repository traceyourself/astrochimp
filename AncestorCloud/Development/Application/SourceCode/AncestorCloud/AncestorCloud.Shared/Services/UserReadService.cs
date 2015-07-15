using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AncestorCloud.Shared
{
	public class UserReadService : IUserReadService
	{
		private ILoader _loader;

		public UserReadService()
		{
			_loader = Mvx.Resolve<ILoader> ();
		}

		public async Task<ResponseModel<LoginModel>> MakeUserReadService(LoginModel model)
		{
			_loader.showLoader ();
			//https://wsdev.onegreatfamily.com/v11.02/User.svc/Read?SessionId=s4zxi523e3hlgnhbgjh3hlm4
			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				//String url = "https://wsdev.onegreatfamily.com/v11.02/User.svc/Read?SessionId=s4zxi523e3hlgnhbgjh3hlm4";

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.SESSIONID]=model.Value;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.USEREADSERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				//System.Diagnostics.Debug.WriteLine ("Login response : "+res);

				Mvx.Trace("--User read response : "+res);


				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);                      

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;
						model= DataParser.GetUserReadData(model,dict);

						ResponseModel<LoginModel> avatarModel = await CheckIfAvatarAvailable(model);
						model.AvatarOGFN = avatarModel.Content.AvatarOGFN;
						model.AvatarURL = avatarModel.Content.AvatarURL;
					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
						responsemodal.ResponseCode = dict[AppConstant.CODE].ToString();
					}
				}
					
				responsemodal.Content= model;

				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.ResponseCode = "0";

				return responsemodal;
			}
			finally{

				_loader.hideLoader();
			}

		}

		//Get User Avatar Info
		public async Task<ResponseModel<LoginModel>> CheckIfAvatarAvailable(LoginModel model)
		{
			_loader.showLoader ();
			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.SESSIONID]=model.Value;
				param[AppConstant.INDIOGFN]=model.IndiOGFN;
				param[AppConstant.MEDIATYPEKEY]=AppConstant.AVATAR;
				param[AppConstant.STACKTRACE]=AppConstant.TRUE;

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.MEDIA_LISTREAD_SERVICE,param);

				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				Mvx.Trace("Check media list response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						LoginModel avatarModel = DataParser.GetAvatarAvailabiltyData(dict);
						if(avatarModel.AvatarOGFN != null)
						{
							model.AvatarOGFN = avatarModel.AvatarOGFN;

							//http://wsdev.onegreatfamily.com/v11.02/Media.svc/AvatarRead?avatarOgfn=267861&ImageType=png&
							//ImageSize=100%2c120&sessionId=duujwkoj2gfftdq2jdp4yd3e&stacktrace

							Dictionary <string,string> avatarParam = new Dictionary<string, string>();

							avatarParam[AppConstant.SESSIONID]=model.Value;
							avatarParam[AppConstant.AVATAR_OGFN]=avatarModel.AvatarOGFN;
							avatarParam[AppConstant.IMAGE_TYPE]=AppConstant.PNG;
							avatarParam[AppConstant.IMAGE_SIZE]="200"+"%2c"+"200";
							avatarParam[AppConstant.STACKTRACE]=AppConstant.TRUE;

							String AvatarUrl = WebServiceHelper.GetWebServiceURL(AppConstant.AVATAR_IMAGE_SERVICE,avatarParam);
							Mvx.Trace("Avatar URL : "+AvatarUrl);

							model.AvatarURL = AvatarUrl;

							responsemodal.Status = ResponseStatus.OK;
						}
					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
						responsemodal.ResponseCode = dict[AppConstant.CODE].ToString();
					}
				}

				responsemodal.Content= model;

				return responsemodal;

			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				//return CommonConstants.FALSE;
				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.ResponseCode = "0";
				return responsemodal;
			}
			finally{

				_loader.hideLoader();
			}

		}
		//Get Avatar Info ===============

	}
}

