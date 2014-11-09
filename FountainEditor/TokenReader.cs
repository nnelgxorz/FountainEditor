using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor {
    public class TokenReader {
        private string text;
        private int offset;
        private int length;

        public bool EndOfString {
            get { return (offset + length) >= text.Length; }
        }

        public TokenReader(string text) {
            this.text = text;
        }

        public char PeekChar() {
            return text[offset + length];
        }

        public void SkipChar() {
            offset++;
        }

        public void TakeChar() {
            length++;
        }

        public string GetToken() {
            var token = text.Substring(offset, length);

            offset += length;
            length = 0;

            return token;
        }
    }
}
