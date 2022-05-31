using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;
using Calendar.Common;
using Calendar.Services;

namespace Calendar.Components
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Constructor to initialize the window.
        /// </summary>
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
        /// Handles clicks on the close button. Closes the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Runs immediately after the window is loaded. The method is used to fill in the current data. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Init calendar with current date
            SetCalendarToToday();
            GetWindow(this).KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                SetPrevMonth();
            }

            if (e.Key == Key.Right)
            {
                SetNextMonth();
            }
        }


        /// <summary>
        /// Displays the data of the current month in the calendar sheet.
        /// </summary>
        private void SetCalendarToToday()
        {
            SetCalenderSheet(DateTime.Today.Month.ToMonth(), DateTime.Today.Year);
        }


        /// <summary>
        /// Displays the days of the given month in the calender sheet (including national holidays and the last days
        /// of the previous month as well as the first days of the next month).
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        public void SetCalenderSheet(Month month, int year)
        {
            ClearCalendarSheet();

            // sets the labels for current month and year
            CurrentMonth.Content = month.StringLiteral();
            CurrentYear.Content = year;

            // determine the integer value of the weekday of the first day of the given month
            var firstWeekday = CalendarService.GetWeekday(01, month, year).IntLiteral() - 2;

            // get the number of days of the current and last month
            var daysCurrentMonth = month.NumberOfDays(year);
            var daysPrevMonth = month.PrevMonth().NumberOfDays(year);

            // loop through all day panels in the calendar sheet
            for (var i = 0; i < DatePanels.Children.Count; i++)
            {
                // select the working DayPanel of this loop
                var dayPanel = (DayPanel) DatePanels.Children[i];

                // display the last days of the previous month
                if (i <= firstWeekday)
                {
                    dayPanel.Day = daysPrevMonth - firstWeekday + i;
                    dayPanel.IsCurrentMonth = false;
                }

                // display the days of the current month
                else if (i > firstWeekday && i <= firstWeekday + daysCurrentMonth)
                {
                    dayPanel.Day = i - firstWeekday;
                    dayPanel.IsCurrentMonth = true;
                }

                // display the first days of the next month
                else
                {
                    dayPanel.Day = i - firstWeekday - daysCurrentMonth;
                    dayPanel.IsCurrentMonth = false;
                }
            }

            // set the holidays for the current month
            SetHolidays(month, year, firstWeekday);

            // set the prev/next month buttons
            SetMonthButtons(month, year);

            // Check if the first possible month is selected
            if (year == 1582 && month == Month.OCTOBER)
            {
                // Remove the invalid days
                RemoveDatesBeforeGregorianCalendar();
            }
        }

        /// <summary>
        /// Clears all days before the 15th of the month in the current calendar sheet. This is used to remove all
        /// days from the calendar that predate the introduction of the Gregorian calendar on Oct. 15, 1582.
        /// </summary>
        private void RemoveDatesBeforeGregorianCalendar()
        {
            foreach (DayPanel dayPanel in DatePanels.Children)
            {
                if (dayPanel.Day.Equals(15))
                {
                    break;
                }

                dayPanel.Day = "";
                dayPanel.Holiday = "";
                dayPanel.IsCurrentMonth = false;
            }
        }

        /// <summary>
        /// Determines whether the buttons to switch to the previous or next month are enabled or disabled.
        /// This prevents leaving the allowed range of the calendar
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        private void SetMonthButtons(Month month, int year)
        {
            // If the current month/year is december 3000 then the next month button should be disabled
            if (month == Month.DECEMBER && year == 3000)
            {
                NextMonthButton.IsEnabled = false;
            }
            // re-enable the button if it is unnecessarily disabled
            else
            {
                NextMonthButton.IsEnabled = true;
            }

            // If the current month/year is october 1582 then the previous month button should be disabled
            if (month == Month.OCTOBER && year == 1582)
            {
                PrevMonthButton.IsEnabled = false;
            }
            // re-enable the button if it is unnecessarily disabled
            else
            {
                PrevMonthButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// Retrieves the holidays for the given month and displays the holidays in the correct day panels.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="firstWeekday"></param>
        private void SetHolidays(Month month, int year, int firstWeekday)
        {
            // get holidays of this month
            var holidays = month.HolidaysInThisMonth(year);

            // display the holidays
            foreach (var holiday in holidays)
            {
                var dayPanel = (DayPanel) DatePanels.Children[holiday.ToDate(year).Day + firstWeekday];
                dayPanel.Holiday = holiday.StringLiteral();
            }
        }

        /// <summary>
        /// Clears the whole calendar sheet to prepare the calendar to show new data.
        /// </summary>
        private void ClearCalendarSheet()
        {
            foreach (DayPanel dayPanel in DatePanels.Children)
            {
                dayPanel.Day = "";
                dayPanel.Holiday = "";
            }
        }

        /// <summary>
        /// Handles clicks on the next moth button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextMonth_OnClick(object sender, RoutedEventArgs e)
        {
            SetNextMonth();
        }

        /// <summary>
        /// Displays the next month in the calendar sheet.
        /// </summary>
        private void SetNextMonth()
        {
            // determine the next month and year
            var nextMonth = CurrentMonth.Content.ToString()!.ToMonth().NextMonth();
            var nextYear = nextMonth == Month.JANUARY ? (int) CurrentYear.Content + 1 : (int) CurrentYear.Content;

            SetCalenderSheet(nextMonth, nextYear);
        }

        /// <summary>
        /// Handles clicks on the previous month button. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrevMonth_OnClick(object sender, RoutedEventArgs e)
        {
            SetPrevMonth();
        }

        /// <summary>
        /// Displays the previous month in the calendar sheet.
        /// </summary>
        private void SetPrevMonth()
        {
            // determine the previous month and year
            var prevMonth = CurrentMonth.Content.ToString()!.ToMonth().PrevMonth();
            var prevYear = prevMonth == Month.DECEMBER ? (int) CurrentYear.Content - 1 : (int) CurrentYear.Content;

            SetCalenderSheet(prevMonth, prevYear);
        }

        /// <summary>
        /// Handles clicks on the select date button. This will open the select date modal window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectDate_OnClick(object sender, RoutedEventArgs e)
        {
            var modal = new DateSelectModal(this);
            Effect = new BlurEffect();
            modal.ShowDialog();
        }

        /// <summary>
        /// Handles clicks on the today button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Today_OnClick(object sender, RoutedEventArgs e)
        {
            SetCalendarToToday();
        }
    }
}