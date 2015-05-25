using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class SectionIndexClass
    {
        public int index;
        public string text;
        public int blockAmount;
        public int hashCount;
        public SectionIndexClass(int index, string text, int blockAmount, int hashCount)
        {
            this.index = index;
            this.text = text;
            this.blockAmount = blockAmount;
            this.hashCount = hashCount;
        }
    }
}
