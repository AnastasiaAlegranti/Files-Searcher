using System;

namespace Anastasia
{
	class SearchResultsTreat
	{
		public string Directory;
		public string TextToSearch;

		public SearchResultsTreat(string textToSearch)//Use this ctor if user did not select directory
		{
			TextToSearch = textToSearch;
		}

		public SearchResultsTreat(string directory, string textToSearch)//Use this ctor if user select directory
		{
			Directory = @directory;
			TextToSearch = textToSearch;
		}

		public void StartToSearch()
		{
			FilesDataLogic filesDataLogic = new FilesDataLogic();//Make new FilesDataLogic object
			int searchID = filesDataLogic.AddSearch(TextToSearch, Directory);// Add search to DB and get searchID from DB.
			FilesSearcher f = new FilesSearcher(searchID);//Start searching files.			
			f.FoundFile += SaveInDataBase;//If file found event occure, save details in DB.
			f.FoundFile += DisplayFoundFileMessage;//If file found event occure, display details message.
			f.CatchException += DisplayExceptionMessage;//If exception event occured, display details  message.			

			if (Directory == null)//If directory not selected, search the entire computer
			{
				f.SearchFilesInAllComp(TextToSearch);
			}
			else //If directory selected, search files in selected directory
			{
				f.SearchFilesInDirectory(Directory, TextToSearch);
			}
		}

		public void SaveInDataBase(object sender, ResultEventArgs e)//Save result in DB.
		{
			FilesDataLogic filesDataLogic = new FilesDataLogic();
			filesDataLogic.AddResult(e.StringToSearchId, e.SearchResult);
		}

		public void DisplayFoundFileMessage(object sender, ResultEventArgs e)//Display result on console.
		{
			Console.WriteLine(e.SearchResult);
		}

		public void DisplayExceptionMessage(object sender, ExceptionEventArgs e)// Display exception message.
		{
			Console.ForegroundColor = e.Color;//Color exception message in console.
			Console.WriteLine(e.Exception);
			Console.ResetColor();
		}
	}
}
