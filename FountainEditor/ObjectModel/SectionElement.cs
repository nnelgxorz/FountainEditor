using System.Collections.Generic;

namespace FountainEditor.ObjectModel
{
    public partial class SectionElement : Element
    {
        public List<Element> Children { get; set; }
        public int Level { get; set; }

        partial void Initialize(string text)
        {
            int level;

            for (level = 0; ; level++)
            {
                if (text[level] != '#')
                {
                    break;
                }
            }

            Children = new List<Element>();
            Level = level;
        }
    }
}
