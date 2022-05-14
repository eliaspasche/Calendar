using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace Calendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  Used to enable drag and drop for the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainGrid_EventHandler(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// OnClick Handler for the close button. Closes the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            

            // Init calendar with current date
            SetCalenderSheet(DateTime.Today.Month.ToMonth(), DateTime.Today.Year);
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        public void SetCalenderSheet(Month month, int year)
        {
            ClearCalendarSheet();
            
            bool isLeap = CalendarService.IsLeapYear(year);

            CurrentMonth.Content = month.StringLiteral();
            CurrentYear.Content = year;

            int firstWeekday = CalendarService.GetWeekday(01, month, year).IntLiteral();
            int daysCurrentMonth = month.NumberOfDays(isLeap);
            int daysPrevMonth = month.PrevMonth().NumberOfDays(isLeap);

            for (var i = 1; i < 43; i++)
            {
                var dayPanel = (Label) FindName($"DayPanel{i}");

                if (i < firstWeekday)
                {
                    dayPanel.Content = daysPrevMonth - firstWeekday + 1 + i;
                    dayPanel.FontWeight = FontWeights.ExtraLight;
                }

                else if (i >= firstWeekday && i < firstWeekday + daysCurrentMonth)
                {
                    dayPanel.Content = i + 1 - firstWeekday;
                    dayPanel.FontWeight = FontWeights.Bold;
                }

                else
                {
                    dayPanel.Content = i + 1 - firstWeekday - daysCurrentMonth;
                    dayPanel.FontWeight = FontWeights.ExtraLight;
                }
            }
        }

        /// <summary>
        /// Clears the whole calendar sheet to prepare the Calendar to show further information.
        /// </summary>
        private void ClearCalendarSheet()
        {
            for (var i = 1; i < 43; i++)
            {
                var dayPanel = (Label) FindName($"DayPanel{i}");
                dayPanel.Content = "";
                dayPanel.FontWeight = FontWeights.Normal;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextMonth_OnClick(object sender, RoutedEventArgs e)
        {
            Month nextMonth = CurrentMonth.Content.ToString()!.ToMonth().NextMonth();
            int nextYear = nextMonth == Month.JANUARY ? (int) CurrentYear.Content + 1 : (int) CurrentYear.Content;
            SetCalenderSheet(nextMonth, nextYear);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrevMonth_OnClick(object sender, RoutedEventArgs e)
        {
            Month prevMonth = CurrentMonth.Content.ToString()!.ToMonth().PrevMonth();
            int prevYear = prevMonth == Month.DECEMBER ? (int) CurrentYear.Content - 1 : (int) CurrentYear.Content;
            SetCalenderSheet(prevMonth, prevYear);
        }

        private void SelectDate_OnClick(object sender, RoutedEventArgs e)
        {
            DateSelectModal modal = new DateSelectModal(this);
            Effect = new BlurEffect();
            modal.ShowDialog();
        }
    }
}