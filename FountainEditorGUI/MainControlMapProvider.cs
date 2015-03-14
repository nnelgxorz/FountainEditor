using System;
using System.Collections.Generic;
using FountainEditorGUI.Controls;
using FountainEditorGUI.Views;

namespace FountainEditorGUI {
    public sealed class MainControlMapProvider : IControlMapProvider {
        public Dictionary<string, Type> GetMap() {
            return new Dictionary<string, Type> {
                { "socket1", typeof(FountainNameBox) },
                { "socket2", typeof(FountainOutline) },
                { "socket3", typeof(FountainTextBox) }
            };
        }
    }
}
