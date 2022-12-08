﻿using System.Collections.Generic;

namespace de.softwaremess.loxnet
{

  public abstract class Expr
  {
      public interface IVisitor<R>
      {
          R VisitBinaryExpr(Binary expr);
          R VisitGroupingExpr(Grouping expr);
          R VisitLiteralExpr(Literal expr);
          R VisitUnaryExpr(Unary expr);
          R VisitVariableExpr(Variable expr);
      }
      public class Binary : Expr
      {
          public Binary(Expr left, Token op, Expr right)
          {
              this.left = left;
              this.op = op;
              this.right = right;
          }

          public override R Accept<R>(IVisitor<R> visitor)
          {
              return visitor.VisitBinaryExpr(this);
          }

          public readonly Expr left;
          public readonly Token op;
          public readonly Expr right;
      }
      public class Grouping : Expr
      {
          public Grouping(Expr expression)
          {
              this.expression = expression;
          }

          public override R Accept<R>(IVisitor<R> visitor)
          {
              return visitor.VisitGroupingExpr(this);
          }

          public readonly Expr expression;
      }
      public class Literal : Expr
      {
          public Literal(Object value)
          {
              this.value = value;
          }

          public override R Accept<R>(IVisitor<R> visitor)
          {
              return visitor.VisitLiteralExpr(this);
          }

          public readonly Object value;
      }
      public class Unary : Expr
      {
          public Unary(Token op, Expr right)
          {
              this.op = op;
              this.right = right;
          }

          public override R Accept<R>(IVisitor<R> visitor)
          {
              return visitor.VisitUnaryExpr(this);
          }

          public readonly Token op;
          public readonly Expr right;
      }
      public class Variable : Expr
      {
          public Variable(Token name)
          {
              this.name = name;
          }

          public override R Accept<R>(IVisitor<R> visitor)
          {
              return visitor.VisitVariableExpr(this);
          }

          public readonly Token name;
      }

      public abstract R Accept<R>(IVisitor<R> visitor);

  }
}
