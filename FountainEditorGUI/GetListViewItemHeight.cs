using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FountainEditorGUI
{
    public sealed class GetListViewItemHeight
    {
        public double GetHeight (ListView listView, int index)
        {
            ListViewItem item = listView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
            double height = item.ActualHeight;

            return height;
        }
    }
}
