using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class GenerateObservableCollectionFromHierarchy
    {
        private CountHashTags countHashTags;
        public GenerateObservableCollectionFromHierarchy(CountHashTags countHashTags)
        {
            this.countHashTags = countHashTags;
        }
        public ObservableCollection<string> Generate(ObservableCollection<string> collection, int index, int depth)
        {
            ObservableCollection<string> subCollection = new ObservableCollection<string>();
            subCollection.Add(collection.ElementAt(index));

            for (int i = index + 1; i < collection.Count; i++)
            {
                string currentItem = collection.ElementAt(i);
                int currentDepth = countHashTags.Count(currentItem);

                if (currentItem.StartsWith("=") | currentDepth > depth)
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
