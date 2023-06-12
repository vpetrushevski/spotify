using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.Exceptions.Auth
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException()
        {
        }
        public AccessDeniedException(string message) : base(message)
        {
        }

        public AccessDeniedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
