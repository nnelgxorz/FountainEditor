using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using SimpleInjector;

namespace FountainEditorGUI
{
    public class SimpleInjectorServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private Container container;

        public SimpleInjectorServiceLocatorAdapter(Container container)
        {
            this.container = container;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return container.GetAllInstances(serviceType);
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return container.GetInstance(serviceType);
        }
    }
}
