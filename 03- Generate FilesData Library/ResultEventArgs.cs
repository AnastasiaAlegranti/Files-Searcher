using System;

namespace Anastasia
{
	public class ResultEventArgs:EventArgs
	{
		public readonly int StringToSearchId;
		public readonly string SearchResult;

		public ResultEventArgs(int stringToSearchId, string searchResult)
		{
			StringToSearchId = stringToSearchId;
			SearchResult = searchResult;
		}
	}
}
