using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Maze
{
    internal class Window : GameWindow
    {
        private const float CameraSpeed = 0.02f;
        private const float Sensitivity = 0.2f;
        private readonly Wall _wall;

        private Camera _camera;

        public float AspectRatio => (float) Width / Height;


        public Window( int width, int height, string title )
            : base( width, height, GraphicsMode.Default, title )
        {
            _wall = new Wall( -0.2f, 0.2f, 0.2f, -0.2f, 1.0f, new Color4( 1.0f, 0.0f, 0.0f, 1.0f ) );
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            GL.ClearColor( 0.0f, 0.0f, 0.0f, 1.0f );
            GL.Enable( EnableCap.CullFace );
            GL.CullFace( CullFaceMode.Back );
            GL.FrontFace( FrontFaceDirection.Cw );
            GL.Enable( EnableCap.DepthTest );

            _camera = new Camera( Vector3.UnitZ, Width / Height );
            MouseMove += Window_MouseMove;
        }

        protected override void OnUpdateFrame( FrameEventArgs e )
        {
            base.OnUpdateFrame( e );

            if ( !Focused )
            {
                return;
            }

            HandleKeyboardInput();

            DrawFrame();
        }

        protected override void OnResize( EventArgs e )
        {
            base.OnResize( e );

            _camera.AspectRatio = AspectRatio;
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

        private void Window_MouseMove( object sender, MouseMoveEventArgs e )
        {
            if ( e.Mouse.LeftButton == ButtonState.Pressed )
            {
                _camera.Yaw += e.XDelta * Sensitivity;
                _camera.Pitch -= e.YDelta * Sensitivity;
            }
        }

        private void HandleKeyboardInput()
        {
            KeyboardState input = Keyboard.GetState( 1 );

            if ( input.IsKeyDown( Key.W ) )
            {
                _camera.Position += _camera.Front * CameraSpeed;
            }

            if ( input.IsKeyDown( Key.S ) )
            {
                _camera.Position -= _camera.Front * CameraSpeed;
            }

            if ( input.IsKeyDown( Key.A ) )
            {
                _camera.Position -= _camera.Right * CameraSpeed;
            }

            if ( input.IsKeyDown( Key.D ) )
            {
                _camera.Position += _camera.Right * CameraSpeed;
            }

            if ( input.IsKeyDown( Key.Space ) )
            {
                _camera.Position += _camera.Up * CameraSpeed;
            }

            if ( input.IsKeyDown( Key.LShift ) )
            {
                _camera.Position -= _camera.Up * CameraSpeed;
            }
        }
    }
}
