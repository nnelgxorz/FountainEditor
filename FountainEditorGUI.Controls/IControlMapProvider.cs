using System;
using System.Collections.Generic;

namespace FountainEditorGUI.Controls {
    public interface IControlMapProvider {
        Dictionary<string, Type> GetMap();
    }
}
