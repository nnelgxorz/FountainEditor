using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI.Messages
{
    public sealed class DragDropMessage
    {
        public int dragIndex { get; private set; }
        public int dropIndex { get; private set; }
        public string dragItem { get; private set; }

        public DragDropMessage (int dragIndex, int dropIndex, string dragItem)
        {
            this.dragIndex = dragIndex;
            this.dropIndex = dropIndex;
            this.dragItem = dragItem;
        }
    }

}
