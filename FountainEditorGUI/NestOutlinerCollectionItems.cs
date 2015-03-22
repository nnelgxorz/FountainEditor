using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class NestOutlinerCollectionItems
    {
        private AddHashtags addHashTags;
        public NestOutlinerCollectionItems(AddHashtags addHashTags)
        {
            this.addHashTags = addHashTags;
        }
        public ObservableCollection<string> Nest (ObservableCollection<string> collection, int amount)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                string text = collection.ElementAt(i);
                if (text.StartsWith("#"))
                {
                    text = addHashTags.Add(text, amount);
                    collection.Insert(i + 1, text);
                    collection.RemoveAt(i);
                }
            }
            return collection;
        }
    }
}
