using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Common
{
    public class TLException : Exception
    {
        public TLException()
        {
        }

        public TLException(string message)
            : base(message)
        {
        }

        public TLException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
