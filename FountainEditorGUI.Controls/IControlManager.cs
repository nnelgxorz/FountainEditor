using System.Windows;

namespace FountainEditorGUI.Controls {
    public interface IControlManager {
        FrameworkElement FindControl(string socketName);
    }
}
