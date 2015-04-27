using System;
using System.Threading.Tasks;
using System.Net.Http;
using ModernHttpClient;
using Cirrious.CrossCore;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AncestorCloud.Shared
{
	public class SignUpService : ISignUpService
	{
		#region ISignUpService implementation
		public async Task<ResponseModel<LoginModel>> SignUp (string name, string email, string password, string developerId, string developerPassword)
		{

			try
			{

				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");


				//== hit for getting session id
				email = "mikeyamadeo@gmail.com";
				password = "password";

				String url = "https://wsdev.onegreatfamily.com/v11.02/User.svc/Signin?username="+email+"&Password="+password+"&DeveloperId="+developerId+"&DeveloperPassword="+developerPassword;
				Mvx.Trace(url);

				var response = await client.GetAsync(url);
				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("SignUp response : "+res);

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				LoginModel modal = DataParser.GetSignUpDetails (dict);
				Mvx.Trace("Parced Values : "+modal.Code+" : "+modal.Message+" : "+modal.Value);
				ResponseModel<LoginModel> responsemodal = new ResponseModel<LoginModel>();
				responsemodal.loginModal = modal;
				responsemodal.Status = ResponseStatus.OK;
				//==


				//hit for sign up
				/*url = "https://wsdev.onegreatfamily.com/v11.02/User.svc/Create?SessionId="+modal.Value+"&EmailAddress="+email+"&Password="+password+"&FirstName="+name+"&LastName="+name+"&ProductId="+"Pro_id";
				response = await client.GetAsync(url);
				res = response.Content.ReadAsStringAsync().Result;
				Mvx.Trace("sign up : "+res );
				*/

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
		}
		#endregion


	}
}

