using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI.Messages
{
    public sealed class DragDropMessage
    {
        public int dragItemDepth { get; private set; }
        public int dropItemDepth { get; private set; }
        public int dragIndex { get; private set; }
        public int dropIndex { get; private set; }
        public string dragItem { get; private set; }
        public string dropItem { get; private set; }

        public DragDropMessage (int dragIndex, int dropIndex, string dragItem, string dropItem, int dragItemDepth, int dropItemDepth)
        {
            this.dragIndex = dragIndex;
            this.dropIndex = dropIndex;
            this.dragItem = dragItem;
            this.dropItem = dropItem;
            this.dragItemDepth = dragItemDepth;
            this.dropItemDepth = dropItemDepth;
        }
    }

}
