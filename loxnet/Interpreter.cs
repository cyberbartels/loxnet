using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.softwaremess.loxnet
{
    public class Interpreter : Expr.IVisitor<object>, Stmt.IVisitor<object>
    {
        private VarEnvironment environment = new VarEnvironment();

        public void Interpret(List<Stmt> statements)
        {
            try
            {
                foreach (Stmt statement in statements)
                {
                    Execute(statement);
                }
            }
            catch (RuntimeError error)
            {
                Lox.RuntimeError(error);
            }
        }

        public object VisitLiteralExpr(Expr.Literal expr)
        {
            return expr.value;
        }

        public object VisitUnaryExpr(Expr.Unary expr)
        {
            object right = Evaluate(expr.right);

            switch (expr.op.type) {
                case TokenType.BANG:
                    return !IsTruthy(right);
                case TokenType.MINUS:
                    CheckNumberOperand(expr.op, right);
                    return -(double)right;
            }

            // Unreachable.
            return null;
        }

        public object VisitGroupingExpr(Expr.Grouping expr)
        {
            return Evaluate(expr.expression);
        }

        public object VisitBinaryExpr(Expr.Binary expr)
        {
            object left = Evaluate(expr.left);
            object right = Evaluate(expr.right);

            switch (expr.op.type)
            {
                case TokenType.GREATER:
                    CheckNumberOperands(expr.op, left, right);
                    return (double)left > (double)right;
                case TokenType.GREATER_EQUAL:
                    CheckNumberOperands(expr.op, left, right);
                    return (double)left >= (double)right;
                case TokenType.LESS:
                    CheckNumberOperands(expr.op, left, right);
                    return (double)left < (double)right;
                case TokenType.LESS_EQUAL:
                    CheckNumberOperands(expr.op, left, right);
                    return (double)left <= (double)right;
                case TokenType.MINUS:
                    CheckNumberOperands(expr.op, left, right);
                    return (double)left - (double)right;
                case TokenType.PLUS:
                    if (typeof(Double).IsInstanceOfType(left) && typeof(Double).IsInstanceOfType(right))
                    {
                        return (double)left + (double)right;
                    }

                    if (typeof(string).IsInstanceOfType(left) && typeof(string).IsInstanceOfType(right))
                    {
                        return (string)left + (string)right;
                    }
                    throw new RuntimeError(expr.op, "Operands must be two numbers or two strings.");
                case TokenType.SLASH:
                    CheckNumberOperands(expr.op, left, right);
                    return (double)left / (double)right;
                case TokenType.STAR:
                    CheckNumberOperands(expr.op, left, right);
                    return (double)left * (double)right;
                case TokenType.BANG_EQUAL: return !IsEqual(left, right);
                case TokenType.EQUAL_EQUAL: return IsEqual(left, right);
            }

            // Unreachable.
            return null;
        }

        public object VisitVariableExpr(Expr.Variable expr)
        {
            return environment.Get(expr.name);
        }

        public object VisitVarStmt(Stmt.Var stmt)
        {
            object value = null;
            if (stmt.initializer != null)
            {
                value = Evaluate(stmt.initializer);
            }

            environment.Define(stmt.name.lexeme, value);
            return null;
        }

        public object VisitExpressionStmt(Stmt.Expression stmt)
        {
            Evaluate(stmt.expression);
            return null;
            
        }

        public object VisitPrintStmt(Stmt.Print stmt)
        {
            object value = Evaluate(stmt.expression);
            Console.WriteLine(stringify(value));
            return null;
        }

        private object Evaluate(Expr expr)
        {
            return expr.Accept(this);
        }

        private void Execute(Stmt stmt)
        {
            stmt.Accept(this);
        }

        private void CheckNumberOperand(Token op, object operand)
        {
            if (typeof(Double).IsInstanceOfType(operand)) return;
            throw new RuntimeError(op, "Operand must be a number.");
        }

        private void CheckNumberOperands(Token op, Object left, Object right)
        {
            if (typeof(Double).IsInstanceOfType(left) && typeof(Double).IsInstanceOfType(right)) return;

            throw new RuntimeError(op, "Operands must be numbers.");
        }

        private bool IsTruthy(object obj)
        {
            if (obj == null) return false;
            if (typeof(bool).IsInstanceOfType(obj)) return (bool)obj;
            return true;
        }

        private bool IsEqual(object a, object b)
        {
            if (a == null && b == null) return true;
            if (a == null) return false;

            return a.Equals(b);
        }

        private string stringify(object obj)
        {
            if (obj == null) return "nil";

            if (typeof(bool).IsInstanceOfType(obj)) {
                string text = obj.ToString();
                if (text.EndsWith(".0"))
                {
                    text = text.Substring(0, text.Length - 2);
                }
                return text;
            }

            return obj.ToString();
        }
    }
}
