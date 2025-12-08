using campusCore.Common;
using campusCore.Repository;
using System.ComponentModel;
using System.Windows;

namespace campusCore
{
    // ViewModel: 뷰(XAML UI)와 데이터(DB 또는 DTO)를 연결하는 역할
    public class StuInfoViewModel : INotifyPropertyChanged
    {
        private readonly StuInfoRepository repo = new();

        private StuInfoDto student;
        public StuInfoDto Student
        {
            get => student;
            set
            {
                student = value;
                OnPropertyChanged(nameof(Student));
                OnPropertyChanged(nameof(MaskedResidentNumber)); // UI 업데이트
            }
        }

        // 생성자
        public StuInfoViewModel()
        {
            Student = new StuInfoDto(); // Null 방지

            if (!string.IsNullOrEmpty(UserSession.StudentId))
                LoadStudent(int.Parse(UserSession.StudentId));
        }

        // DB에서 학생 정보 로드
        public void LoadStudent(int id)
        {
            Student = repo.GetStudent(id);
        }

        // 주민번호 마스킹된 UI 표시용 속성
        public string MaskedResidentNumber
        {
            get
            {
                if (string.IsNullOrEmpty(Student?.residentNum))
                    return string.Empty;
                return Student.residentNum.Substring(0, 8) + "******";
            }
        }

        // 바인딩 이벤트 처리
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
