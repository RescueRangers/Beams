using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beams.Core.Util
{
	internal class FileSaveException : Exception
	{
		public FileSaveException(string? message) : base(message)
		{
		}
	}
}
