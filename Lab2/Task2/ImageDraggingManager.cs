using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImagesMvp.App.Dragging
{
    public class ImageDraggingManager
    {
        private Point? _dragStart = null;
        private Canvas? _canvas = null;
        private Image? _image = null;

        public void EnableDrag( Image image )
        {
            if ( image != null )
            {
                image.MouseDown += OnMouseDown;
                image.MouseUp += OnMouseUp;
                image.MouseMove += OnMouseMove;
            }
        }

        private void OnMouseDown( object sender, MouseButtonEventArgs e )
        {
            _image = (Image) sender;
            _image.Cursor = Cursors.SizeAll;
            _canvas = (Canvas) _image.Parent;
            _dragStart = e.GetPosition( _image );
            _image.CaptureMouse();
        }

        private void OnMouseUp( object sender, MouseButtonEventArgs e )
        {
            if ( _image != null )
            {
                _image.Cursor = Cursors.Arrow;
                _dragStart = null;
                _canvas = null;
                _image.ReleaseMouseCapture();
                _image = null;
            }
        }

        private void OnMouseMove( object sender, MouseEventArgs e )
        {
            if ( _dragStart != null && e.LeftButton == MouseButtonState.Pressed )
            {
                Point pos = e.GetPosition( _image );
                double dX = pos.X - _dragStart.Value.X;
                double dY = pos.Y - _dragStart.Value.Y;
                double newX = Canvas.GetLeft( _image ) + dX;
                double newY = Canvas.GetTop( _image ) + dY;
                Canvas.SetLeft( _image, newX );
                Canvas.SetTop( _image, newY );
            }
        }
    }
}
