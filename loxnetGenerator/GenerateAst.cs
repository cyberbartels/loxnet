using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.softwaremess.loxnet.tool
{
    public class GenerateAst
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: generate_ast <output directory>");
                Environment.Exit(64);
            }
            string outputDir = args[0];
            string[] expressionTypes = { "Assign   : Token name, Expr value",
                                         "Binary   : Expr left, Token op, Expr right",
                                         "Call     : Expr callee, Token paren, List<Expr> arguments",
                                         "Get      : Expr expression, Token name",
                                         "Grouping : Expr expression",
                                         "Literal  : Object value",
                                         "Logical  : Expr left, Token op, Expr right",
                                         "Set      : Expr obj, Token name, Expr value",
                                         "Unary    : Token op, Expr right",
                                         "Variable : Token name"};
            DefineAst(outputDir, "Expr", new List<string>(expressionTypes));

            string[] statementTypes = { "Block      : List<Stmt> statements",
                                        "Class      : Token name, List<Stmt.Function> methods",
                                        "Expression : Expr expression",
                                        "Function   : Token name, List<Token> parameters," +
                                                    " List<Stmt> body",
                                        "If         : Expr condition, Stmt thenBranch," +
                                                    " Stmt elseBranch",
                                        "Print      : Expr expression",
                                        "Return     : Token keyword, Expr value",
                                        "Var        : Token name, Expr initializer",
                                        "While      : Expr condition, Stmt body"};
            DefineAst(outputDir, "Stmt", new List<string>(statementTypes));
        }

        private static void DefineAst(String outputDir, String baseName, List<String> types)
        {
            string path = outputDir + "/" + baseName + ".cs";
            StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);

            writer.WriteLine("using System.Collections.Generic;");
            writer.WriteLine();
            writer.WriteLine("namespace de.softwaremess.loxnet");
            writer.WriteLine("{");
            writer.WriteLine();
            writer.WriteLine("  public abstract class " + baseName);
            writer.WriteLine("  {");

            DefineVisitor(writer, baseName, types);

            // The AST classes.
            foreach (string type in types)
            {
                string className = type.Split(":")[0].Trim();
                string fields = type.Split(":")[1].Trim();
                DefineType(writer, baseName, className, fields);
            }

            // The base accept() method.
            writer.WriteLine();
            writer.WriteLine("      public abstract R Accept<R>(IVisitor<R> visitor);");

            writer.WriteLine();
            writer.WriteLine("  }");

            writer.WriteLine("}");
            writer.Close();
        }

        private static void DefineVisitor(StreamWriter writer, string baseName, List<String> types)
        {
            writer.WriteLine("      public interface IVisitor<R>");
            writer.WriteLine("      {");

            foreach (string type in types)
            {
                string typeName = type.Split(":")[0].Trim();
                writer.WriteLine("          R Visit" + typeName + baseName + "(" +
                    typeName + " " + baseName.ToLower() + ");");
            }

            writer.WriteLine("      }");
        }

        private static void DefineType(StreamWriter writer, string baseName, string className, string fieldList)
        {
            writer.WriteLine("      public class " + className + " : " + baseName);
            writer.WriteLine("      {");

            // Constructor.
            writer.WriteLine("          public " + className + "(" + fieldList + ")"); 
            writer.WriteLine("          {");

            // Store parameters in fields.
            string[] fields = fieldList.Split(", ");
            foreach (string field in fields)
            {
                string name = field.Split(" ")[1];
                writer.WriteLine("              this." + name + " = " + name + ";");
            }

            writer.WriteLine("          }");

            // Visitor pattern.
            writer.WriteLine();
            writer.WriteLine("          public override R Accept<R>(IVisitor<R> visitor)");
            writer.WriteLine("          {");
            writer.WriteLine("              return visitor.Visit" + className + baseName + "(this);");
            writer.WriteLine("          }");

            // Fields.
            writer.WriteLine();
            foreach (string field in fields)
            {
                writer.WriteLine("          public readonly " + field + ";");
            }

            writer.WriteLine("      }");
        }
    }
}
