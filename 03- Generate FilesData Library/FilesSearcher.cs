using System;
using System.IO;

namespace Anastasia
{
	public class FilesSearcher
	{
		public event ResultHandler FoundFile;
		public event ExceptionHandler CatchException;
		public int SearchId;

		public FilesSearcher(int searchId)//Search ID from DB.
		{
			SearchId = searchId;
		}

		public void SearchFilesInDirectory(string directory, string textToSearch) //Search files in directory.
		{
			try
			{
				string[] files = Directory.GetFiles(directory);//Get all files in directory
				foreach (string item in files)
				{
					string itemToLowerCase = item.ToLower();//Make file name in lowercase letters
					if (itemToLowerCase.Contains(textToSearch.ToLower()))//Check if file name contains searching text in lowercase letters
					{						
						FoundFile?.Invoke(this, new ResultEventArgs(SearchId, item));//If matching file found, invoke event and pass him search ID from DB, and matching file name.
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("There is no required permission to: " + directory, ConsoleColor.Red));
			}
			catch (DirectoryNotFoundException)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("Path: " + directory + " is not found or is invalid", ConsoleColor.DarkMagenta));
			}
			catch (PathTooLongException)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("Error:The specified path, file name, or both exceed the system-defined maximum length.", ConsoleColor.DarkRed));
			}
			catch (ArgumentException)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("Error:path is a zero-length string, contains only white space, or contains one or more invalid characters.", ConsoleColor.DarkCyan));
			}
			catch (IOException)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("Error. Path: " + directory + " is a file name or a network error has occurred", ConsoleColor.Green));
			}
			catch (Exception ex)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("Error: " + ex.Message, ConsoleColor.DarkBlue));
			}

			try
			{
				string[] dirs = Directory.GetDirectories(directory);//Get all directories in selected directory
				foreach (string item in dirs)
					SearchFilesInDirectory(item, textToSearch);//Recursion, search files in every directory in directory.
			}
			catch (UnauthorizedAccessException)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("There is no required permission to: " + directory, ConsoleColor.Red));
			}
			catch (DirectoryNotFoundException)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("Path: " + directory + " is not found or is invalid", ConsoleColor.DarkMagenta));
			}
			catch (ArgumentException)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("Error:path is a zero-length string, contains only white space, or contains one or more invalid characters.", ConsoleColor.DarkCyan));
			}
			catch (PathTooLongException)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("Error:The specified path, file name, or both exceed the system-defined maximum length.", ConsoleColor.DarkRed));
			}
			catch (IOException)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("Error. Path: " + directory + " is a file name", ConsoleColor.Green));
			}
			catch (Exception ex)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("Error: " + ex.Message, ConsoleColor.Blue));
			}
		}

		public void SearchFilesInAllComp(string textToSearch)//If directory not selected.
		{
			try
			{
				string[] drivers = Environment.GetLogicalDrives();//Get all drives.
				foreach (string item in drivers)//Search in every drive
				{
					SearchFilesInDirectory(item, textToSearch);//Drive = directory.
				}
			}
			catch (System.Security.SecurityException)		
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("There is no required permission. ", ConsoleColor.Yellow));
			}
			catch (Exception ex)
			{
				CatchException?.Invoke(this, new ExceptionEventArgs("Error occurs. Details: " + ex.Message, ConsoleColor.Blue));
			}	
		}
	}
}
