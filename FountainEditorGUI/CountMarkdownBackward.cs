using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class CountMarkdownBackward : ITextCounter
    {
        public int Count(string text)
        {
            int count = 0;
            int length = text.Length;
            Char[] charArray = text.ToCharArray();

            for (int i = length - 1; i >= 0; i--)
            {
                var c = charArray[i];

                if (c.Equals('*') | c.Equals('_'))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return count;
        }
    }
}
