using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ImagesMvp.App.Dragging;
using Microsoft.Win32;

namespace Task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ImageDraggingManager _imageDraggingManager = new();

        public MainWindow()
        {
            InitializeComponent();
            _imageDraggingManager.EnableDrag( ImageView );
        }

        private void Open_Click( object sender, RoutedEventArgs e )
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg;*,png)|*.jpg;*.png";

            if ( dialog.ShowDialog() == true )
            {
                string filePath = dialog.FileName;
                ImageView.Source = new BitmapImage( new Uri( filePath ) );
                ImageView.Stretch = System.Windows.Media.Stretch.None;
                Canvas.SetLeft( ImageView, 0 );
                Canvas.SetTop( ImageView, 0 );
            }
        }
    }
}
