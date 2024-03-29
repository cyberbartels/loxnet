﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.softwaremess.loxnet
{
    public interface ILoxCallable
    {
        public int Arity { get; }
        object Call(Interpreter interpreter, List<object> arguments);
    }
}
