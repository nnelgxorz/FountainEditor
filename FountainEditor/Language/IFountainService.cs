using FountainEditor.ObjectModel;

namespace FountainEditor.Language
{
    public interface IFountainService
    {
        Element[] Parse(string text);

        Element[] ParseFile(string fileName);
    }
}
