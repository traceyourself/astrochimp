using System;

namespace AncestorCloud.Shared
{
	public class ResponseModel<T> : BaseModel
	{
		public string ResponseError {get;set;}
		public ResponseStatus Status{ get; set;}
		public string ResponseCode{ get; set;}
		public T Content { get; set; }
	}
}

