using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace FunkyProgressBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private double _progressValue;
        public double ProgressValue
        {
            get => _progressValue;
            set
            {
                if (_progressValue != value)
                {
                    _progressValue = value;
                    OnPropertyChanged(nameof(ProgressValue));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            PropertyChanged += MainWindow_PropertyChanged;
        }

        private void MainWindow_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ProgressValue):
                    UpdateGradientFill();
                    break;
            }
        }

        private void UpdateGradientFill()
        {
            // Convert ProgressValue (0-100) to gradient offset (0-1)
            double normalizedProgress = ProgressValue / 100.0;

            RedGradientStop.Offset = 0;
            RedGradientStop.Color = Colors.Red;

            if (normalizedProgress > 0 && normalizedProgress <= 0.33)
            {
                OrangeGradientStop.Offset = normalizedProgress;
                OrangeGradientStop.Color = Colors.Orange;

                YellowGradientStop.Offset = 0.33;
                YellowGradientStop.Color = Colors.Transparent;

                GreenGradientStop.Offset = 0.66;
                GreenGradientStop.Color = Colors.Transparent;
            }
            else if (normalizedProgress > 0.33 && normalizedProgress <= 0.66)
            {
                OrangeGradientStop.Offset = 0.33;
                OrangeGradientStop.Color = Colors.Orange;

                YellowGradientStop.Offset = normalizedProgress;
                YellowGradientStop.Color = Colors.Yellow;

                GreenGradientStop.Offset = 0.66;
                GreenGradientStop.Color = Colors.Transparent;
            }
            else if (normalizedProgress > 0.66)
            {
                OrangeGradientStop.Offset = 0.33;
                OrangeGradientStop.Color = Colors.Orange;

                YellowGradientStop.Offset = 0.66;
                YellowGradientStop.Color = Colors.Yellow;

                GreenGradientStop.Offset = normalizedProgress;
                GreenGradientStop.Color = Colors.Green;
            }
            else
            {
                OrangeGradientStop.Color = Colors.Transparent;
                YellowGradientStop.Color = Colors.Transparent;
                GreenGradientStop.Color = Colors.Transparent;
            }
        }
    }
}