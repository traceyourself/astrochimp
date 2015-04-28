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

		private ILoader _loader;

		public LoginService()
		{
			_loader = Mvx.Resolve<ILoader> ();
		}

		#region ILoginService implementationsw323we3we33wa3w34

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


				String url = "https://wsdev.onegreatfamily.com/v11.02/User.svc/Signin?username="+email+"&Password="+password+"&DeveloperId="+developerId+"&DeveloperPassword="+developerPassword;
				Mvx.Trace(url);

				var response = await client.GetAsync(url);

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("Login response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				LoginModel modal = DataParser.GetLoginDetails (dict);

				Mvx.Trace("Parced Values : "+modal.Code+" : "+modal.Message+" : "+modal.Value);

				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.loginModal = modal;
				responsemodal.Status = ResponseStatus.OK;

				return responsemodal;
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


		#endregion

	}
}

