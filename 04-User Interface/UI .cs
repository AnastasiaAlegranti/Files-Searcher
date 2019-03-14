using System;

namespace Anastasia
{
	class UI
	{
		public void Start()//Start UI
		{
			try
			{
				int mainScreen;
				string fileName;
				string parentDirectory;

				do //Do this function until user press 3
				{
					Console.Clear();
					Console.WriteLine("Press 1, 2 or 3. ");
					Console.WriteLine("1. Enter file name to search");
					Console.WriteLine("2. Enter file name to search + parent directory to search in.");
					Console.WriteLine("3. Exit");
					mainScreen = int.Parse(Console.ReadLine());

					while (mainScreen > 3 || mainScreen < 1)
					{
						Console.WriteLine("Please, enter only 1, 2 or 3!");
						mainScreen = int.Parse(Console.ReadLine());
					}

					if (mainScreen == 1)
					{
						Console.Clear();
						Console.Write("Enter file name to search : ");
						fileName = Console.ReadLine();
						SearchResultsTreat searchResultTreat = new SearchResultsTreat(fileName);
						Console.WriteLine("Start Searching...");
						searchResultTreat.StartToSearch();
					}
					if (mainScreen == 2)
					{
						Console.Clear();
						Console.Write("Enter file name to search : ");
						fileName = Console.ReadLine();
						Console.Write("Enter root directory to search : ");
						parentDirectory = Console.ReadLine();
						SearchResultsTreat searchResultTreat = new SearchResultsTreat(parentDirectory, fileName);
						Console.WriteLine("Start Searching...");
						searchResultTreat.StartToSearch();
					}
					Console.WriteLine("End of search. Press any key to continue");
					Console.ReadLine();
				}
				while (mainScreen != 3);//If user press 3, exit from console, if not = start UI again.
			}
			catch (Exception ex)
			{
				if (ex.GetType() == typeof(FormatException))
				{
					Console.WriteLine("You must enter a numeric value.");
				}
				else if (ex.GetType() == typeof(OverflowException))
				{
					Console.WriteLine("Enter Only Sane value.");
				}
				else
				{
					Console.WriteLine("Error." + ex.Message);
				}
				Console.WriteLine("Enter any key to continue");
				Console.ReadLine();
				Start();//Start again UI.
			}
		}
	}
}
