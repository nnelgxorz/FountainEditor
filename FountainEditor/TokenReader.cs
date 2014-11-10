using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor 
{
    public class TokenReader 
    {
        private string text;
        private int offset;
        private int length;

        public bool EndOfString 
        {
            get
            {
                return (offset + length) >= text.Length;
            }
        }

        public TokenReader(string text) 
        {
            this.text = text;
        }

        public char PeekChar(int ahead = 0) 
        {
            return text[offset + length + ahead];
        }

        public void SkipChar(int amount = 1) 
        {
            offset += amount;
        }

        public void TakeChar(int amount = 1) 
        {
            length += amount;
        }

        public string GetToken()
        {
            var token = text.Substring(offset, length);

            offset += length;
            length = 0;

            return token;
        }
    }
}
