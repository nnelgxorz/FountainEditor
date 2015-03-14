using System;
using System.Collections.Generic;
using System.Windows;
using SimpleInjector;

namespace FountainEditorGUI.Controls {
    public sealed class ControlManager : IControlManager {
        private Container container;
        private Dictionary<string, Type> controlMap;

        public ControlManager(Container container, IControlMapProvider controlMapProvider) {
            this.container = container;
            this.controlMap = controlMapProvider.GetMap();
        }

        public FrameworkElement FindControl(string socketName) {
            var controlName = default(Type);
            if (!controlMap.TryGetValue(socketName, out controlName)) {
                return null;
            }

            return (FrameworkElement)container.GetInstance(controlName);
        }
    }
}
