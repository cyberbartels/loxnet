using System.Collections.Generic;

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
      }
      public class Binary : Expr
      {
          Binary(Expr left, Token op, Expr right)
          {
              this.left = left;
              this.op = op;
              this.right = right;
          }

          public override R Accept<R>(IVisitor<R> visitor)
          {
              return visitor.VisitBinaryExpr(this);
          }

          readonly Expr left;
          readonly Token op;
          readonly Expr right;
      }
      public class Grouping : Expr
      {
          Grouping(Expr expression)
          {
              this.expression = expression;
          }

          public override R Accept<R>(IVisitor<R> visitor)
          {
              return visitor.VisitGroupingExpr(this);
          }

          readonly Expr expression;
      }
      public class Literal : Expr
      {
          Literal(Object value)
          {
              this.value = value;
          }

          public override R Accept<R>(IVisitor<R> visitor)
          {
              return visitor.VisitLiteralExpr(this);
          }

          readonly Object value;
      }
      public class Unary : Expr
      {
          Unary(Token op, Expr right)
          {
              this.op = op;
              this.right = right;
          }

          public override R Accept<R>(IVisitor<R> visitor)
          {
              return visitor.VisitUnaryExpr(this);
          }

          readonly Token op;
          readonly Expr right;
      }

      public abstract R Accept<R>(IVisitor<R> visitor);

  }
}
