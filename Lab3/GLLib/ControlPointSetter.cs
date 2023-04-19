using System.Globalization;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;

namespace GLLib
{
    /// <summary>
    /// Helper class for drawing complex shapes
    /// <br>Click right mouse button to set control point</br>
    /// <br>Hold left mouse button to drag point</br>
    /// <br>Press "S" key to save points coordinates to vertices.txt. 
    ///  Vertices will be exported in ready-to-insert code</br>
    /// </summary>
    public class ControlPointSetter
    {
        private const int _pointSize = 10;
        private readonly GameWindow _window;

        private Vector2[] _points = new Vector2[] { };
        private int? _draggedIndex = null;

        private float AspectRatio => (float) _window.Width / _window.Height;

        public ControlPointSetter( GameWindow window )
        {
            _window = window;

            _window.KeyDown += Window_KeyDown;
            _window.MouseDown += Window_MouseDown;
            _window.MouseUp += Window_MouseUp;
            _window.MouseMove += Window_MouseMove;
        }

        /// <summary>
        /// Place it in window's OnUpdateFrame method <b>before</b> swapping buffers and 
        /// <b>after</b> drawing other stuff to display points
        /// </summary>
        public void Display()
        {
            DrawControlPoints();
            ConnectControlPoints();
        }

        private void Window_KeyDown( object sender, KeyboardKeyEventArgs e )
        {
            switch ( e.Key )
            {
                case Key.S:
                    SavePointsToFile();
                    break;
            }
        }

        private void SavePointsToFile()
        {
            using ( StreamWriter sw = new StreamWriter( "Vertices.txt" ) )
            {
                foreach ( Vector2 point in _points )
                {
                    string x = point.X.ToString( CultureInfo.GetCultureInfo( "en-US" ) );
                    string y = point.Y.ToString( CultureInfo.GetCultureInfo( "en-US" ) );
                    sw.WriteLine( $"GL.Vertex2( {x}f, {y}f );" );
                }
            }
        }

        private void Window_MouseDown( object sender, MouseButtonEventArgs e )
        {
            float centerX = (float) _window.Width / 2;
            float centerY = (float) _window.Height / 2;

            float mouseX = ( e.X - centerX ) / centerX;
            float mouseY = -( e.Y - centerY ) / centerY;

            AdjustMouseCoordinates( ref mouseX, ref mouseY );

            switch ( e.Button )
            {
                case MouseButton.Left:
                    TryGrabPoint( mouseX, mouseY );
                    break;
                case MouseButton.Right:
                    AddPoint( mouseX, mouseY );
                    break;
            }
        }

        private void TryGrabPoint( float atX, float atY )
        {
            for ( int i = 0; i <= _points.Length - 1; i++ )
            {
                Vector2 point = _points[ i ];
                if ( IsMouseOverPoint( atX, atY, point ) )
                {
                    _draggedIndex = i;
                    break;
                }
            }
        }

        private bool IsMouseOverPoint( float mouseX, float mouseY, Vector2 point )
        {
            float pointWidthInViewPort = (float) _pointSize / _window.Width;
            float pointHeightInViewPort = (float) _pointSize / _window.Height;

            return point.X - pointWidthInViewPort < mouseX
                            && mouseX < point.X + pointWidthInViewPort
                            && point.Y - pointHeightInViewPort < mouseY
                            && mouseY < point.Y + pointHeightInViewPort;
        }

        private void AddPoint( float x, float y )
        {
            _points = _points.Append( new Vector2( x, y ) ).ToArray();
        }

        private void Window_MouseUp( object sender, MouseButtonEventArgs e )
        {
            _draggedIndex = null;
        }

        private void Window_MouseMove( object sender, MouseMoveEventArgs e )
        {
            if ( _draggedIndex != null )
            {
                Vector2 point = _points[ (int) _draggedIndex ];

                float deltaX = e.XDelta;
                float deltaY = e.YDelta;

                AdjustMouseCoordinates( ref deltaX, ref deltaY );

                // 2 is point speed coefficient
                // With lower values point will be slower than mouse
                // With higher values point will be faster than mouse
                point.X += deltaX / _window.Width * 2;
                point.Y -= deltaY / _window.Height * 2;

                _points[ (int) _draggedIndex ] = new Vector2( point.X, point.Y );
            }
        }

        private void DrawControlPoints()
        {
            GL.PointSize( _pointSize );
            GL.Begin( PrimitiveType.Points );
            for ( int i = 0; i <= _points.Length - 1; i++ )
            {
                GL.Color3( 1.0f, 0.0f, 0.0f );
                GL.Vertex2( _points[ i ].X, _points[ i ].Y );
            }
            GL.End();
        }

        private void ConnectControlPoints()
        {
            Vector2? prev = null;

            GL.PointSize( 1 );
            GL.Color3( 0.0f, 0.0f, 0.0f );
            GL.Begin( PrimitiveType.Points );
            foreach ( Vector2 p in _points )
            {
                if ( prev == null )
                {
                    GL.Vertex2( p.X, p.Y );
                }
                else
                {
                    for ( float t = 0; t <= 1; t += 0.0001f )
                    {
                        GL.Vertex2( Lerp( prev.Value.X, p.X, t ), Lerp( prev.Value.Y, p.Y, t ) );
                    }
                }
                prev = p;
            }
            GL.End();
        }

        /// <summary>
        /// Adjusts mouse coordinates considering window's aspect ratio
        /// </summary>
        private void AdjustMouseCoordinates( ref float mouseX, ref float mouseY )
        {
            float aspectRatio = AspectRatio;

            if ( aspectRatio >= 1.0 )
            {
                mouseX *= aspectRatio;
            }
            else
            {
                mouseY /= aspectRatio;
            }
        }

        static public float Lerp( float v, float u, float t )
        {
            return ( 1 - t ) * v + t * u;
        }
    }
}
