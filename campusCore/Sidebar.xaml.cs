using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace campusCore
{
    /// <summary>
    /// Sidebar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Sidebar : UserControl
    {
        public Sidebar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // MainWindow 안 Frame에 StuInfo 페이지 띄우기
            MainWindow main = Application.Current.MainWindow as MainWindow;
            if (main != null)
            {
                main.MainFrame.Navigate(new StuInfo());
            }
        }
    }
}
