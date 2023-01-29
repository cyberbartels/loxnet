using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace de.softwaremess.loxnet
{
    public class LoxInstance
    {
        private LoxClass klass;
        private readonly Dictionary<string, object> fields = new Dictionary<string, object>();

        public LoxInstance(LoxClass klass)
        {
            this.klass = klass;
        }

        public object Get(Token name)
        {
            if (fields.ContainsKey(name.lexeme))
            {
                return fields[name.lexeme];
            }

            throw new RuntimeError(name, "Undefined property '" + name.lexeme + "'.");
        }

        public override string ToString()
        {
            return klass.name + " instance";
        }
    }
}
