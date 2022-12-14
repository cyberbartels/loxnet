using de.softwaremess.loxnet;

namespace de.softwaremess.loxnetTests
{
    public class ScannerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanCreateToken()
        {
            Token token = new Token(TokenType.PLUS, "", "", 111);
        }

        [Test]
        public void CanCreateScanner()
        {
            Scanner scanner = new Scanner("");
        }

        [Test]
        public void CanScanSimpleSum()
        {
            Scanner scanner = new Scanner("3+4\n");
            Assert.Contains(new Token(TokenType.PLUS, "+", null, 1), scanner.ScanTokens(), "Expected PLUS Token in line 1");
        }

        [Test]
        public void CanScanEQUAL_EQUAL()
        {
            Scanner scanner = new Scanner("3==4\n");
            Assert.Contains(new Token(TokenType.EQUAL_EQUAL, "==", null, 1), scanner.ScanTokens(), "Expected EQUAL_EQUAL Token in line 1");
        }

        [Test]
        public void CanScanEQUAL()
        {
            Scanner scanner = new Scanner("a=4;\n");
            Assert.Contains(new Token(TokenType.EQUAL, "=", null, 1), scanner.ScanTokens(), "Expected EQUAL Token in line 1");
        }

        [Test]
        public void CanScanParantheses()
        {
            Scanner scanner = new Scanner("a=(3==4);\n");
            var tokens = scanner.ScanTokens();
            Assert.Contains(new Token(TokenType.LEFT_PAREN, "(", null, 1), tokens, "Expected LEFT_PAREN Token in line 1");
            Assert.Contains(new Token(TokenType.RIGHT_PAREN, ")", null, 1), tokens, "Expected RIGHT_PAREN Token in line 1");
        }

        [Test]
        public void CanScanMultiline()
        {
            Scanner scanner = new Scanner("a=(3==4);\n a-3!=7;");
            var tokens = scanner.ScanTokens();
            Assert.Contains(new Token(TokenType.LEFT_PAREN, "(", null, 1), tokens, "Expected LEFT_PAREN Token in line 1");
            Assert.Contains(new Token(TokenType.RIGHT_PAREN, ")", null, 1), tokens, "Expected RIGHT_PAREN Token in line 1");
            Assert.Contains(new Token(TokenType.MINUS, "-", null, 2), tokens, "Expected MINUS Token in line 2");
            Assert.That(tokens, Does.Not.Contain(new Token(TokenType.RIGHT_PAREN, ")", null, 2)));
        }

        [Test]
        public void IgnoresComments()
        {
            Scanner scanner = new Scanner("//Some comment a+1 \n a=(3==4); \n a-3!=7;");
            var tokens = scanner.ScanTokens();
            Assert.That(tokens, Does.Not.Contain(new Token(TokenType.PLUS, "+", null, 1)));
        }

        [Test]
        public void CanScanComplexStuff()
        {
            //See https://craftinginterpreters.com/scanning.html Chapter 4.6

            Scanner scanner = new Scanner("// this is a comment\r\n (( )){} // grouping stuff\r\n !*+-/=<> <= == // operators");
            var tokens = scanner.ScanTokens();
            Assert.That(tokens, Does.Not.Contain(new Token(TokenType.PLUS, "+", null, 1)));
            Assert.Contains(new Token(TokenType.LEFT_PAREN, "(", null, 2), tokens, "Expected LEFT_PAREN Token in line 2");
            Assert.Contains(new Token(TokenType.RIGHT_PAREN, ")", null, 2), tokens, "Expected RIGHT_PAREN Token in line 2");
            Assert.Contains(new Token(TokenType.LEFT_BRACE, "{", null, 2), tokens, "Expected LEFT_BRACE Token in line 2");
            Assert.Contains(new Token(TokenType.RIGHT_BRACE, "}", null, 2), tokens, "Expected RIGHT_BRACE Token in line 2");
            Assert.Contains(new Token(TokenType.MINUS, "-", null, 3), tokens, "Expected MINUS Token in line 3");
            Assert.Contains(new Token(TokenType.BANG, "!", null, 3), tokens, "Expected BANG Token in line 3");
            Assert.Contains(new Token(TokenType.SLASH, "/", null, 3), tokens, "Expected SLASH Token in line 3");
            Assert.Contains(new Token(TokenType.LESS_EQUAL, "<=", null, 3), tokens, "Expected LESS_EQUAL Token in line 3");
            Assert.Contains(new Token(TokenType.GREATER, ">", null, 3), tokens, "Expected GREATER Token in line 3");
            Assert.That(tokens, Does.Not.Contain(new Token(TokenType.BANG_EQUAL, "!=", null, 3)));
            Assert.That(tokens, Does.Not.Contain(new Token(TokenType.GREATER_EQUAL, ">=", null, 3)));
        }

        [Test]
        public void CanScanString()
        {
            string literal = @"This is a string literal";
            string code = @"""" + literal + @"""";
            Scanner scanner = new Scanner(code);
            var tokens = scanner.ScanTokens();
            StringAssert.AreEqualIgnoringCase(literal, (string) tokens[0].literal);
            Assert.Contains(new Token(TokenType.STRING, code, literal, 1), tokens, "Expected STRING Token in line 1");
        }

        [Test]
        public void CanScanStringinExpression()
        {
            string literal = @"This is another string literal";
            string code = @"""" + literal + @"""";
            Scanner scanner = new Scanner(@"//Some comment with a ""double quoted text""" + " \n name = " + @"""" + literal + @"""");
            var tokens = scanner.ScanTokens();
            Assert.Contains(new Token(TokenType.STRING, code, literal, 2), tokens, "Expected STRING Token in line 2");
        }

        [Test]
        public void CanScanNUMBER()
        {
            Scanner scanner = new Scanner("3==4\n");
            Assert.Contains(new Token(TokenType.NUMBER, "3", null, 1), scanner.ScanTokens(), "Expected NUMBER 3 Token in line 1");
            Assert.Contains(new Token(TokenType.NUMBER, "4", null, 1), scanner.ScanTokens(), "Expected NUMBER 4 Token in line 1");
        }

        [Test]
        public void CanScanFractionalNumber ()
        {
            Scanner scanner = new Scanner("3.1==4\n");
            Assert.Contains(new Token(TokenType.NUMBER, "3.1", null, 1), scanner.ScanTokens(), "Expected NUMBER 3.1 Token in line 1");
            Assert.Contains(new Token(TokenType.NUMBER, "4", null, 1), scanner.ScanTokens(), "Expected NUMBER 4 Token in line 1");
        }

        [Test]
        public void CanScanIdentifier()
        {
            Scanner scanner = new Scanner("myVar=3.1\n");
            Assert.Contains(new Token(TokenType.IDENTIFIER, "myVar", null, 1), scanner.ScanTokens(), "Expected IDENTIFIER Token in line 1");
        }

        [Test]
        public void CanScanKeywords()
        {
            Scanner scanner = new Scanner("else if class\n");
            Assert.Contains(new Token(TokenType.ELSE, "else", null, 1), scanner.ScanTokens(), "Expected ELSE Token in line 1");
            Assert.Contains(new Token(TokenType.IF, "if", null, 1), scanner.ScanTokens(), "Expected IF Token in line 1");
            Assert.Contains(new Token(TokenType.CLASS, "class", null, 1), scanner.ScanTokens(), "Expected CLASS Token in line 1");
        }
    }
}