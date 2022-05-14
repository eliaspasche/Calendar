using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Calendar.Components;

public partial class DayPanel : UserControl
{
    private bool _isCurrentMonth;
    private string? _holiday;

    public DayPanel()
    {
        InitializeComponent();
        DataContext = this;
    }

    public object Day
    {
        get => DayLabel.Content;
        set => DayLabel.Content = value;
    }

    public string Holiday
    {
        get => _holiday ?? "";
        set
        {
            _holiday = value;
            HolidayLabel.Content = value;
            HolidayBorder.Visibility = value.Equals("") ? Visibility.Hidden : Visibility.Visible;
        }
    }

    public bool IsCurrentMonth
    {
        get => _isCurrentMonth;
        set
        {
            _isCurrentMonth = value;
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