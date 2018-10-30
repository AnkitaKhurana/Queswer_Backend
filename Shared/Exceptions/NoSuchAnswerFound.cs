﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class NoSuchAnswerFound : Exception
    {
        public NoSuchAnswerFound() : base("No such answer exist") { }
    }
}
