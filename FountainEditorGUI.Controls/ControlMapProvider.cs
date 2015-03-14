using System;
using System.Collections.Generic;
using System.Linq;

namespace FountainEditorGUI.Controls {
    public sealed class ControlMapProvider : IControlMapProvider {
        private IControlMapProvider[] providers;

        public ControlMapProvider(IControlMapProvider[] providers) {
            this.providers = providers;
        }

        public Dictionary<string, Type> GetMap() {
            return providers.SelectMany(e => e.GetMap()).ToDictionary(e => e.Key, e => e.Value);
        }
    }
}
