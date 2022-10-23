using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.softwaremess.loxnet
{
    public class Lox
    {
        public static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: Lox [script]");
                throw new ArgumentException("Needs one argument [script]");
            }
            else if (args.Length == 1)
            {
                throw new NotImplementedException("");
            }
            else
            {
                throw new NotImplementedException("");
            }

        }
    }
}
