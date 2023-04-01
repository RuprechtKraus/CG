using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Task2
{
    internal class Window : GameWindow
    {
        public Window( int width, int height, string title )
            : base ( width, height, GraphicsMode.Default, title )
        { 
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
            SetupProjectionMatrix( Width, Height );
            Context.SwapBuffers();
        }

        private void SetupProjectionMatrix( int width, int height )
        {
            GL.MatrixMode( MatrixMode.Projection );
            GL.LoadIdentity();

            double aspectRatio = (double) width / height;

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
