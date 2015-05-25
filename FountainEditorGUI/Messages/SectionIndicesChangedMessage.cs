using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI.Messages
{
    public sealed class SectionIndicesChangedMessage
    {
        public List<SectionIndexClass> indices;
        public SectionIndicesChangedMessage(List<SectionIndexClass> indices)
        {
            this.indices = indices;
        }
    }
}
