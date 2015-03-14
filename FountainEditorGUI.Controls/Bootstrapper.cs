using System.Windows;

namespace FountainEditorGUI.Controls {
    public sealed class Bootstrapper {
        private Window shell;
        private IControlSocketManager controlSocketManager;
        private IControlManager controlManager;

        public Bootstrapper(Window shell, IControlSocketManager controlSocketManager, IControlManager controlManager) {
            this.shell = shell;
            this.controlSocketManager = controlSocketManager;
            this.controlManager = controlManager;
        }

        public void Run() {
            PlugControlsIntoSockets(shell);

            shell.Show();
        }

        private void PlugControlsIntoSockets(Window shell) {
            var controlSockets = controlSocketManager.GetControlSockets(shell);

            foreach (var controlSocket in controlSockets) {
                var control = controlManager.FindControl(controlSocket.Name);
                if (control == null) {
                    controlSocket.Content = null;
                }
                else {
                    controlSocket.Content = control;
                }
            }
        }
    }
}
