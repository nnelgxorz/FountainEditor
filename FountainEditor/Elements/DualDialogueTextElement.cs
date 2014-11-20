using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class DualDialogueTextElement : Element
    {
        public DualDialogueTextElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<dch>{0}</dch>", Text);
        }
    }
}
