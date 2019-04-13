using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MultiLauncher.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            IServiceCollection collection = new ServiceCollection();

            // Application ViewModel
            collection.AddSingleton(typeof(ApplicationViewModel));

            // Build the provider
            DI.BuildServiceProvider(collection);
        }
    }
}
