using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class UserAlreadyExists : Exception
    {
        public UserAlreadyExists()
        : base(String.Format("UserName Already Exists"))
        {

        }
    }

}
