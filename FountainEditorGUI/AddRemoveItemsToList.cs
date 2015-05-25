using FountainEditorGUI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class AddRemoveItemsToList
    {
        public List<SectionIndexClass> Add(List<SectionIndexClass> list, List<SectionIndexClass> subList, int insertStart, DragDropMessage message)
        {
            if (insertStart >= list.Count)
            {
                for (int i = 0; i < subList.Count; i++)
                {
                    list.Add(subList.ElementAt(i));
                }
            }
            else
            {
                for (int i = subList.Count - 1; i >= 0; i--)
                {
                    if (message.dropAction.Equals("After") || message.dropAction.Equals("Nest"))
                    {
                        if (insertStart == list.Count - 1)
                        {
                            list.Add(subList.ElementAt(i));
                        }
                        else
                        {
                            list.Insert(insertStart, subList.ElementAt(i));
                        }
                    }
                    else
                    {
                        list.Insert(insertStart, subList.ElementAt(i));
                    }
                }
            }

            return list;
        }

        public List<SectionIndexClass> Remove(List<SectionIndexClass> list, int sectionStart, int sectionEnd)
        {
            if (sectionStart == sectionEnd)
            {
                list.RemoveAt(sectionStart);
            }
            else
            {
                for (int i = sectionStart; i < sectionEnd; i++)
                {
                    list.RemoveAt(sectionStart);
                }
            }

            return list;
        }
    }
}
