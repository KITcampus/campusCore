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

                var txt = new TextBlock
                {
                    Text = day.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(6, 4, 4, 4),
                    FontSize = 14
                };

                DateTime thisDate = new DateTime(year, month, day);
                if (thisDate.Date == DateTime.Today.Date)
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(240, 248, 255));
                    txt.FontWeight = FontWeights.Bold;
                    txt.Foreground = Brushes.DarkBlue;
                }

                border.Child = txt;

                Grid.SetRow(border, row);
                Grid.SetColumn(border, col);
                CalendarGrid.Children.Add(border);
            }
        }
    }
}