using System;

namespace AncestorCloud.Shared
{
	public class ResponseModel<T> : BaseModel
	{
		public LoginModel loginModal{ get; set;}
		public ResponseStatus Status{ get; set;}
	}
}

