using FountainEditor.ObjectModel;

namespace FountainEditor.Language
{
    public interface IFountainService
    {
        Element[] Parse(string document);

        Element[] ParseFile(string fileName);
    }
}
