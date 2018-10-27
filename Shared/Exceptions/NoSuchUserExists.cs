using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class NoSuchUserExists : Exception
    {
        public NoSuchUserExists() : base(String.Format("No such User exists"))
        {

        }
    }
}
