using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WPF_Controls
{
    public partial class CircularProgressBar : ProgressBar
    {
        public CircularProgressBar()
        {
            this.ValueChanged += CircularProgressBar_ValueChanged;
        }

        void CircularProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CircularProgressBar bar = sender as CircularProgressBar;
            double currentAngle = bar.Angle;
            double targetAngle = e.NewValue / bar.Maximum * 359.999;

            DoubleAnimation anim = new DoubleAnimation(currentAngle, targetAngle, TimeSpan.FromMilliseconds(300));
            bar.BeginAnimation(CircularProgressBar.AngleProperty, anim, HandoffBehavior.SnapshotAndReplace);
        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(CircularProgressBar), new PropertyMetadata(0.0));

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(CircularProgressBar), new PropertyMetadata(10.0));

        public double EllipseStrokeThickness
        {
            get { return (double)GetValue(EllipseStrokeThicknessProperty); }
            set { SetValue(EllipseStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty EllipseStrokeThicknessProperty =
            DependencyProperty.Register("EllipseStrokeThickness", typeof(double), typeof(CircularProgressBar), new PropertyMetadata(1.0));

        public SolidColorBrush EllipseStroke
        {
            get { return (SolidColorBrush)GetValue(EllipseStrokeProperty); }
            set { SetValue(EllipseStrokeProperty, value); }
        }

        public static readonly DependencyProperty EllipseStrokeProperty =
            DependencyProperty.Register("EllipseStroke", typeof(SolidColorBrush), typeof(CircularProgressBar), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        public SolidColorBrush EllipseFill
        {
            get { return (SolidColorBrush)GetValue(EllipseFillProperty); }
            set { SetValue(EllipseFillProperty, value); }
        }

        public static readonly DependencyProperty EllipseFillProperty =
            DependencyProperty.Register("EllipseFill", typeof(SolidColorBrush), typeof(CircularProgressBar), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));


        public SolidColorBrush FontForeground
        {
            get { return (SolidColorBrush)GetValue(FontForegroundProperty); }
            set { SetValue(FontForegroundProperty, value); }
        }

        public static readonly DependencyProperty FontForegroundProperty =
            DependencyProperty.Register("FontForeground", typeof(SolidColorBrush), typeof(CircularProgressBar), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        public PenLineCap PenStartCap
        {
            get { return (PenLineCap)GetValue(PenStartCapProperty); }
            set { SetValue(PenStartCapProperty, value); }
        }

        public static readonly DependencyProperty PenStartCapProperty =
            DependencyProperty.Register("PenStartCap", typeof(PenLineCap), typeof(CircularProgressBar), new PropertyMetadata(PenLineCap.Flat));

        public PenLineCap PenEndCap
        {
            get { return (PenLineCap)GetValue(PenEndCapProperty); }
            set { SetValue(PenEndCapProperty, value); }
        }

        public static readonly DependencyProperty PenEndCapProperty =
            DependencyProperty.Register("PenEndCap", typeof(PenLineCap), typeof(CircularProgressBar), new PropertyMetadata(PenLineCap.Flat));
    }




    public class ActualWidthToStartPoint : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Point((double)value / 2, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ActualWidthToSize : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double width = (double)value / 2;
            return new Size(width, width);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AngleToIsLargeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double angle = (double)value;

            return angle > 180;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AngleToPointConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double angle = (double)value;
            double radius = (parameter as System.Windows.Shapes.Ellipse).ActualWidth / 2;
            double piang = angle * Math.PI / 180;

            double px = Math.Sin(piang) * radius + radius;
            double py = -Math.Cos(piang) * radius + radius;

            return new Point(px, py);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

