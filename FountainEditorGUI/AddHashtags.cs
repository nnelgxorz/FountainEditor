using System.Linq;

namespace FountainEditorGUI
{
    public sealed class AddHashtags : FountainEditorGUI.IAddHashtags
    {
        public string Add (string text, int amount)
        {
            return string.Join("", Enumerable.Repeat("#", amount)) + text;
        }
    }
}
