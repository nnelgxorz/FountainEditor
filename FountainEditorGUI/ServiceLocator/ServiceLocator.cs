using Microsoft.Practices.ServiceLocation;

namespace FountainEditorGUI
{
    public sealed class ServiceLocator
    {
        public static IServiceLocator Current { get; set; }
    }
}
