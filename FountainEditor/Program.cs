using System;
using System.IO;


namespace FountainEditor
{
    class Program
    {
        static void Main()
        {
            var tokenizer = new Tokenizer();

            //tokenizer.Parse(Console.In);
            tokenizer.Parse(new StreamReader(@"C:\Users\Glenn\Desktop\Screenplay.txt"));
        }
    }
}
