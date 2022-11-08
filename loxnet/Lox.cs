using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace de.softwaremess.loxnet
{
    public class Lox
    {
        private static bool hadError;

        public static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: Lox [script]");
                throw new ArgumentException("Needs one argument [script]");
            }
            else if (args.Length == 1)
            {
                RunFile(args[0]);
            }
            else
            {
                throw new NotImplementedException("");
            }

        }

        private static void RunFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            Run(Encoding.Default.GetString(bytes));
            // Indicate an error in the exit code.
            if (hadError) Environment.Exit(65);
        }

        private static void Run(string source)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.scanTokens();

            // For now, just print the tokens.
            foreach (Token token in tokens)
            {
                Console.WriteLine(token);
            }
        }

        private static void RunPrompt()
        { 
            for (;;) 
            { 
                Console.Write("> ");
                string line = Console.In.ReadLine();
                if (line == null) break;
                Run(line);
                hadError = false;
            }
        }

        public static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        private static void Report(int line, string where, string message)
        {
            Console.Error.WriteLine("[line " + line + "] Error" + where + ": " + message);
            hadError = true;
        }

    }
}
