using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FountainEditorGUI
{
    public sealed class GetDropActionFromMousePosition
    {
        private GetListViewItemHeight getItemHeight;
        public GetDropActionFromMousePosition(GetListViewItemHeight getItemHeight)
        {
            this.getItemHeight = getItemHeight;
        }
        public string GetAction (ListView listView, int dropIndex, int dropItemDepth, DragEventArgs e)
        {
            var height = getItemHeight.GetHeight(listView, dropIndex);
            var listItem = listView.ItemContainerGenerator.ContainerFromIndex(dropIndex) as ListViewItem;
            var position = e.GetPosition(listItem);

            if (position.Y > height * .25 && position.Y < height * .75)
            {
                return "Nest";
            }

            if (position.Y > height * .75)
            {
                return "After";
            }

            if (position.Y < height * .25)
            {
                return "Before";
            }

            return null;
        }
    }
}
