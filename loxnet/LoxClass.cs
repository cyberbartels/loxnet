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
        private readonly Dictionary<string, LoxFunction> methods;

        public int Arity { get { return 0; } }

        public LoxClass(string name, Dictionary<string, LoxFunction> methods)
        {
            this.name = name;
            this.methods = methods;
        }

        public LoxFunction FindMethod(String name)
        {
            if (methods.ContainsKey(name))
            {
                return methods[name];
            }

            return null;
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
