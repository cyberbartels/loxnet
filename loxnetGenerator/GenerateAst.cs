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
            string[] expressionTypes = { "Binary   : Expr left, Token op, Expr right",
                                                   "Grouping : Expr expression",
                                                   "Literal  : Object value",
                                                   "Unary    : Token op, Expr right" };
            DefineAst(outputDir, "Expr", new List<string>(expressionTypes));
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
            writer.WriteLine("          " + className + "(" + fieldList + ")"); 
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
                writer.WriteLine("          readonly " + field + ";");
            }

            writer.WriteLine("      }");
        }
    }
}
