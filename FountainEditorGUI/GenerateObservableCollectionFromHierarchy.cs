using System.Collections.ObjectModel;
using System.Linq;

namespace FountainEditorGUI
{
    public sealed class GenerateObservableCollectionFromHierarchy
    {
        private readonly ITextCounter counter;

        public GenerateObservableCollectionFromHierarchy(ITextCounter counter)
        {
            this.counter = counter;
        }

        public ObservableCollection<string> Generate(ObservableCollection<string> collection, int index, int depth)
        {
            ObservableCollection<string> subCollection = new ObservableCollection<string>();
            subCollection.Add(collection.ElementAt(index));
            
            for (int i = index+1; i < collection.Count; i++)
            {
                string currentItem = collection.ElementAt(i);

                int currentDepth = counter.CountHashTags(currentItem);
                if (currentItem.StartsWith("=") || currentDepth > depth)
                {
                    subCollection.Add(currentItem);
                }

                else
                {
                    break;
                }
            }

            return subCollection;
        }
    }
}
