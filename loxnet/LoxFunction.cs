using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace de.softwaremess.loxnet
{
    public class LoxFunction : ILoxCallable
    {
        private readonly Stmt.Function declaration;
        private readonly VarEnvironment closure;

        public int Arity { get { return declaration.parameters.Count; } }
        private readonly bool isInitializer;

        public LoxFunction(Stmt.Function declaration, VarEnvironment closure, bool isInitializer)
        {
            this.isInitializer = isInitializer;
            this.closure = closure;
            this.declaration = declaration;
        }

        public LoxFunction Bind(LoxInstance instance)
        {
            VarEnvironment environment = new VarEnvironment(closure);
            environment.Define("this", instance);
            return new LoxFunction(declaration, environment, isInitializer);
        }

        public object Call(Interpreter interpreter, List<object> arguments)
        {
            VarEnvironment environment = new VarEnvironment(closure);
            for (int i = 0; i < declaration.parameters.Count; i++)
            {
                environment.Define(declaration.parameters[i].lexeme,
                    arguments[i]);
            }

            try
            {
                interpreter.ExecuteBlock(declaration.body, environment);
            }
            catch (Return returnValue)
            {
                if (isInitializer) return closure.GetAt(0, "this");
                return returnValue.value;
            }
            if (isInitializer) return closure.GetAt(0, "this");
            return null;
        }

        public string ToString()
        {
            return "<fn " + declaration.name.lexeme + ">";
        }
    }
}
