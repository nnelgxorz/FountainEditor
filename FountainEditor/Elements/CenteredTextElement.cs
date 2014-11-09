using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    class CenteredTextElement : IElement
    {
        public string Text { get; set; }

        public CenteredTextElement(string text)
        {
            this.Text = text;
        }
    }
}
