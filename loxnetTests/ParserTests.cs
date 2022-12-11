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
            Parser parser = new Parser(scanner.ScanTokens());
        }

        [Test]
        public void CanParseUnaryExpression()
        {
            Scanner scanner = new Scanner("-3;\n");
            Parser parser = new Parser(scanner.ScanTokens());
            List<Stmt> stmts = parser.Parse();

            Assert.That(((Stmt.Expression)stmts[0]).expression, Is.InstanceOf(typeof(Expr.Unary)));
            Assert.That(((Expr.Unary)((Stmt.Expression)stmts[0]).expression).right, Is.InstanceOf(typeof(Expr.Literal)));
            Assert.That(((Expr.Literal)((Expr.Unary)((Stmt.Expression)stmts[0]).expression).right).value, Is.EqualTo(3));
        }

        [Test]
        public void CanParseBinaryExpression()
        {
            Scanner scanner = new Scanner("5+4;\n");
            Parser parser = new Parser(scanner.ScanTokens());
            List<Stmt> stmts = parser.Parse();

            Assert.That(((Stmt.Expression)stmts[0]).expression, Is.InstanceOf(typeof(Expr.Binary)));
            Assert.That(((Expr.Binary)((Stmt.Expression)stmts[0]).expression).right, Is.InstanceOf(typeof(Expr.Literal)));
            Assert.That(((Expr.Binary)((Stmt.Expression)stmts[0]).expression).left, Is.InstanceOf(typeof(Expr.Literal)));
            Assert.That(((Expr.Literal)((Expr.Binary)((Stmt.Expression)stmts[0]).expression).right).value, Is.EqualTo(4));
        }

        [Test]
        public void CanParseGroupingExpression()
        {
            Scanner scanner = new Scanner("(4==5)==true;\n");
            Parser parser = new Parser(scanner.ScanTokens());
            List<Stmt> stmts = parser.Parse();

            Assert.That(((Stmt.Expression)stmts[0]).expression, Is.InstanceOf(typeof(Expr.Binary)));
            Assert.That(((Expr.Binary)((Stmt.Expression)stmts[0]).expression).left, Is.InstanceOf(typeof(Expr.Grouping)));
            Assert.That(((Expr.Binary)((Stmt.Expression)stmts[0]).expression).op, Is.InstanceOf(typeof(Token)));
            Assert.That(((Expr.Binary)((Stmt.Expression)stmts[0]).expression).right, Is.InstanceOf(typeof(Expr.Literal)));

            Assert.That(((Expr.Literal)((Expr.Binary)((Stmt.Expression)stmts[0]).expression).right).value, Is.EqualTo(true));

        }

        [Test]
        public void CanParsePrintStatement()
        {
            Scanner scanner = new Scanner("print (4==5)==true;\n");
            Parser parser = new Parser(scanner.ScanTokens());
            List<Stmt> stmts = parser.Parse();

            Assert.That((stmts[0]), Is.InstanceOf(typeof(Stmt.Print)));
        }

        [Test]
        public void CanParseVarStatement()
        {
            Scanner scanner = new Scanner("var a = 1;\n");
            Parser parser = new Parser(scanner.ScanTokens());
            List<Stmt> stmts = parser.Parse();

            Assert.That((stmts[0]), Is.InstanceOf(typeof(Stmt.Var)));
        }
    }
}
