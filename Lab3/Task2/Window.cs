using System;
using GLLib;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Task2
{
    internal class Window : GameWindow
    {
        private readonly ControlPointSetter _controlPointSetter;

        private float AspectRatio => (float) Width / Height;
        private IDrawable _character = new Pin();

        public Window( int width, int height, string title )
            : base( width, height, GraphicsMode.Default, title )
        {
            _controlPointSetter = new ControlPointSetter( this );
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            GL.ClearColor( 1.0f, 1.0f, 1.0f, 1.0f );
        }

        protected override void OnUpdateFrame( FrameEventArgs e )
        {
            base.OnUpdateFrame( e );

            DrawFrame();
        }

        protected override void OnResize( EventArgs e )
        {
            base.OnResize( e );

            GL.Viewport( 0, 0, Width, Height );
            DrawFrame();
        }

        private void DrawFrame()
        {
            GL.Clear( ClearBufferMask.ColorBufferBit );

            SetupProjectionMatrix();
            _character.Draw();
            _controlPointSetter.Display();

            Context.SwapBuffers();
        }

        private void SetupProjectionMatrix()
        {
            GL.MatrixMode( MatrixMode.Projection );
            GL.LoadIdentity();

            double aspectRatio = AspectRatio;

            if ( aspectRatio > 1.0 )
            {
                GL.Ortho( -1.0 * aspectRatio, 1.0 * aspectRatio, -1.0, 1.0, -1.0, 1.0 );
            }
            else
            {
                GL.Ortho( -1.0, 1.0, -1.0 / aspectRatio, 1.0 / aspectRatio, -1.0, 1.0 );
            }

            GL.MatrixMode( MatrixMode.Modelview );
        }
    }
}
