using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class NoteTextElement : Element
    {
        public NoteTextElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<n>{0}</n>", Text);
        }
    }
}
