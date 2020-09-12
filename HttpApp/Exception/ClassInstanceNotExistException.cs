using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.exception
{
    [Serializable]
    public class ClassInstanceNotExistException : ApplicationException
    {
        public ClassInstanceNotExistException() { }
        public ClassInstanceNotExistException(string message)
            : base(message) { }
        public ClassInstanceNotExistException(string message, Exception inner)
            : base(message, inner) { }
    }

}
