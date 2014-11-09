using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenPlayEditor.Elements
{
    class OutlineTextElement : IElement
    {
        public string Text { get; set; }

        public OutlineTextElement (string text)
        {
            this.Text = text;
        }
    }
}
