using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    class EmphasisTextElement : IElement
    {
        public string Text { get; set; }

        public EmphasisTextElement (string text)
        {
            this.Text = text;
        }
    }
}
