using de.softwaremess.loxnet;

namespace de.softwaremess.loxnetTests
{
    public class InterpreterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanCreateInterpreter()
        { 
            Interpreter interpreter = new Interpreter();
        }

        [Test]
        public void CanInterpretNil()
        {
            Scanner scanner = new Scanner("nil;");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Interpreter interpreter = new Interpreter();
            interpreter.Interpret(stmts);
        }

        [Test]
        public void CanInterpretSimpleExpression()
        {
            Scanner scanner = new Scanner("(3+4)==7;");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Interpreter interpreter = new Interpreter();
            interpreter.Interpret(stmts);
        }

        [Test]
        public void CanInterpretPrint()
        {
            Scanner scanner = new Scanner("print \"test\"; ");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Interpreter interpreter = new Interpreter();
            interpreter.Interpret(stmts);
        }

        [Test]
        public void CanInterpretAssignment()
        {
            Scanner scanner = new Scanner("var a = 0; print a; ");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Interpreter interpreter = new Interpreter();
            interpreter.Interpret(stmts);
        }

        [Test]
        public void CanInterpretWhileLoop()
        {
            Scanner scanner = new Scanner("var a = 0;\r\nwhile(a < 100 ) {\r\n  print a;\r\n  a = a + 1;\r\n}");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Interpreter interpreter = new Interpreter();
            interpreter.Interpret(stmts);
        }

        [Test]
        public void CanInterpretFibonacciProgram()
        {
            Scanner scanner = new Scanner("var a = 0;\r\nvar temp;\r\n\r\nfor (var b = 1; a < 10000; b = temp + b) {\r\n  print a;\r\n  temp = a;\r\n  a = b;\r\n}");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Interpreter interpreter = new Interpreter();
            interpreter.Interpret(stmts);
        }

    }
}
