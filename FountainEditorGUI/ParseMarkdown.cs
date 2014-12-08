using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using FountainEditor;

namespace FountainEditorGUI
{
    class ParseMarkdown
    {
        public static Span Parse(string input)
        {
            var stream = new Antlr4.Runtime.AntlrInputStream(input);
            var lexer = new MarkdownLexer(stream);
            var tokens = new Antlr4.Runtime.CommonTokenStream(lexer);
            var parser = new MarkdownParser(tokens);
            var tree = parser.compileUnit();
            var treeWalker = new Antlr4.Runtime.Tree.ParseTreeWalker();
            var visitor = new InlineVistitor();
            treeWalker.Walk(visitor, tree);
            Span span = visitor.s;
            return span;
        }
    }
}
