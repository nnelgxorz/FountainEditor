using System.Collections.ObjectModel;
using System.Linq;

namespace FountainEditorGUI
{
    public sealed class GetEndOfHierarchicalElementsIndex
    {
        private ITextCounter counter;

        public GetEndOfHierarchicalElementsIndex(ITextCounter counter)
        {
            this.counter = counter;
        }

        public int GetIndex(ObservableCollection<string> collection, int depth, int index)
        {
            int endSelection = collection.Count;

            for (int i = index + 1; i < collection.Count; i++)
            {
                string currentText = collection.ElementAt(i);
                
                if (currentText.StartsWith("#"))
                {
                    int curHashes = counter.CountHashTags(currentText);
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
