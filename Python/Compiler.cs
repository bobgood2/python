using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Python
{
    using Python.Antlr;
    using System.Text.RegularExpressions;

    public partial class Compiler : Python3ParserBaseVisitor<IPythonTreeNode>
    {
        public IPythonTreeNode Run(string program)
        {
            var input = new AntlrInputStream(program);
            var lexer = new Python3Lexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new Python3Parser(tokens);
            var tree = parser.file_input(); // Replace 'startRule' with your actual start rule
            return this.Visit(tree);
        }

        public static string EscapeString(string value)
        {
            return value.Replace("\\", "\\\\")
                        .Replace("\"", "\\\"")
                        .Replace("\n", "\\n")
                        .Replace("\r", "\\r")
                        .Replace("\t", "\\t");
        }

        public static string UnescapeString(string value)
        {
            return Regex.Unescape(value);
        }


    }
}
