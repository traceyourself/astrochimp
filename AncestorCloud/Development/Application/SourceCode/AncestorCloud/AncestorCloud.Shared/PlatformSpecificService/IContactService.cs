using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public interface IContactService
	{
		List<People> GetDeviceContacts();
		Task<List<People>> ReadPhoneContacts ();
	}
}

