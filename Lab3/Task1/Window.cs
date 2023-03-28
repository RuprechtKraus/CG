using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Task1
{
    internal class Window : GameWindow
    {
        private const int _marksCount = 10;

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
            DrawAxes();
            DrawGraph();

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

        private void DrawAxes()
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

            double step = 1.0 / _marksCount;

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

        private void DrawGraph()
        {
            const double xMin = -2.0;
            const double xMax = 3.0;

            GL.Color3( 1.0, 0.0, 0.0 );

            GL.Begin( PrimitiveType.LineStrip );
            for ( double x = xMin; x <= xMax; x += 0.01 )
            {
                double y = ( 2 * x * x ) - ( 3 * x ) - 8;
                GL.Vertex2( x / _marksCount, y / _marksCount );
            }
            GL.End();
        }
    }
}
