using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor
{
    public abstract class Element
    {
        public string Text { get; set; }

        public Element(string text) {
            this.Text = text;
        }
    }
}
