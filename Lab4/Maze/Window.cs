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
            GL.Enable( EnableCap.CullFace );
            GL.CullFace( CullFaceMode.Back );
            GL.FrontFace( FrontFaceDirection.Cw );
            GL.Enable( EnableCap.DepthTest );
        }

        protected override void OnUpdateFrame( FrameEventArgs e )
        {
            base.OnUpdateFrame( e );

            if ( !Focused )
            {
                return;
            }

            const float cameraSpeed = 0.02f;
            const float sensitivity = 0.2f;

            var input = Keyboard.GetState( 1 );

            if ( input.IsKeyDown( Key.W ) )
            {
                _camera.Position += _camera.Front * cameraSpeed; // Forward
            }
            if ( input.IsKeyDown( Key.S ) )
            {
                _camera.Position -= _camera.Front * cameraSpeed; // Backwards
            }
            if ( input.IsKeyDown( Key.A ) )
            {
                _camera.Position -= _camera.Right * cameraSpeed; // Left
            }
            if ( input.IsKeyDown( Key.D ) )
            {
                _camera.Position += _camera.Right * cameraSpeed; // Right
            }
            if ( input.IsKeyDown( Key.Space ) )
            {
                _camera.Position += _camera.Up * cameraSpeed; // Up
            }
            if ( input.IsKeyDown( Key.LShift ) )
            {
                _camera.Position -= _camera.Up * cameraSpeed; // Down
            }

            Console.WriteLine( _camera.Position.Z );
            
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
