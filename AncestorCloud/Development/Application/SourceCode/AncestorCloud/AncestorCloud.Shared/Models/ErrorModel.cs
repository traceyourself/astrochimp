using System;

namespace AncestorCloud.Shared
{
	public class ErrorModel : BaseModel
	{
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
	}
}

