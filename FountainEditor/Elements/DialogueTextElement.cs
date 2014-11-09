using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    class DialogueTextElement : IElement
    {
        public string Text { get; set; }

        public DialogueTextElement (string text)
        {
            this.Text = text;
        }
    }
}
