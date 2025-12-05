using System.Configuration;
using System.Data;
using System.Windows;

namespace campusCore
{
    
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            bool? result = login.ShowDialog();

            if (result == true)
            {
                MainWindow main = new MainWindow();
                main.Show();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }

}
