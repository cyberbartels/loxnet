﻿using de.softwaremess.loxnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace de.softwaremess.loxnet
{
    public class ASTPrinter : Expr.IVisitor<string>
    {
        public string Print(Expr expr)
        {
            return expr.Accept(this);
        }

        public string VisitBinaryExpr(Expr.Binary expr)
        {
            return Parenthesize(expr.op.lexeme, expr.left, expr.right);
        }

        public string VisitCallExpr(Expr.Call expr)
        {
            return expr.callee + "( " + expr.arguments.ToArray().ToString() + ")";
        }

        public string VisitGetExpr(Expr.Get expr)
        {
            return $"getter {expr.expression}";
        }

        public string VisitSetExpr(Expr.Set expr)
        {
            return $"setter {expr.obj} {expr.value}";
        }

        public string VisitSuperExpr(Expr.Super expr) 
        {
            return $"super {expr.method.toString()}";
        }

        public string VisitThisExpr(Expr.This expr)
        {
            return $"{expr.keyword}";
        }

        public string VisitLogicalExpr(Expr.Logical expr)
        {
            return Parenthesize(expr.op.lexeme, expr.left, expr.right);
        }

        public string VisitGroupingExpr(Expr.Grouping expr)
        {
            return Parenthesize("group", expr.expression);
        }

        public string VisitLiteralExpr(Expr.Literal expr)
        {
            if (expr.value == null) return "nil";
            return expr.value.ToString();
        }

        public string VisitUnaryExpr(Expr.Unary expr)
        { 
            return Parenthesize(expr.op.lexeme, expr.right);
        }

        public string VisitVariableExpr(Expr.Variable expr)
        {
            if (expr.name == null) return "nil";
            return expr.name.ToString();
        }

        public string VisitAssignExpr(Expr.Assign expr)
        {
            if (expr.name == null) return "nil";
            return "Assign " + expr.name.ToString() + " to " + expr.value.ToString();
        }

        private String Parenthesize(string name, params Expr[] exprs)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("(").Append(name);
            foreach (Expr expr in exprs)
            {
                builder.Append(" ");
                builder.Append(expr.Accept(this));
            }
            builder.Append(")");

            return builder.ToString();
        }
    }
}
