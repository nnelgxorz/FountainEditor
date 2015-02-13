using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FountainEditor.ObjectModel;

namespace FountainEditor.Elements
{
    public class TitlePageValue : Element
    {
        public TitlePageValue(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
        }
    }
}
