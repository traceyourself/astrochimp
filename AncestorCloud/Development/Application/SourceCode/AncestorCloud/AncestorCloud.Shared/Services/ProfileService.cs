using System;
using System.IO;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Newtonsoft.Json;

namespace AncestorCloud.Shared
{
	public class ProfileService : IProfileService
	{
		private readonly ILoader _loader;


		public ProfileService()
		{
			_loader = Mvx.Resolve<ILoader> ();

		}

		#region IProfileService implementation

		public async System.Threading.Tasks.Task<ResponseModel<ResponseDataModel>> PostProfileData (LoginModel login, Stream stream)
		{
			_loader.showLoader ();

			try   
			{
				HttpClient client = new HttpClient(new NativeMessageHandler());
				client.DefaultRequestHeaders.Add("Accept","application/json");
				client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type","text/raw");

				Dictionary <string,string> param = new Dictionary<string, string>();

				param[AppConstant.SESSIONID] = login.Value;
				param[AppConstant.INDIOGFN] = login.IndiOGFN;
				param[AppConstant.FILENAMEKEY] = AppConstant.FILENAME;
				param[AppConstant.FILETYPEKEY] = AppConstant.FILETYPE;
				param[AppConstant.MEDIATYPEKEY] = AppConstant.AVATAR;
				param [AppConstant.MEDIATITLEKEY] = AppConstant.MEDIATITLE;
			
				//http://wsdev.onegreatfamily.com/v11.02/Individual.svc/MediaCreate?IndiOgfn=747567208
				//&FileName=test.png&FileType=png&Title=test&MediaType=AVATAR&sessionId=sx10dccmtw5njgempffdcsey

				String url = WebServiceHelper.GetWebServiceURL(AppConstant.UPLOAD_MEDIA_SERVICE,param);

				Mvx.Trace("Upload Service URL:- "+url);

				var response = await client.PostAsync(url, new StreamContent(stream));

				String res = response.Content.ReadAsStringAsync().Result;

				System.Diagnostics.Debug.WriteLine ("PostProfileData response : "+res );// "response.EnsureSuccessStatusCode(); " +response.EnsureSuccessStatusCode());

				Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (res);

				ResponseDataModel model = DataParser.GetAddMemberDetails(dict);

				ResponseModel<ResponseDataModel> responsemodal = new ResponseModel<ResponseDataModel>();

				if(dict.ContainsKey(AppConstant.MESSAGE))
				{
					if(dict[AppConstant.MESSAGE].Equals((AppConstant.SUCCESS)))
					{
						responsemodal.Status = ResponseStatus.OK;
						responsemodal.Content = model;
					}else
					{
						responsemodal.Status = ResponseStatus.Fail;
						responsemodal.ResponseCode = dict[AppConstant.CODE].ToString();
					}
				}
				return responsemodal;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.StackTrace);
				ResponseModel<ResponseDataModel> responsemodal = new ResponseModel<ResponseDataModel>();
				responsemodal.Status = ResponseStatus.Fail;
				responsemodal.ResponseCode = "0";

				return responsemodal;
			}
			finally{
				_loader.hideLoader();
			}
		}
		#endregion
	}
}

