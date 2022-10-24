using de.softwaremess.loxnet;

namespace loxnetTests
{
    public class ScannerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanCreateLoxClass()
        {
            Lox lox = new Lox();
        }

        [Test]
        public void CanInvokeMainWithFilePath()
        {
            string[] args = { @"X:\Projekte\VisualStudio\loxnet\loxnetTests\test.lox" };
            Lox.Main(args);
        }

        [Test]
        public void CannotInvokeMainWithToManyArguments()
        {
            string[] args = {"", "", ""};
            Assert.Throws<ArgumentException>(() => Lox.Main(args));
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
            Assert.Contains(new Token(TokenType.PLUS, "+", null, 1), scanner.scanTokens(), "Expected PLUS Token in line 1");
        }

        [Test]
        public void CanScanEQUAL_EQUAL()
        {
            Scanner scanner = new Scanner("3==4\n");
            Assert.Contains(new Token(TokenType.EQUAL_EQUAL, "==", null, 1), scanner.scanTokens(), "Expected EQUAL_EQUAL Token in line 1");
        }

        [Test]
        public void CanScanEQUAL()
        {
            Scanner scanner = new Scanner("a=4;\n");
            Assert.Contains(new Token(TokenType.EQUAL, "=", null, 1), scanner.scanTokens(), "Expected EQUAL Token in line 1");
        }

        [Test]
        public void CanScanParantheses()
        {
            Scanner scanner = new Scanner("a=(3==4);\n");
            var tokens = scanner.scanTokens();
            Assert.Contains(new Token(TokenType.LEFT_PAREN, "(", null, 1), tokens, "Expected LEFT_PAREN Token in line 1");
            Assert.Contains(new Token(TokenType.RIGHT_PAREN, ")", null, 1), tokens, "Expected RIGHT_PAREN Token in line 1");
        }

        [Test]
        public void CanScanMultiline()
        {
            Scanner scanner = new Scanner("a=(3==4);\na-3!=7;");
            var tokens = scanner.scanTokens();
            Assert.Contains(new Token(TokenType.LEFT_PAREN, "(", null, 1), tokens, "Expected LEFT_PAREN Token in line 1");
            Assert.Contains(new Token(TokenType.RIGHT_PAREN, ")", null, 1), tokens, "Expected RIGHT_PAREN Token in line 1");
            Assert.Contains(new Token(TokenType.MINUS, "-", null, 2), tokens, "Expected MINUS Token in line 2");
           // Assert.Contains(new Token(TokenType.BANG_EQUAL, "!=", null, 2), tokens, "Expected BANG_EQUAL Token in line 2");
        }

        [Test]
        public void IgnoresComments()
        {
            Scanner scanner = new Scanner("//Some comment a+1 \n a=(3==4); \n a-3!=7;");
            var tokens = scanner.scanTokens();
            Assert.That(tokens, Does.Not.Contain(new Token(TokenType.PLUS, "+", null, 1)));
        }
    }
}