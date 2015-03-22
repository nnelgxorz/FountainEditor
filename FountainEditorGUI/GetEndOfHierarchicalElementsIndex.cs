using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class GetEndOfHierarchicalElementsIndex
    {
        private CountHashTags countHashTags;
        public GetEndOfHierarchicalElementsIndex(CountHashTags countHashtags)
        {
            this.countHashTags = countHashtags;
        }
        public int GetIndex(ObservableCollection<string> collection, int depth, int index)
        {
            int endSelection = collection.Count;

            for (int i = index + 1; i < collection.Count; i++)
            {
                string currentText = collection.ElementAt(i);
                
                if (currentText.StartsWith("#"))
                {
                    int curHashes = countHashTags.Count(currentText);

                    if (curHashes <= depth)
                    {
                        endSelection = i - 1;
                        break;
                    }
                }
            }
            return endSelection;
        }
    }
}
