using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenPlayEditor.Elements
{
    class TransitionTextElement : IElement
    {
        public string Text { get; set; }

        public TransitionTextElement (string text)
        {
            this.Text = text;
        }
    }
}
