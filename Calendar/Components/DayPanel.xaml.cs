using System.Windows;
using System.Windows.Media;

namespace Calendar.Components;

/// <summary>
/// Component to display a single day with an optional national holiday.
/// An attribute can also be used to specify whether the DayPanel contains a day from the currently displayed month.
/// </summary>
public partial class DayPanel
{
    public DayPanel()
    {
        InitializeComponent();
        DataContext = this;
    }

    /// <summary>
    /// Used to display the date (integer value).
    /// </summary>
    public object Day
    {
        get => DayLabel.Content;
        set => DayLabel.Content = value;
    }

    /// <summary>
    /// Used to display a national holiday in the panel
    /// </summary>
    public string Holiday
    {
        set
        {
            HolidayLabel.Content = value;
            HolidayBorder.Visibility = value.Equals("") ? Visibility.Hidden : Visibility.Visible;
        }
    }

    /// <summary>
    /// Defines the styling for the current month to highlight the days of the current month.
    /// </summary>
    public bool IsCurrentMonth
    {
        set
        {
            if (value)
            {
                PanelBorder.Background = new SolidColorBrush(Color.FromRgb(249, 249, 249));
                DayLabel.FontWeight = FontWeights.Bold;
            }
            else
            {
                PanelBorder.Background = new SolidColorBrush(Color.FromRgb(252, 252, 252));
                DayLabel.FontWeight = FontWeights.ExtraLight;
            }
        }
    }
}