﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace de.softwaremess.loxnet
{
    public class LoxFunction : ILoxCallable
    {
        private readonly Stmt.Function declaration;

        public int Arity { get { return declaration.parameters.Count; } }
        public LoxFunction(Stmt.Function declaration)
        {
            this.declaration = declaration;
        }

        public object Call(Interpreter interpreter, List<object> arguments)
        {
            VarEnvironment environment = new VarEnvironment(interpreter.globals);
            for (int i = 0; i < declaration.parameters.Count; i++) {
                environment.Define(declaration.parameters[i].lexeme,
                    arguments[i]);
            }

            interpreter.ExecuteBlock(declaration.body, environment);
            return null;
        }

        public string ToString()
        {
            return "<fn " + declaration.name.lexeme + ">";
        }
    }
}
