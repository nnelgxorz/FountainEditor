using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FountainEditorGUI
{
    public sealed class UnNestOutlinerCollectionItems
    {
        private RemoveHashtags removeHastTags;
        public UnNestOutlinerCollectionItems(RemoveHashtags removeHastTags)
        {
            this.removeHastTags = removeHastTags;
        }
        public ObservableCollection<string> UnNest(ObservableCollection<string> collection, int amount)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                string text = collection.ElementAt(i);
                if (text.StartsWith("#"))
                {
                    text = removeHastTags.Remove(text, amount);
                    collection.Insert(i + 1, text);
                    collection.RemoveAt(i);
                }
            }

            return collection;
        }
    }
}
