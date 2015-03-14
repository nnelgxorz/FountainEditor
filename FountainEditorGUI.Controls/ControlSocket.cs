using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FountainEditorGUI.Controls {
    public sealed class ControlSocket : Control {
        public static DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content",
            typeof(object),
            typeof(ControlSocket)
        );

        public object Content {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public ControlSocket() {
            if (DesignerProperties.GetIsInDesignMode(this)) {
                var content = new ControlSocketDefaultContent();
                content.DataContext = this;

                this.Content = content;
            }
        }
    }
}
