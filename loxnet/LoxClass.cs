using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace de.softwaremess.loxnet
{
    public class LoxClass : ILoxCallable
    {
        public readonly string name;

        public int Arity { get { return 0; } }

        public LoxClass(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }

        public Object Call(Interpreter interpreter,
                           List<Object> arguments)
        {
            LoxInstance instance = new LoxInstance(this);
            return instance;
        }

    }
}
