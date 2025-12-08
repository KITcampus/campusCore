using campusCore.Common;
using System.Windows;
using System.Windows.Controls;

namespace campusCore
{
    /// <summary>
    /// StuInfo.xaml 화면(Page)의 코드 비하인드 파일
    /// XAML과 상호작용 이벤트 처리 및 ViewModel을 바인딩하는 역할
    /// </summary>
    public partial class StuInfo : Page
    {
        // StuInfoViewModel 인스턴스를 저장할 필드
        private StuInfoViewModel vm;

        // Page가 생성될 때 실행되는 생성자
        public StuInfo()
        {
            InitializeComponent();
            // XAML(UI 요소들)을 초기화하는 메서드 (필수)

            vm = new StuInfoViewModel();
            // ViewModel 객체 생성 → 데이터 로딩은 ViewModel 내부 구조에 따라 자동 처리됨

            DataContext = vm;
            // WPF 바인딩 시스템에 ViewModel을 연결 → XAML에서 Binding 사용 가능해짐
        }

        // 데이터그리드에서 행을 선택했을 때 실행되는 이벤트 (현재는 비어있음)
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 나중에 행 선택 후 상세 정보 표시하거나 수정 기능 넣을 때 사용됨
        }

    }
}
