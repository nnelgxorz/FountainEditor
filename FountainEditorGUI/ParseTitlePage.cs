using Antlr4.Runtime;
using FountainEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    class ParseTitlePage
    {
        public static Block Parse(string text)
        {
            var input = new AntlrInputStream(text);
            var lexer = new TitlePageLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new TitlePageParser(tokens);
            var output = parser.compileUnit();
            var visitor = new TitlePageVisitor();

            return visitor.Visit(output);
        }
    }
}
