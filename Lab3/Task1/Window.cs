using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Task1
{
    internal class Window : GameWindow
    {
        public Window( int width, int height, string title )
            : base( width, height, GraphicsMode.Default, title )
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
            DrawAxes( 10 );

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
        }


        private void DrawAxes( int marksCount )
        {
            GL.Color3( 0.0f, 0.0f, 0.0f );

            GL.Begin( PrimitiveType.Lines );
            // X axis
            GL.Vertex2( -1, 0 );
            GL.Vertex2( 1, 0 );

            // Y axis
            GL.Vertex2( 0, -1 );
            GL.Vertex2( 0, 1 );
            GL.End();

            GL.Begin( PrimitiveType.Triangles );
            // X axis arrow
            GL.Vertex2( 0.98, 0.02 );
            GL.Vertex2( 0.98, -0.02 );
            GL.Vertex2( 1, 0 );

            // Y axis arrow
            GL.Vertex2( -0.02, 0.98 );
            GL.Vertex2( 0.02, 0.98 );
            GL.Vertex2( 0, 1 );
            GL.End();

            double step = 1.0 / marksCount;

            GL.Begin( PrimitiveType.Lines );
            for ( double i = 0; i < 1.0 - step; i += step )
            {
                // X axis marks
                GL.Vertex2( i, 0.02 );
                GL.Vertex2( i, -0.02 );

                GL.Vertex2( -i, 0.02 );
                GL.Vertex2( -i, -0.02 );

                // Y axis marks
                GL.Vertex2( 0.02, i );
                GL.Vertex2( -0.02, i );

                GL.Vertex2( 0.02, -i );
                GL.Vertex2( -0.02, -i );
            }
            GL.End();
        }
    }
}
