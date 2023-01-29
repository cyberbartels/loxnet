using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace de.softwaremess.loxnet
{
    public class LoxClass
    {
        readonly string name;

        public LoxClass(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
