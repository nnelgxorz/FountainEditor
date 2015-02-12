using System;
using System.Windows.Markup;

namespace FountainEditorGUI.Extensions
{
    public sealed class InjectInstanceExtension : MarkupExtension
    {
        private Type serviceType;

        public InjectInstanceExtension(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ServiceLocator.Current == null)
            {
                return null;
            }

            return ServiceLocator.Current.GetInstance(serviceType);
        }
    }
}
