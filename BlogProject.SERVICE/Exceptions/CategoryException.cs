using BlogProject.SERVICE.Utilities.ILogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Exceptions
{
    public class CategoryException : Exception
    {
        private readonly ILogging _logging;

        public CategoryException(string message, ILogging logging) : base(message)
        {
            logging.LogError(message);
        }
    }
}
