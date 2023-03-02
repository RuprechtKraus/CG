using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Task1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeElements();
            AnimateElements();
        }

        private void InitializeElements()
        {
            FirstLetter.RenderTransform = new TranslateTransform();
            SecondLetter.RenderTransform = new TranslateTransform();
            ThirdLetter.RenderTransform = new TranslateTransform();
        }

        private void AnimateElements()
        {
            Storyboard sb = new();
            AddJumpAnimation( FirstLetter, sb, 0, 0.5, 30 );
            AddJumpAnimation( SecondLetter, sb, 0.3, 0.5, 30 );
            AddJumpAnimation( ThirdLetter, sb, 0.6, 0.5, 30 );
            sb.Begin();
        }

        private static void AddJumpAnimation(
            FrameworkElement element,
            Storyboard storyboard,
            double startDelay,
            double duration,
            double jumpHeight )
        {
            DoubleAnimation jumpAnimation = new()
            {
                From = element.Margin.Top,
                To = element.Margin.Top - jumpHeight,
                Duration = TimeSpan.FromSeconds( duration ),
                BeginTime = TimeSpan.FromSeconds( startDelay ),
                RepeatBehavior = RepeatBehavior.Forever,
                DecelerationRatio = 1,
                AutoReverse = true
            };

            Storyboard.SetTarget( jumpAnimation, element );
            Storyboard.SetTargetProperty(
                jumpAnimation,
                new PropertyPath( "RenderTransform.(TranslateTransform.Y)" ) );

            storyboard.Children.Add( jumpAnimation );
        }
    }
}
