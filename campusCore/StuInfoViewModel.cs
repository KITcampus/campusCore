using campusCore.Common;
using campusCore.Repository;
using System.ComponentModel;

namespace campusCore
{
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
            }
        }

        public StuInfoViewModel()
        {
            if (!string.IsNullOrEmpty(UserSession.StudentId))
                repo.GetStudent(int.Parse(UserSession.StudentId));

        }

        public void LoadStudent(int id)
        {
            Student = repo.GetStudent(id);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
