using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Task1
{
    internal class Window : GameWindow
    {
        public Window( int width, int height, string title )
            : base( width, height, GraphicsMode.Default, title, GameWindowFlags.FixedWindow )
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

            GL.Clear( ClearBufferMask.ColorBufferBit );

            DrawAxes( 10 );

            Context.SwapBuffers();
        }

        protected override void OnResize( EventArgs e )
        {
            base.OnResize( e );

            GL.Viewport( 0, 0, Width, Height );
        }

        private void DrawAxes( int marksCount )
        {
            GL.Color4( 0.0f, 0.0f, 0.0f, 1.0f );

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
