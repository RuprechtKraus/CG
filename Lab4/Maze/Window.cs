using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Maze
{
    internal class Window : GameWindow
    {
        private readonly Wall _wall;

        public float AspectRatio => (float) Width / Height;

        private Camera _camera;

        public Window( int width, int height, string title )
            : base( width, height, GraphicsMode.Default, title )
        {
            _wall = new Wall(
                new Vector3( -1, 0, 0 ),
                new Vector3( -1, 1, 0 ),
                new Vector3( 1, 1, 0 ),
                new Vector3( 1, 0, 0 ),
                new Color4( 0.0f, 1.0f, 0.0f, 1.0f ) );
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            _camera = new Camera( Vector3.UnitZ, Width / Height );
            MouseMove += Window_MouseMove;
        }

        private const float CameraSpeed = 0.02f;
        private const float Sensitivity = 0.2f;

        private void Window_MouseMove( object sender, MouseMoveEventArgs e )
        {
            if ( e.Mouse.LeftButton == ButtonState.Pressed )
            {
                _camera.Yaw += e.XDelta * Sensitivity;
                _camera.Pitch -= e.YDelta * Sensitivity;
            }
        }

        protected override void OnUpdateFrame( FrameEventArgs e )
        {
            base.OnUpdateFrame( e );

            if ( !Focused )
            {
                return;
            }            

            var input = Keyboard.GetState( 1 );

            if ( input.IsKeyDown( Key.W ) )
            {
                _camera.Position += _camera.Front * CameraSpeed; // Forward
            }
            if ( input.IsKeyDown( Key.S ) )
            {
                _camera.Position -= _camera.Front * CameraSpeed; // Backwards
            }
            if ( input.IsKeyDown( Key.A ) )
            {
                _camera.Position -= _camera.Right * CameraSpeed; // Left
            }
            if ( input.IsKeyDown( Key.D ) )
            {
                _camera.Position += _camera.Right * CameraSpeed; // Right
            }
            if ( input.IsKeyDown( Key.Space ) )
            {
                _camera.Position += _camera.Up * CameraSpeed; // Up
            }
            if ( input.IsKeyDown( Key.LShift ) )
            {
                _camera.Position -= _camera.Up * CameraSpeed; // Down
            }

            DrawFrame();
        }

        protected override void OnResize( EventArgs e )
        {
            base.OnResize( e );

            GL.Viewport( 0, 0, Width, Height );
            GL.MatrixMode( MatrixMode.Projection );
            Matrix4 projection = _camera.GetProjectionMatrix();
            GL.LoadMatrix( ref projection );
            GL.MatrixMode( MatrixMode.Modelview );

            DrawFrame();
        }

        protected virtual void DrawFrame()
        {
            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );

            Matrix4 view = _camera.GetViewMatrix();
            GL.LoadMatrix( ref view );
            _wall.Draw();

            SwapBuffers();
        }
    }
}
