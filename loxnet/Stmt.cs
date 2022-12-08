﻿using System.Collections.Generic;

namespace de.softwaremess.loxnet
{

  public abstract class Stmt
  {
      public interface IVisitor<R>
      {
          R VisitExpressionStmt(Expression stmt);
          R VisitPrintStmt(Print stmt);
          R VisitVarStmt(Var stmt);
      }
      public class Expression : Stmt
      {
          public Expression(Expr expression)
          {
              this.expression = expression;
          }

          public override R Accept<R>(IVisitor<R> visitor)
          {
              return visitor.VisitExpressionStmt(this);
          }

          public readonly Expr expression;
      }
      public class Print : Stmt
      {
          public Print(Expr expression)
          {
              this.expression = expression;
          }

          public override R Accept<R>(IVisitor<R> visitor)
          {
              return visitor.VisitPrintStmt(this);
          }

          public readonly Expr expression;
      }
      public class Var : Stmt
      {
          public Var(Token name, Expr initializer)
          {
              this.name = name;
              this.initializer = initializer;
          }

          public override R Accept<R>(IVisitor<R> visitor)
          {
              return visitor.VisitVarStmt(this);
          }

          public readonly Token name;
          public readonly Expr initializer;
      }

      public abstract R Accept<R>(IVisitor<R> visitor);

  }
}
