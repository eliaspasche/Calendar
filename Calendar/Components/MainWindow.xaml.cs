using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using Calendar.Components;

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
            SetCalendarToToday();
        }

        private void SetCalendarToToday()
        {
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

            var isLeap = CalendarService.IsLeapYear(year);

            CurrentMonth.Content = month.StringLiteral();
            CurrentYear.Content = year;

            var firstWeekday = CalendarService.GetWeekday(01, month, year).IntLiteral() - 2;
            var daysCurrentMonth = month.NumberOfDays(isLeap);
            var daysPrevMonth = month.PrevMonth().NumberOfDays(isLeap);

            for (var i = 0; i < DatePanels.Children.Count; i++)
            {
                var dayPanel = (DayPanel) DatePanels.Children[i];

                if (i <= firstWeekday)
                {
                    dayPanel.Day = daysPrevMonth - firstWeekday + i;
                    dayPanel.IsCurrentMonth = false;
                }

                else if (i > firstWeekday && i <= firstWeekday + daysCurrentMonth)
                {
                    dayPanel.Day = i - firstWeekday;
                    dayPanel.IsCurrentMonth = true;
                }

                else
                {
                    dayPanel.Day = i - firstWeekday - daysCurrentMonth;
                    dayPanel.IsCurrentMonth = false;
                }
            }

            SetHolidays(month, year, firstWeekday);
            SetMonthButtons(month, year);
        }

        private void SetMonthButtons(Month month, int year)
        {
            if (month == Month.DECEMBER && year == 3000)
            {
                NextMonth_Button.IsEnabled = false;
            }
            else
            {
                NextMonth_Button.IsEnabled = true;
            }

            if (month == Month.OCTOBER && year == 1582)
            {
                PrevMonth_Button.IsEnabled = false;
            }
            else
            {
                PrevMonth_Button.IsEnabled = true;
            }
        }

        private void SetHolidays(Month month, int year, int firstWeekday)
        {
            var holidays = month.HolidaysInThisMonth(year);

            foreach (var holiday in holidays)
            {
                var dayPanel = (DayPanel) DatePanels.Children[holiday.ToDate(year).day + firstWeekday];
                dayPanel.Holiday = holiday.StringLiteral();
            }
        }

        /// <summary>
        /// Clears the whole calendar sheet to prepare the Calendar to show further information.
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextMonth_OnClick(object sender, RoutedEventArgs e)
        {
            var nextMonth = CurrentMonth.Content.ToString()!.ToMonth().NextMonth();
            var nextYear = nextMonth == Month.JANUARY ? (int) CurrentYear.Content + 1 : (int) CurrentYear.Content;

            SetCalenderSheet(nextMonth, nextYear);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrevMonth_OnClick(object sender, RoutedEventArgs e)
        {
            var prevMonth = CurrentMonth.Content.ToString()!.ToMonth().PrevMonth();
            var prevYear = prevMonth == Month.DECEMBER ? (int) CurrentYear.Content - 1 : (int) CurrentYear.Content;
            SetCalenderSheet(prevMonth, prevYear);
        }

        private void SelectDate_OnClick(object sender, RoutedEventArgs e)
        {
            DateSelectModal modal = new DateSelectModal(this);
            Effect = new BlurEffect();
            modal.ShowDialog();
        }

        private void Today_OnClick(object sender, RoutedEventArgs e)
        {
            SetCalendarToToday();
        }
    }
}