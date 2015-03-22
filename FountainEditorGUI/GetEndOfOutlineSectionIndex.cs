using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class GetEndOfOutlineSectionIndex
    {
        public int GetIndex(ObservableCollection<string> collection, int index)
        {
            int endSelection = collection.Count;

            for (int i = index + 1; i < collection.Count; i++)
            {
                string currentText = collection.ElementAt(i);
                
                if (currentText.StartsWith("#"))
                {
                        endSelection = i - 1;
                        break;
                }
            }
            return endSelection;
        }
    }
}
