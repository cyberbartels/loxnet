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

        public LoxInstance(LoxClass klass)
        {
            this.klass = klass;
        }


        public override string ToString()
        {
            return klass.name + " instance";
        }
    }
}
