using de.softwaremess.loxnet;

namespace de.softwaremess.loxnetTests
{
    public class LoxTests
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
            string[] args = { "", "", "" };
            Assert.Throws<ArgumentException>(() => Lox.Main(args));
        }
    }
}