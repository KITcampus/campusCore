using campusCore.Common;
using System.Text.RegularExpressions;
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

        private bool isPwValid = false;
        private bool isPwMatch = false;


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

        private void txtNewPw_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string pw = txtNewPw.Password;
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#]).{8,}$";

            isPwValid = Regex.IsMatch(pw, pattern);

            if (!isPwValid)
                txtPwCheckMsg.Text = "❌ 대/소문자, 숫자, 특수문자 포함 8자 이상";
            else
            {
                txtPwCheckMsg.Foreground = System.Windows.Media.Brushes.Green;
                txtPwCheckMsg.Text = "✔ 사용 가능한 비밀번호";
            }

            CheckPasswordMatch();
        }

        private void txtPwConfirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckPasswordMatch();
        }

        private void CheckPasswordMatch()
        {
            if (txtNewPw.Password == string.Empty || txtNewPwConfirm.Password == string.Empty)
            {
                txtPwMatchMsg.Text = "";
                return;
            }

            if (txtNewPw.Password == txtNewPwConfirm.Password)
            {
                if (!isPwValid)
                {
                    txtPwMatchMsg.Text = "✔ 비밀번호는 같지만 조건이 충족되지 않았습니다.";
                    txtPwMatchMsg.Foreground = System.Windows.Media.Brushes.Orange;
                }
                else
                {
                    txtPwMatchMsg.Text = "✔ 비밀번호가 일치합니다.";
                    txtPwMatchMsg.Foreground = System.Windows.Media.Brushes.Green;
                }

                isPwMatch = true;
            }
            else
            {
                txtPwMatchMsg.Text = "❌ 비밀번호가 일치하지 않습니다.";
                txtPwMatchMsg.Foreground = System.Windows.Media.Brushes.Red;
                isPwMatch = false;
            }
        }

        private void ChangePw_Click(object sender, RoutedEventArgs e)
        {
            if (!isPwValid)
            {
                MessageBox.Show("비밀번호 조건을 만족해야 합니다.");
                return;
            }

            if (!isPwMatch)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
                return;
            }

            var vm = this.DataContext as StuInfoViewModel;

            if (vm.ChangePassword(txtNewPw.Password))
            {
                MessageBox.Show("비밀번호가 변경되었습니다!");

                // 입력창 초기화
                txtNewPw.Password = string.Empty;
                txtNewPwConfirm.Password = string.Empty;

                // 메시지 초기화
                txtPwCheckMsg.Text = string.Empty;
                txtPwMatchMsg.Text = string.Empty;

                // 상태 초기화
                isPwValid = false;
                isPwMatch = false;
            }
            else
            {
                MessageBox.Show("변경 실패");
            }
        }
    }
}