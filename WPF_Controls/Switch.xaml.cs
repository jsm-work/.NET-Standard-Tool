using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WPF_Controls
{
    /// <summary>
    /// Switch.xaml에 대한 상호 작용 논리
    /// </summary>

    public partial class Switch : UserControl
    {
        public static readonly DependencyProperty TempStringProperty = DependencyProperty.Register("TempString", typeof(string), typeof(Switch));
        public string TempString
        {
            get { return (string)GetValue(TempStringProperty); }
            set { SetValue(TempStringProperty, value); }
        }

        public static readonly DependencyProperty AnimationSpeedProperty = DependencyProperty.Register("AnimationSpeed", typeof(int), typeof(Switch));
        public int AnimationSpeed
        {
            get { return (int)GetValue(AnimationSpeedProperty); }
            set { SetValue(AnimationSpeedProperty, value); }
        }

        public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(Switch));
        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty IsCheckProperty = DependencyProperty.Register("IsCheck", typeof(bool), typeof(Switch));
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckProperty); }
            set
            {
                SetValue(IsCheckProperty, value);
                OnIsCheckedChanged();
            }
        }
        public event System.EventHandler IsCheckedChanged;
        protected virtual void OnIsCheckedChanged()
        {
            if (IsCheckedChanged != null) IsCheckedChanged(this, EventArgs.Empty);
        }


        public static readonly DependencyProperty EllipseOnBrushProperty = DependencyProperty.Register("EllipseOnBrush", typeof(SolidColorBrush), typeof(Switch));
        public SolidColorBrush EllipseOnBrush
        {
            get { return (SolidColorBrush)GetValue(EllipseOnBrushProperty); }
            set { SetValue(EllipseOnBrushProperty, value); }
        }

        public static readonly DependencyProperty EllipseOffBrushProperty = DependencyProperty.Register("EllipseOffBrush", typeof(SolidColorBrush), typeof(Switch));
        public SolidColorBrush EllipseOffBrush
        {
            get { return (SolidColorBrush)GetValue(EllipseOffBrushProperty); }
            set { SetValue(EllipseOffBrushProperty, value); }
        }

        public static readonly DependencyProperty OnBrushProperty = DependencyProperty.Register("OnBrush", typeof(SolidColorBrush), typeof(Switch));
        public SolidColorBrush OnBrush
        {
            get { return (SolidColorBrush)GetValue(OnBrushProperty); }
            set { SetValue(OnBrushProperty, value); }
        }

        public static readonly DependencyProperty OffBrushProperty = DependencyProperty.Register("OffBrush", typeof(SolidColorBrush), typeof(Switch));
        public SolidColorBrush OffBrush
        {
            get { return (SolidColorBrush)GetValue(OffBrushProperty); }
            set { SetValue(OffBrushProperty, value); }
        }


        public Switch()
        {
            InitializeComponent();

            AnimationSpeed = 1000;
        }

        private void Switch_IsCheckedChanged(object sender, EventArgs e)
        {
            if (!IsChecked)
            {
                UIElementMove(Ell, AnimationSpeed, this.ActualWidth - Ell.Width - BorderThickness.Left - BorderThickness.Right, 0, TranslateTransform.XProperty);
                border.Background = OffBrush;
            }
            else
            {
                UIElementMove(Ell, AnimationSpeed, 0, this.ActualWidth - Ell.Width - BorderThickness.Left - BorderThickness.Right, TranslateTransform.XProperty);
                border.Background = OnBrush;
            }
            Ell.Fill = IsChecked == true ? EllipseOnBrush : EllipseOffBrush;
        }

        private void UC_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            border.CornerRadius = new CornerRadius(this.ActualHeight / 2);
            Ell.Width = Ell.Height = this.ActualHeight - BorderThickness.Top - BorderThickness.Bottom;
            Ell.Margin = new Thickness(BorderThickness.Left,
                                        BorderThickness.Top,
                                        this.ActualWidth - Ell.Width - BorderThickness.Left,
                                        BorderThickness.Bottom);
            if (IsChecked)
            {
                UIElementMove(Ell, 0, 0, this.ActualWidth - Ell.Width - BorderThickness.Left - BorderThickness.Right, TranslateTransform.XProperty);
            }
            else
            {
                UIElementMove(Ell, 0, this.ActualWidth - Ell.Width - BorderThickness.Left - BorderThickness.Right, 0, TranslateTransform.XProperty);
            }
        }

        private void UC_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsChecked)
            {
                IsChecked = false;
            }
            else
            {
                IsChecked = true;
            }
            Ell.Fill = IsChecked == true ? EllipseOnBrush : EllipseOffBrush;
        }

        public void SetCheck()
        {
            if (IsChecked)
            {
                UIElementMove(Ell, AnimationSpeed, this.ActualWidth - Ell.Width - BorderThickness.Left - BorderThickness.Right, 0, TranslateTransform.XProperty);
                border.Background = OffBrush;
                IsChecked = false;
            }
            else
            {
                UIElementMove(Ell, AnimationSpeed, 0, this.ActualWidth - Ell.Width - BorderThickness.Left - BorderThickness.Right, TranslateTransform.XProperty);
                border.Background = OnBrush;
                IsChecked = true;
            }
            Ell.Fill = IsChecked == true ? EllipseOnBrush : EllipseOffBrush;
        }


        #region Animation
        /// <summary>
        /// 
        /// </summary>
        /// <param Name="obj">UIElement</param>
        /// <param Name="milliSeconds">milliSeconds</param>
        /// <param Name="startX"></param>
        /// <param Name="endX"></param>
        /// <param Name="dependencyProperty">TranslateTransform.XProperty / TranslateTransform.YProperty</param>
        /// <param Name="Accel">0~0.9</param>
        /// <param Name="Decel">0~0.9</param>
        public static void UIElementMove(UIElement obj, int milliSeconds, double startX, double endX, DependencyProperty dependencyProperty, double Accel = 0.9, double Decel = 0.1)
        {
            try
            {
                DoubleAnimation animation = new DoubleAnimation(startX, endX, new Duration(TimeSpan.FromMilliseconds(milliSeconds)));
                animation.AccelerationRatio = Accel;
                animation.DecelerationRatio = Decel;

                obj.RenderTransform = new TranslateTransform();
                obj.RenderTransform.BeginAnimation(dependencyProperty, animation);
            }
            catch (Exception ex)
            { }
        }
        #endregion

        private void UC_Loaded(object sender, RoutedEventArgs e)
        {
            IsCheckedChanged += Switch_IsCheckedChanged;

            if (EllipseOnBrush == null && EllipseOffBrush == null)
                EllipseOnBrush = EllipseOffBrush = Brushes.White;
            else if (EllipseOnBrush != null && EllipseOffBrush != null)
            { }
            else if (EllipseOnBrush == null)
                EllipseOnBrush = EllipseOffBrush;
            else if (EllipseOffBrush == null)
                EllipseOffBrush = EllipseOnBrush;

            Ell.Fill = IsChecked == true ? EllipseOnBrush : EllipseOffBrush;
            border.Background = IsChecked == true ? OnBrush : OffBrush;
        }
    }
}
