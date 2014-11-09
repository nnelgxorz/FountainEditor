using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenPlayEditor.Elements
{
    class ActionTextElement : IElement
    {
        public string Text { get; set; }

        public ActionTextElement (string text)
        {
            this.Text = text;
        }
    }
}
