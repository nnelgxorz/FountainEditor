using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenPlayEditor.Elements
{
    class LyricsTextElement : IElement
    {
        public string Text { get; set; }

        public LyricsTextElement (string text)
        {
            this.Text = text;
        }
    }
}
