using System.Collections.Generic;
using System.Windows;

namespace FountainEditorGUI.Controls {
    public sealed class ControlSocketManager : IControlSocketManager {
        public IEnumerable<ControlSocket> GetControlSockets(DependencyObject parent) {
            var sockets = new List<ControlSocket>();

            DiscoverSockets(parent, sockets);

            return sockets;
        }

        public void DiscoverSockets(DependencyObject parent, List<ControlSocket> sockets) {
            if (parent == null) {
                return;
            }

            if (parent is ControlSocket) {
                sockets.Add(parent as ControlSocket);
            }

            foreach (var child in LogicalTreeHelper.GetChildren(parent as DependencyObject)) {
                DiscoverSockets(child as DependencyObject, sockets);
            }
        }
    }
}
