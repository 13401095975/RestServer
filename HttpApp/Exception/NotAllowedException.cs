using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.exception
{
    [Serializable]
    public class NotAllowedException : ApplicationException
    {
        public NotAllowedException() { }
        public NotAllowedException(string message)
            : base(message) { }
        public NotAllowedException(string message, Exception inner)
            : base(message, inner) { }
    }

}
