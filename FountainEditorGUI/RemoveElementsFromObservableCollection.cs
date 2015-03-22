using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class RemoveElementsFromObservableCollection
    {
        public ObservableCollection<string> Remove (ObservableCollection<string> collection, int index, int count)
        {
            for (int i = 0; i < count; i++)
			{
                collection.RemoveAt(index);
			}
            return collection;
        }
    }
}
