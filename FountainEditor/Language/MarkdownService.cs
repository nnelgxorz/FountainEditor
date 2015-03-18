using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Language
{
    public sealed class MarkdownService : IMarkdownService
    {
        public ObjectModel.Element[] Parse(string text)
        {
            return Parse(new Antlr4.Runtime.AntlrInputStream(text));
        }

        private ObjectModel.Element[] Parse(Antlr4.Runtime.AntlrInputStream stream)
        {
            var lexer = new MarkdownLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new MarkdownParser(tokens);
            var walker = new ParseTreeWalker();
            var listener = new MarkdownListener();

            walker.Walk(listener, parser.compileUnit());

            return listener.Elements.ToArray();
        }
    }
}
