using System;

namespace Anastasia
{
	public class ExceptionEventArgs : EventArgs
	{
		public readonly string Exception;
		public readonly ConsoleColor Color;

		public ExceptionEventArgs(string exception, ConsoleColor color)
		{
			Exception = exception;
			Color = color;
		}
	}
}
