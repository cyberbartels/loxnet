using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.softwaremess.loxnet.NativeFunction
{
    internal class Clock : ILoxCallable
    {
        public int Arity { get { return 0; } }
        public object Call(Interpreter interpreter, List<object> arguments)
        {
            return (double) DateTime.Now.Ticks / 1000.0;
        }
        public string ToString() { return "<native fn>"; }
    }
}
