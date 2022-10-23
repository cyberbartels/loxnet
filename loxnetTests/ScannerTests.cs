using de.softwaremess.loxnet;

namespace loxnetTests
{
    public class Tests
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
            Scanner scanner = new Scanner("var i = 7");
        }
    }
}