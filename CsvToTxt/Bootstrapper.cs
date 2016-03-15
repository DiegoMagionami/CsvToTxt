using System.Windows;
using Prism.Unity;
using Microsoft.Practices.Unity;
using CsvToTxt.Views;

namespace CsvToTxt
{
    /*
     * Initialize all services 
     */
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
