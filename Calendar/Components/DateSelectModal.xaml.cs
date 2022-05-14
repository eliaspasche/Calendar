using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Calendar;

public partial class DateSelectModal : Window
{
    private MainWindow parentWindow;

    public DateSelectModal(MainWindow window)
    {
        parentWindow = window;
        InitializeComponent();
    }

    private void SearchButton_OnClick(object sender, RoutedEventArgs e)
    {
        SearchDate();
    }

    private void DateSelectModal_OnLoaded(object sender, RoutedEventArgs e)
    {
        // Init month select with months
        FillDropdown();

        MonthSelect.SelectedItem = parentWindow.CurrentMonth.Content;

        YearSelect.Text = parentWindow.CurrentYear.Content.ToString();
    }

    private void FillDropdown()
    {
        MonthSelect.Items.Clear();
        foreach (Month month in Enum.GetValues(typeof(Month)))
        {
            MonthSelect.Items.Add(month.StringLiteral());
        }
    }

    private void ModalHeading_EventHandler(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        CloseModal();
    }

    private void CloseModal()
    {
        this.Close();
        parentWindow.Effect = null;
    }

    private void SearchDate()
    {
        Month month = MonthSelect.Text.ToMonth();
        int year = int.Parse(YearSelect.Text);

        parentWindow.SetCalenderSheet(month, year);
        CloseModal();
    }

    private void YearSelect_OnEnter(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Return && ValidateYear(YearSelect.Text))
        {
            SearchDate();
        }
    }

    private void YearSelect_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var _regex = new Regex("[^0-9.-]+");
        e.Handled = _regex.IsMatch(e.Text);
    }

    private void YearSelect_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (ValidateYear(YearSelect.Text))
        {
            LabelValidation.Content = "";
            YearSelect.BorderBrush = new SolidColorBrush(Color.FromRgb(242, 242, 242));
            SearchButton.IsEnabled = true;
        }
        else
        {
            LabelValidation.Content = "Year must be between 1582 and 3000.";
            YearSelect.BorderBrush = Brushes.Red;
            SearchButton.IsEnabled = false;
        }
    }

    private bool ValidateYear(string year)
    {
        try
        {
            var yearInt = int.Parse(year);
            return yearInt >= 1582 && yearInt <= 3000;
        }
        catch
        {
            return false;
        }
    }
}