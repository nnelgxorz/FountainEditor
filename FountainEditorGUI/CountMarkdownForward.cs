using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class CountMarkdownForward : ITextCounter
    {
        public int Count (string text)
        {
            int count = 0;
            int length = text.Length;
            Char[] charArray = text.ToCharArray();

            for (int i = 0; i < length; i++)
            {
                var c = charArray[i];

                if (c.Equals('*') | c.Equals('_'))
                {
                    count ++;   
                }

                else
                {
                    return count;
                }
            }

            return count;
        }
    }
}
