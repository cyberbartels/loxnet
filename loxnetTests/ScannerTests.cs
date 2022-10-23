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
    }
}