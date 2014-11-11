using System;
using System.IO;

namespace FountainEditor
{
    class Program
    {
        static void Main(params string[] args)
        {
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Parse(string.Empty);

            var optimizer = new Optimizer();
            optimizer.Optimize(tokens);
        }
    }
}
