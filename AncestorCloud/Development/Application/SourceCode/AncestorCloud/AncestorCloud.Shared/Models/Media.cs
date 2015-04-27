using System;

namespace AncestorCloud.Shared
{
	public class Media : BaseModel
	{
		public string MediaName { get; set; }
		public string MediaURL { get; set; }
		public MediaType Type { get; set; }
	}
}

