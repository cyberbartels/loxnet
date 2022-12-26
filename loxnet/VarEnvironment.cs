using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.softwaremess.loxnet
{
    public class VarEnvironment
    {
        private readonly VarEnvironment enclosing;
        private readonly Dictionary<string, object> values = new Dictionary<string, object>();

        public VarEnvironment()
        {
            enclosing = null;
        }

        public VarEnvironment(VarEnvironment enclosing)
        {
            this.enclosing = enclosing;
        }

        public void Define(string name, object value)
        {
            values[name] = value;
        }

        private VarEnvironment Ancestor(int distance)
        {
            VarEnvironment environment = this;
            for (int i = 0; i < distance; i++)
            {
                environment = environment.enclosing;
            }

            return environment;
        }

        public object GetAt(int distance, String name)
        {
            return Ancestor(distance).values[name];
        }

        public void AssignAt(int distance, Token name, Object value)
        {
            Ancestor(distance).values[name.lexeme] = value;
        }

        public object Get(Token name)
        {
            if (values.ContainsKey(name.lexeme))
            {
                return values[name.lexeme];
            }

            if (enclosing != null) return enclosing.Get(name);

            throw new RuntimeError(name, "Undefined variable '" + name.lexeme + "'.");
        }

        public void Assign(Token name, object value)
        {
            if (values.ContainsKey(name.lexeme))
            {
                values[name.lexeme] = value;
                return;
            }

            if (enclosing != null)
            {
                enclosing.Assign(name, value);
                return;
            }

            throw new RuntimeError(name, "Undefined variable '" + name.lexeme + "'.");
        }
    }
}
