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

        [Test]
        public void CanParseUnaryExpression()
        {
            Scanner scanner = new Scanner("-3;\n");
            Parser parser = new Parser(scanner.scanTokens());
            Expr expr = parser.Parse();

            Assert.That(expr, Is.InstanceOf(typeof(Expr.Unary)));
            Assert.That(((Expr.Unary)expr).right, Is.InstanceOf(typeof(Expr.Literal)));
            Assert.That(((Expr.Literal)((Expr.Unary)expr).right).value, Is.EqualTo(3));
        }

        [Test]
        public void CanParseBinaryExpression()
        {
            Scanner scanner = new Scanner("5+4;\n");
            Parser parser = new Parser(scanner.scanTokens());
            Expr expr = parser.Parse();

            Assert.That(expr, Is.InstanceOf(typeof(Expr.Binary)));
            Assert.That(((Expr.Binary)expr).right, Is.InstanceOf(typeof(Expr.Literal)));
            Assert.That(((Expr.Binary)expr).left, Is.InstanceOf(typeof(Expr.Literal)));
            Assert.That(((Expr.Literal)((Expr.Binary)expr).right).value, Is.EqualTo(4));
        }

        [Test]
        public void CanParseGroupingExpression()
        {
            Scanner scanner = new Scanner("(4==5)==true;\n");
            Parser parser = new Parser(scanner.scanTokens());
            Expr expr = parser.Parse();

            Assert.That(expr, Is.InstanceOf(typeof(Expr.Binary)));
            Assert.That(((Expr.Binary)expr).left, Is.InstanceOf(typeof(Expr.Grouping)));
            Assert.That(((Expr.Binary)expr).op, Is.InstanceOf(typeof(Token)));
            Assert.That(((Expr.Binary)expr).right, Is.InstanceOf(typeof(Expr.Literal)));

            Assert.That(((Expr.Literal)((Expr.Binary)expr).right).value, Is.EqualTo(true));

        }
    }
}
