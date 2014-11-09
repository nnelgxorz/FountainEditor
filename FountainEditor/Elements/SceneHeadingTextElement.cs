using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    class SceneHeadingTextElement : IElement
    {
        public string Text { get; set; }

        public SceneHeadingTextElement (string text)
        {
            this.Text = text;
        }
    }
}
