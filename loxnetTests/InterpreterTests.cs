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

    }
}
