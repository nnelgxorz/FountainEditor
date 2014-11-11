using System;
using System.IO;

namespace FountainEditor
{
    class Program
    {
        static void Main(params string[] args)
        {
            var tokenizer = new Tokenizer();
            tokenizer.Parse(string.Empty);
        }
    }
}
