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
        public LoxFunction(Stmt.Function declaration, VarEnvironment closure)
        {
            this.closure= closure;
            this.declaration = declaration;
        }

        public object Call(Interpreter interpreter, List<object> arguments)
        {
            VarEnvironment environment = new VarEnvironment(closure);
            for (int i = 0; i < declaration.parameters.Count; i++) {
                environment.Define(declaration.parameters[i].lexeme,
                    arguments[i]);
            }

            try
            {
                interpreter.ExecuteBlock(declaration.body, environment);
            }
            catch (Return returnValue)
            {
                return returnValue.value;
            }
            return null;
        }

        public string ToString()
        {
            return "<fn " + declaration.name.lexeme + ">";
        }
    }
}
