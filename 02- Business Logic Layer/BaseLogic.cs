using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anastasia
{
	public class BaseLogic : IDisposable
	{
		protected FilesDataEntities DB = new FilesDataEntities();
		public void Dispose()
		{
			DB.Dispose();
		}
	}
}
