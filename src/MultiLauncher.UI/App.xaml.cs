using System.Linq;
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
            try
            {
                foreach (var arg in e.Args)
                {
                    MessageBox.Show(arg);
                }


                // Start the program as normal if there are no startup arguments
                if (!e.Args.Any())
                    return;

                if (!ValidateArguments(e.Args))
                    // TODO : Show error indicating the arguments where invalid.
                    return;


                // TODO : Start the files

                this.Shutdown(0);
            }
            catch
            {

            }

            base.OnStartup(e);
        }

        private bool ValidateArguments(string[] args)
        {
            // To many arguments
            if (args.Length > 1)
                return false;

            // Not a file
            if (args[0].Split('.').Length < 2)
                return false;

            // Not a supported filetype
            if (args[0].Split('.').Last() != "filetyp or types")
                return false;

            return true;
        }

    }
}
