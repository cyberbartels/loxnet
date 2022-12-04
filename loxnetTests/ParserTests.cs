using de.softwaremess.loxnet;

namespace de.softwaremess.loxnetTests
{
    public class ParserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanCreateParserWithTokenList()
        {
            Scanner scanner = new Scanner("a=(3==4);\n");
            Parser parser = new Parser(scanner.scanTokens());
        }
    }
}
