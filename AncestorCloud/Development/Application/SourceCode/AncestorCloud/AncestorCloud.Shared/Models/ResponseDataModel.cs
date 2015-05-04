using System;

namespace AncestorCloud.Shared
{
	public class ResponseDataModel : BaseModel
	{
		public ErrorModel Error { get; set; }
		//rest will follow 
		public String Code {get; set;}
		public String Message { get; set;}
		public String value{ get; set;}
	}
}

