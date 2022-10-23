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
        public void CanInvokeMainWithEmptyArgumentList()
        {
            string[] args = { };
            Assert.Throws<NotImplementedException>(() => Lox.Main(args));
        }

        [Test]
        public void CannotInvokeMainWithToManyArguments()
        {
            string[] args = {"", "", ""};
            Assert.Throws<ArgumentException>(() => Lox.Main(args));
        }
    }
}