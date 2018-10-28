using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class NoSuchQuestionFound : Exception
    {
        public NoSuchQuestionFound() : base(String.Format("No such Question found"))
        {

        }
    }
}