using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GLLib
{
    /// <summary>
    /// Provides functionality for simple drawing in GLConext
    /// </summary>
    public static class Drawing
    {
        public static void DrawCircle( float centerX, float centerY, float radius, float lineWidth )
        {
            const float step = (float) Math.PI / 180;

            GL.Begin( PrimitiveType.QuadStrip );
            for ( float angle = 0; angle < 2 * Math.PI; )
            {
                GL.Vertex2(
                    radius * (float) Math.Cos( angle ) + centerX,
                    radius * (float) Math.Sin( angle ) + centerY );

                angle += step;

                GL.Vertex2(
                    ( radius + lineWidth ) * (float) Math.Cos( angle ) + centerX,
                    ( radius + lineWidth ) * (float) Math.Sin( angle ) + centerY );

                angle += step;
            }
            GL.End();
        }

        public static void DrawFilledCircle( float centerX, float centerY, float radius )
        {
            DrawFilledEllipse( centerX, centerY, radius, radius );
        }

        public static void DrawFilledEllipse( float centerX, float centerY, float width, float height )
        {
            const float step = (float) Math.PI / 180;

            GL.Begin( PrimitiveType.TriangleFan );
            for ( float angle = 0; angle < 2 * Math.PI; angle += step )
            {
                GL.Vertex2(
                    width * (float) Math.Cos( angle ) + centerX,
                    height * (float) Math.Sin( angle ) + centerY );
            }
            GL.End();
        }

        public static void DrawFilledBezierCurve( params Vector2[] points )
        {
            BezierCurve bezierCurve = new BezierCurve( points );

            GL.Begin( PrimitiveType.TriangleFan );
            for ( float i = 0; i <= 1; i += 0.01f )
            {
                Vector2 point = bezierCurve.CalculatePoint( i );
                GL.Vertex2( point.X, point.Y );
            }
            GL.End();
        }
    }
}
