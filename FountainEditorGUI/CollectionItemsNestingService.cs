using System.Collections.ObjectModel;
using System.Linq;

namespace FountainEditorGUI
{
    public sealed class CollectionItemsNestingService
    {
        private IAddHashtags addHashTags;
        private IRemoveHashtags removeHashTags;

        public CollectionItemsNestingService(
            IAddHashtags addHashTags,
            IRemoveHashtags removeHashTags
            )
        {
            this.addHashTags = addHashTags;
            this.removeHashTags = removeHashTags;
        }

        public ObservableCollection<string> DoNesting(ObservableCollection<string> collection, int amount, string dropAction)
        {
            if (amount == 0 && !(dropAction.Equals("Nest")))
            {
                return collection;
            }

            for (int i = 0; i < collection.Count; i++)
            {
                string text = collection.ElementAt(i);
                if (text.StartsWith("#"))
                {
                    if (amount < 0)
                    {
                        text = removeHashTags.Remove(text, -amount);
                    }
                    else
                    {
                        text = addHashTags.Add(text, amount + 1);
                    }
                    collection.Insert(i + 1, text);
                    collection.RemoveAt(i);
                }
            }
            return collection;
        }
    }
}