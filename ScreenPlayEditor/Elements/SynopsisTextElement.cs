using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenPlayEditor.Elements
{
    class SynopsisTextElement : IElement
    {
        public string Text { get; set; }

        public SynopsisTextElement (string text)
        {
            this.Text = text;
        }
    }
}
