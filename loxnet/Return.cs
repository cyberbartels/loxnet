using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.softwaremess.loxnet
{
    public class Return : Exception
    {
        public readonly object value;

        public Return(object value)
        {
            this.value = value;
        }
    }
}
