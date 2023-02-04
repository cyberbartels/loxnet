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
        private readonly LoxClass superclass;
        private readonly Dictionary<string, LoxFunction> methods;

        public int Arity { 
            get 
            {
                LoxFunction initializer = FindMethod("init");
                if (initializer == null) return 0;
                return initializer.Arity;
            } 
        }

        public LoxClass(string name, LoxClass superclass, Dictionary<string, LoxFunction> methods)
        {
            this.superclass = superclass;
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
            LoxFunction initializer = FindMethod("init");
            if (initializer != null)
            {
                initializer.Bind(instance).Call(interpreter, arguments);
            }
            return instance;
        }

    }
}
