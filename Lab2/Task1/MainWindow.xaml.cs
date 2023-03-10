using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

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
        }

        private void Open_Click( object sender, RoutedEventArgs e )
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg;*,png)|*.jpg;*.png";

            if ( dialog.ShowDialog() == true )
            {
                string filePath = dialog.FileName;
                ImageView.Source = new BitmapImage( new Uri( filePath ) );
            }
        }
    }
}
