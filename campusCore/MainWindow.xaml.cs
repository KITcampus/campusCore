using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using campusCore.Attendance;
using campusCore.Common;

namespace campusCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            int year = DateTime.Now.Year;
            int month = 12;
            TitleText.Text = $"{year}년 {month}월";
            BuildMonthCalendar(year, month);
        }

        // 달력 함수
        private void BuildMonthCalendar(int year, int month)
        {
            CalendarGrid.Children.Clear();
            CalendarGrid.RowDefinitions.Clear();
            CalendarGrid.ColumnDefinitions.Clear();

            // 7열 (일 ~ 토)
            for (int c = 0; c < 7; c++)
                CalendarGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int r = 0; r < 6; r++)
                CalendarGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            DateTime first = new DateTime(year, month, 1);
            int startDayOfWeek = (int)first.DayOfWeek;
            int days = DateTime.DaysInMonth(year, month);

            for (int day = 1; day <= days; day++)
            {
                int index = startDayOfWeek + (day - 1);
                int row = index / 7;
                int col = index % 7;

                var border = new Border
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(220, 220, 220)),
                    BorderThickness = new Thickness(0.5),
                    Margin = new Thickness(1),
                    Background = Brushes.White
                };

                // StackPanel로 날짜 + 상태 표시
                var sp = new StackPanel
                {
                    Margin = new Thickness(4),
                    VerticalAlignment = VerticalAlignment.Top
                };

                // 날짜 텍스트
                var txtDay = new TextBlock
                {
                    Text = day.ToString(),
                    FontSize = 14,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };

                // 오늘 날짜 강조
                DateTime thisDate = new DateTime(year, month, day);
                if (thisDate.Date == DateTime.Today.Date)
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(240, 248, 255));
                    txtDay.FontWeight = FontWeights.Bold;
                    txtDay.Foreground = Brushes.DarkBlue;
                }

                // 출석 상태 텍스트
                var txtStatus = new TextBlock
                {
                    FontSize = 12,
                    Margin = new Thickness(0, 5, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                // 달력에 내 출결상태 표시
                string date = thisDate.ToString("yyyy-MM-dd");
                string studentId = UserSession.StudentId;
                string status = AttendanceDAO.GetStatus(studentId, date);

                if (status == "출석")
                {
                    txtStatus.Text = "출석";
                    txtStatus.Foreground = new SolidColorBrush(Color.FromRgb(0, 120, 0)); // 초록
                }
                else if (status == "지각")
                {
                    txtStatus.Text = "지각";
                    txtStatus.Foreground = new SolidColorBrush(Color.FromRgb(200, 120, 0)); // 주황
                }
                else if (status == "결석")
                {
                    txtStatus.Text = "결석";
                    txtStatus.Foreground = new SolidColorBrush(Color.FromRgb(200, 0, 0)); // 빨강
                }
                else
                {
                    txtStatus.Text = "";  // 기록 없는 날은 빈칸
                }

                // StackPanel에 날짜 + 상태 넣기
                sp.Children.Add(txtDay);
                sp.Children.Add(txtStatus);

                // Border의 자식으로 StackPanel 설정
                border.Child = sp;

                Grid.SetRow(border, row);
                Grid.SetColumn(border, col);
                CalendarGrid.Children.Add(border);
            }
        }
    }
}
