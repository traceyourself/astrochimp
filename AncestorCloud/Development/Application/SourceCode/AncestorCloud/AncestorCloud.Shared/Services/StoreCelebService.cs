using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Cirrious.CrossCore;

namespace AncestorCloud.Shared
{
	public class StoreCelebService : IStoreCelebService
	{

		private IDatabaseService _databaseService;

		public StoreCelebService ()
		{
			_databaseService = Mvx.Resolve<IDatabaseService> ();
		}

		#region IStoreCelebService implementation

		public void StoreCelebData (string dataString)
		{
			if (_databaseService.IsCelebStored())
				return;

			List<Celebrity> celebsList = GetCelebData (dataString);

			_databaseService.StoreCelebrities (celebsList);

			//_databaseService.GetCelebritiesData ();
		}

		private List<Celebrity> GetCelebData(string dataString)
		{
			List<Celebrity> celebList = JsonConvert.DeserializeObject<List<Celebrity>> (dataString);

			return celebList;
		}

		#endregion
	}
}

