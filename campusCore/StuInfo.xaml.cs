using campusCore.Common;
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
    /// StuInfo.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StuInfo : Page
    {
        private StuInfoViewModel vm;
    
        public StuInfo()
        {
            InitializeComponent();
            vm = new StuInfoViewModel();
            DataContext = vm;

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
