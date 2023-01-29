﻿using de.softwaremess.loxnet;

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
            Interpreter interpreter = new Interpreter();

            Scanner scanner = new Scanner("nil;");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Resolver resolver = new Resolver(interpreter);
            resolver.Resolve(stmts);

            interpreter.Interpret(stmts);
        }

        [Test]
        public void CanInterpretSimpleExpression()
        {

            Interpreter interpreter = new Interpreter();

            Scanner scanner = new Scanner("(3+4)==7;");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Resolver resolver = new Resolver(interpreter);
            resolver.Resolve(stmts);

            interpreter.Interpret(stmts);
        }

        [Test]
        public void CanInterpretPrint()
        {
            Interpreter interpreter = new Interpreter();

            Scanner scanner = new Scanner("print \"test\"; ");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Resolver resolver = new Resolver(interpreter);
            resolver.Resolve(stmts);

            interpreter.Interpret(stmts);
        }

        [Test]
        public void CanInterpretAssignment()
        {
            Interpreter interpreter = new Interpreter();

            Scanner scanner = new Scanner("var a = 0; print a; ");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Resolver resolver = new Resolver(interpreter);
            resolver.Resolve(stmts);

            interpreter.Interpret(stmts);
        }

        [Test]
        public void CanInterpretWhileLoop()
        {
            Interpreter interpreter = new Interpreter();

            Scanner scanner = new Scanner("var a = 0;\r\nwhile(a < 100 ) {\r\n  print a;\r\n  a = a + 1;\r\n}");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Resolver resolver = new Resolver(interpreter);
            resolver.Resolve(stmts);

            interpreter.Interpret(stmts);
        }

        [Test]
        public void CanInterpretFibonacciProgram()
        {
            Interpreter interpreter = new Interpreter();

            Scanner scanner = new Scanner("var a = 0; var temp; for (var b = 1; a < 10000; b = temp + b) {  print a;  temp = a;  a = b; }");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Resolver resolver = new Resolver(interpreter);
            resolver.Resolve(stmts);

            interpreter.Interpret(stmts);
        }

        [Test]
        public void CanInterpretClassDeclaration()
        {
            Interpreter interpreter = new Interpreter();

            Scanner scanner = new Scanner("class SimpleClass\r\n{\r\nsomeMethod() {\r\n  return \"Method invoked\";\r\n }\r\n } \r\n print SimpleClass;");
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> stmts = parser.Parse();

            Resolver resolver = new Resolver(interpreter);
            resolver.Resolve(stmts);

            interpreter.Interpret(stmts);
        }
    }
}
