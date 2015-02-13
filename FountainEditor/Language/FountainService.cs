using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FountainEditor.ObjectModel;

namespace FountainEditor.Language
{
    public sealed class FountainService : IFountainService
    {
        public Element[] Parse(string document)
        {
            return Parse(new AntlrInputStream(document));
        }

        public Element[] ParseFile(string fileName)
        {
            return Parse(new AntlrFileStream(fileName));
        }

        private Element[] Parse(ICharStream stream) {
            var lexer = new FountainLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new FountainParser(tokens);
            var walker = new ParseTreeWalker();
            var listener = new ElementListener();

            walker.Walk(listener, parser.compileUnit());

            return listener.Elements.ToArray();
        }
    }
}
