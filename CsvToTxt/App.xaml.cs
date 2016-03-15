using System.Windows;

namespace CsvToTxt
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bs = new Bootstrapper();
            bs.Run();
        }
    }
}
