using System.Windows;
using FountainEditor.Messaging;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace FountainEditorGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var container = new Container();

            // Register services here
            // 

            container.RegisterSingle<MainWindow>();
            container.RegisterSingle<IDocumentService, DocumentService>();
            container.RegisterOpenGeneric(typeof(IMessagePublisher<>), typeof(MessagePublisher<>));

            ServiceLocator.Current = new SimpleInjectorServiceLocatorAdapter(container);

            container.GetInstance<MainWindow>().Show();
        }
    }
}
