using System.Windows;
using FountainEditor.Language;
using FountainEditor.Messaging;
using FountainEditorGUI.Views;
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

            container.RegisterSingle<IFountainService, FountainService>();
            container.RegisterOpenGeneric(typeof(IMessagePublisher<>), typeof(MessagePublisher<>), Lifestyle.Singleton);

            ServiceLocator.Current = new SimpleInjectorServiceLocatorAdapter(container);

            container.GetInstance<MainWindow>().Show();
        }
    }
}
