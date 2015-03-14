using System.Collections.Generic;
using System.Windows;

namespace FountainEditorGUI.Controls {
    public interface IControlSocketManager {
        IEnumerable<ControlSocket> GetControlSockets(DependencyObject parent);
    }
}
