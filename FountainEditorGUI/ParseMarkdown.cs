using System.Windows.Documents;
using Antlr4.Runtime;
using FountainEditor;

namespace FountainEditorGUI
{
    class ParseMarkdown
    {
        public static Inline Parse(string text)
        {
            var input = new AntlrInputStream(text);
            var lexer = new MarkdownLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new MarkdownParser(tokens);
            var output = parser.compileUnit();
            var visitor = new InlineVistitor();

            return visitor.Visit(output);
        }
    }
}
