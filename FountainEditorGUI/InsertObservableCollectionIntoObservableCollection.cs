using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class InsertObservableCollectionIntoObservableCollection
    {
        public ObservableCollection<string> Insert (ObservableCollection<string> collection, ObservableCollection<string> subCollection, int index)
        {
            if (index >= collection.Count)
            {
                for (int i = 0; i < subCollection.Count; i++)
                {
                    collection.Add(subCollection.ElementAt(i));
                }
            }
            else
            {
                for (int i = subCollection.Count - 1; i >= 0; i--)
                {
                    collection.Insert(index + 1, subCollection.ElementAt(i));
                }
            }

            return collection;
        }
    }
}
