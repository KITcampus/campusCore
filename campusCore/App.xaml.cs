using System.Configuration;
using System.Data;
using System.Windows;

namespace campusCore
{
    
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 로그인 창 먼저 띄우기
            Login login = new Login();
            login.Show();
        }
    }

}
