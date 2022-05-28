using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Calendar.Common;

namespace Calendar.Components;

/// <summary>
/// Modal window to select a month and a year.
/// </summary>
public partial class DateSelectModal
{
    private readonly MainWindow _parentWindow;

    /// <summary>
    /// Constructor to initialize the component with the parent window.
    /// </summary>
    /// <param name="window"></param>
    public DateSelectModal(MainWindow window)
    {
        _parentWindow = window;
        InitializeComponent();
    }

    /// <summary>
    /// Handle a click on the search button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SearchButton_OnClick(object sender, RoutedEventArgs e)
    {
        SearchDate();
    }

    /// <summary>
    /// Runs immediately after the component is loaded. The method is used to fill in the current data. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DateSelectModal_OnLoaded(object sender, RoutedEventArgs e)
    {
        // Init month select with months
        FillDropdown();

        // Fill the month and year select with the current data
        MonthSelect.SelectedItem = _parentWindow.CurrentMonth.Content;
        YearSelect.Text = _parentWindow.CurrentYear.Content.ToString();
    }

    /// <summary>
    /// Clears and refills the dropdown of the ComboBox with all 12 month.
    /// </summary>
    private void FillDropdown()
    {
        MonthSelect.Items.Clear();
        foreach (Month month in Enum.GetValues(typeof(Month)))
        {
            MonthSelect.Items.Add(month.StringLiteral());
        }
    }

    /// <summary>
    /// Handles clicks on the heading row of the window. This handler enables drag and drop of the window.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ModalHeading_EventHandler(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    /// <summary>
    /// Handles clicks on the cancel button. This method closes the window without saving the selected month and year.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        CloseModal();
    }

    /// <summary>
    /// Closes the modal window and removes the blur effect of the parent window.
    /// </summary>
    private void CloseModal()
    {
        Close();
        _parentWindow.Effect = null;
    }

    /// <summary>
    /// Executes the search for the selected month and year in the parent window. The new calendar sheet will be set in
    /// the parent window.
    /// </summary>
    private void SearchDate()
    {
        var month = MonthSelect.Text.ToMonth();
        var year = int.Parse(YearSelect.Text);

        _parentWindow.SetCalenderSheet(month, year);
        CloseModal();
    }

    /// <summary>
    /// Executes the search for the selected month and year when the user presses Enter in the TextBox
    /// and the current entry could be validated.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void YearSelect_OnEnter(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Return && ValidateYear(YearSelect.Text))
        {
            SearchDate();
        }
    }

    /// <summary>
    /// Ensures that only numbers can be entered in the TextBox.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void YearSelect_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    /// <summary>
    /// Validates every change in the TextBox.
    /// Displays a hint and disables the search button if the input is outside the allowed time periods.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void YearSelect_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        // Refills the month select if needed
        if (MonthSelect.Items.Count != 12)
        {
            FillDropdown();
            MonthSelect.SelectedItem = Month.OCTOBER.StringLiteral();
        }

        // Removes the first 9 month from the dropdown if the year 1582 is selected.
        // This is needed because the calendar calculation applies to the gregorian calendar. 
        // This calendar was introduced in october 1582.
        if (YearSelect.Text == "1582")
        {
            for (var i = 0; i < 9; i++)
            {
                MonthSelect.Items.RemoveAt(0);
            }

            MonthSelect.SelectedItem = Month.OCTOBER.StringLiteral();
        }

        // Measures if the entered year can be validated
        if (ValidateYear(YearSelect.Text))
        {
            LabelValidation.Content = "";
            YearSelect.BorderBrush = new SolidColorBrush(Color.FromRgb(242, 242, 242));
            SearchButton.IsEnabled = true;
        }
        // Measures if the entered year can not be validated
        else
        {
            LabelValidation.Content = "Year must be between 1582 and 3000.";
            YearSelect.BorderBrush = Brushes.Red;
            SearchButton.IsEnabled = false;
        }
    }

    /// <summary>
    /// Returns true if the given year is in the allowed range of the gregorion calendar.
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    private static bool ValidateYear(string year)
    {
        try
        {
            var yearInt = int.Parse(year);
            return yearInt is >= 1582 and <= 3000;
        }
        catch
        {
            return false;
        }
    }
}