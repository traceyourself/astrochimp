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
	public class LoginService : ILoginService
	{

		private readonly ILoader _loader;
		private readonly IUserReadService _userReadService;

		public LoginService()
		{
			_loader = Mvx.Resolve<ILoader> ();
			_userReadService = Mvx.Resolve<IUserReadService> ();
		}

		#region ILoginService implementation

		public async Task<ResponseModel<LoginModel>> Login(string email,string password, string developerId, string developerPassword)
		{
			_loader.showLoader ();

			//Hit service using HTTP Client
			try   
			{
				
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");

				/*email = "mikeyamadeo@gmail.com";
				password = "password";*/

				String url = "https://ws.onegreatfamily.com/v11.02/User.svc/Signin?username="+email+"&Password="+password+"&DeveloperId="+developerId+"&DeveloperPassword="+developerPassword;
				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				string res = response.Content.ReadAsStringAsync().Result;
				 
				System.Diagnostics.Debug.WriteLine ("Login response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				LoginModel modal = DataParser.GetLoginDetails (dict);

				modal.UserEmail = email;

				Mvx.Trace("Parced Values : "+modal.Code+" : "+modal.Message+" : "+modal.Value);

				ResponseModel<LoginModel> userReadResponse = new ResponseModel<LoginModel>();

				if(dict.ContainsKey(AppConstant.Message))
				{
					if(dict[AppConstant.Message].Equals((AppConstant.SUCCESS)))
					{
						userReadResponse = await _userReadService.MakeUserReadService(modal);
						userReadResponse.Status = ResponseStatus.OK;


					}else
					{
						userReadResponse.Status = ResponseStatus.Fail;
					}
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

		#region my region
		//http://ws.corp.local/v11.02/Individual.svc/MediaListRead?IndiOgfn=747567208&MediaType=AVATAR
		//&sessionId=gaaw1krq0mm350bioal0suv2&stacktrace=true
		#endregion

	}
}

