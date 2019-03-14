using System.Linq;

namespace Anastasia
{
	public class FilesDataLogic : BaseLogic
	{
		public int AddSearch(string stringToSearch, string rootFolder)
		{
			Search search = new Search
			{
				StringToSearch = stringToSearch,
				RootFolder = rootFolder
			};

			DB.Searches.Add(search);
			DB.SaveChanges();
			return DB.Searches.Where(s => s.StringToSearch == stringToSearch).Select(s => s.StringToSearchID).ToList().Last();
		}

		public void AddResult(int stringToSearchID, string searchResult)
		{
			Result result = new Result
			{
				StringToSearchID = stringToSearchID,
				SearchResult = searchResult
			};

			DB.Results.Add(result);
			DB.SaveChanges();
		}		
	}
}
