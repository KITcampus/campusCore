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

            HeaderControl.txtWelcome.Text = $"{UserSession.StudentNm}님 출석 체크가 완료되었습니다.";

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

            // 요일 7칸
            for (int c = 0; c < 7; c++)
                CalendarGrid.ColumnDefinitions.Add(new ColumnDefinition());

            // 최대 6주
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

                var cellGrid = new Grid
                {
                    Margin = new Thickness(4)
                };

                var txtDay = new TextBlock
                {
                    Text = day.ToString(),
                    FontSize = 14,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };
               
                DateTime thisDate = new DateTime(year, month, day);

                if (thisDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    txtDay.Foreground = new SolidColorBrush(Color.FromRgb(30, 80, 200)); // 파랑
                }
                else if (thisDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    txtDay.Foreground = new SolidColorBrush(Color.FromRgb(220, 50, 50)); // 빨강
                }
                // 오늘 날짜 강조
                if (thisDate.Date == DateTime.Today.Date)
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(240, 248, 255));
                    txtDay.FontWeight = FontWeights.Bold;
                    txtDay.Foreground = Brushes.DarkBlue;
                }

                // 도장
                var stampGrid = new Grid
                {
                    Width = 35,
                    Height = 35,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Margin = new Thickness(0, 0, 4, 4),
                    Visibility = Visibility.Hidden
                };

                var circle = new Ellipse
                {
                    Width = 35,
                    Height = 35,
                };

                var stampText = new TextBlock
                {
                    FontSize = 13,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                stampGrid.Children.Add(circle);
                stampGrid.Children.Add(stampText);

                // 출결 상태 가져오기
                string date = thisDate.ToString("yyyy-MM-dd");
                string studentId = UserSession.StudentId;
                string status = AttendanceDAO.GetStatus(studentId, date);

                if (status == "출석")
                {
                    circle.Fill = Brushes.Transparent;
                    circle.Stroke = new SolidColorBrush(Color.FromRgb(44, 160, 44));
                    circle.StrokeThickness = 3;

                    stampText.Text = "출석";
                    stampText.Foreground = new SolidColorBrush(Color.FromRgb(44, 160, 44));
                    stampGrid.Visibility = Visibility.Visible;
                }
                else if (status == "지각")
                {
                    circle.Fill = Brushes.Transparent;
                    circle.Stroke = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                    circle.StrokeThickness = 3;

                    stampText.Text = "지각";
                    stampText.Foreground = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                    stampGrid.Visibility = Visibility.Visible;
                }
                else if (status == "결석")
                {
                    circle.Fill = Brushes.Transparent;
                    circle.Stroke = new SolidColorBrush(Color.FromRgb(220, 40, 40));
                    circle.StrokeThickness = 3;

                    stampText.Text = "결석";
                    stampText.Foreground = new SolidColorBrush(Color.FromRgb(220, 40, 40));
                    stampGrid.Visibility = Visibility.Visible;
                }

                // 날짜 + 도장 추가
                cellGrid.Children.Add(txtDay);
                cellGrid.Children.Add(stampGrid);

                border.Child = cellGrid;

                Grid.SetRow(border, row);
                Grid.SetColumn(border, col);
                CalendarGrid.Children.Add(border);
            }
        }
    }
}
