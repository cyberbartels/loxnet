using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.softwaremess.loxnet
{
    public class Token
    {

        readonly TokenType type;
        readonly string lexeme;
        readonly Object literal;
        readonly int line;

        public Token(TokenType type, string lexeme, Object literal, int line)
        {
            this.type = type;
            this.lexeme = lexeme;
            this.literal = literal;
            this.line = line;
        }

        public string toString()
        {
            return type + " " + lexeme + " " + literal;
        }
    }
}
