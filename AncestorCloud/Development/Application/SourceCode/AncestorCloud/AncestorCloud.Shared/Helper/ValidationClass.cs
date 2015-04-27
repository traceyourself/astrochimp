using System;

namespace AncestorCloud.Shared
{
	public class ValidationClass
	{
		public static bool IsDataNull(object data)
		{
			if (data == null)
				return true;

			return false;
		}

		public static bool IsDataContentValid(object data , Type dataType)
		{
			if (data.GetType().Equals(dataType))
				return true;

			return false;
		}
	}
}

