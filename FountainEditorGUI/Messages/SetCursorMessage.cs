using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI.Messages
{
    public sealed class SetCursorMessage
    {
        public int index;
        public int offset;

        public SetCursorMessage(int index, int offset)
        {
            this.index = index;
            this.offset = offset;
        }
    }
}
