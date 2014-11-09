using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenPlayEditor.Elements
{
    class TextTextElement : IElement
    {
        public string Text { get; set; }

        public TextTextElement(string text)
        {
            this.Text = text;
        }
    }
}
