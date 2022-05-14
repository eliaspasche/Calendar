using System;
using System.Windows;
using System.Windows.Input;
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
        Month month = MonthSelect.Text.ToMonth();
        int year = int.Parse(YearSelect.Text);

        parentWindow.SetCalenderSheet(month, year);
        CloseModal();
    }

    private void DateSelectModal_OnLoaded(object sender, RoutedEventArgs e)
    {
        // Init month select with months
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
}