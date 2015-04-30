﻿using System;

namespace AncestorCloud.Shared
{
	public class ResponseModel<T> : BaseModel
	{
		public ResponseStatus Status{ get; set;}
		public T Content { get; set; }
	}
}

