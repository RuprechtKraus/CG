using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Task2
{
    internal class Window : GameWindow
    {
        private int _image;

        public Window( int width, int height, string title )
            : base( width, height, GraphicsMode.Default, title )
        {
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            _image = LoadPinImage();

            GL.ClearColor( 1.0f, 1.0f, 1.0f, 1.0f );

            MouseDown += Window_MouseDown;
            WindowState = WindowState.Maximized;
        }

        private void Window_MouseDown( object sender, MouseButtonEventArgs e )
        {
            float centerX = (float) Width / 2;
            float centerY = (float) Height / 2;
            Console.WriteLine(
                $"X: {( e.X - centerX ) / centerX} " +
                $"Y: {-( e.Y - centerY ) / centerY}" );
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
            DrawImage( _image );
            DrawPin();

            Context.SwapBuffers();
        }

        private static void SetupProjectionMatrix( int width, int height )
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

        private static void DrawPin()
        {
            DrawBody();
        }

        private static void DrawBody()
        {
            GL.Color3( 0.0f, 0.0f, 0.0f );
            DrawFilledCircle( 0.0f, 0.05f, 0.55f );

            GL.Color3( 0.376, 0.369, 0.361 );
            DrawCircle( 0.0f, 0.05f, 0.55f, 0.05f );

            GL.Color3( 0.376, 0.369, 0.361 );
            DrawFilledBezierCurve(
                new Vector2( -0.415f, -0.37f ),
                new Vector2( -0.3f, -0.15f ),
                new Vector2( 0, 0.08f ),
                new Vector2( 0.3f, -0.15f ),
                new Vector2( 0.415f, -0.37f ) );

            DrawFilledBezierCurve(
                new Vector2( -0.415f, -0.37f ),
                new Vector2( 0, -0.7f ),
                new Vector2( 0.415f, -0.37f ) );

            GL.Color3( 0.82f, 0.808f, 0.812f );
            DrawFilledBezierCurve(
                new Vector2( -0.36f, -0.36f ),
                new Vector2( -0.27f, -0.17f ),
                new Vector2( 0, -0.02f ),
                new Vector2( 0.27f, -0.17f ),
                new Vector2( 0.36f, -0.36f ) );

            DrawFilledBezierCurve(
                new Vector2( -0.36f, -0.36f ),
                new Vector2( 0, -0.65f ),
                new Vector2( 0.36f, -0.36f ) );

            GL.Color3( 1.0f, 1.0f, 1.0f );
            DrawFilledBezierCurve(
                new Vector2( -0.325f, -0.305f ),
                new Vector2( -0.22f, -0.145f ),
                new Vector2( 0.05f, -0.05f ),
                new Vector2( 0.246f, -0.16f ),
                new Vector2( 0.36f, -0.36f ) );

            DrawFilledBezierCurve(
                new Vector2( -0.325f, -0.305f ),
                new Vector2( -0.023f, -0.55f ),
                new Vector2( 0.36f, -0.36f ) );
        }

        private static void DrawCircle( float centerX, float centerY, float radius, float width )
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
                    ( radius + width ) * (float) Math.Cos( angle ) + centerX,
                    ( radius + width ) * (float) Math.Sin( angle ) + centerY );

                angle += step;
            }
            GL.End();
        }

        private static void DrawFilledCircle( float centerX, float centerY, float radius )
        {
            const float step = (float) Math.PI / 180;

            GL.Begin( PrimitiveType.TriangleFan );
            for ( float angle = 0; angle < 2 * Math.PI; angle += step )
            {
                GL.Vertex2(
                    radius * (float) Math.Cos( angle ) + centerX,
                    radius * (float) Math.Sin( angle ) + centerY
                    );
            }
            GL.End();
        }

        private static void DrawFilledBezierCurve( params Vector2[] points )
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

        // TODO: Delete after drawing actual character
        private int LoadPinImage()
        {
            Bitmap bitmap = new Bitmap( @"C:\Users\zombi\Downloads\Pin.jpg" );

            int tex;
            GL.Hint( HintTarget.PerspectiveCorrectionHint, HintMode.Nicest );

            GL.GenTextures( 1, out tex );
            GL.BindTexture( TextureTarget.Texture2D, tex );

            BitmapData data = bitmap.LockBits( new Rectangle( 0, 0, bitmap.Width, bitmap.Height ),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb );

            GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0 );
            bitmap.UnlockBits( data );


            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Linear );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat );

            return tex;
        }

        public static void DrawImage( int image )
        {
            GL.Enable( EnableCap.Texture2D );
            GL.BindTexture( TextureTarget.Texture2D, image );

            GL.Enable( EnableCap.Blend );
            GL.BlendFunc( BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha );

            GL.Color4( 1.0, 1.0, 1.0, 0.2 );

            GL.Begin( PrimitiveType.Quads );

            GL.TexCoord2( 0, 0 );
            GL.Vertex2( -1, 1 );

            GL.TexCoord2( 1, 0 );
            GL.Vertex2( 1, 1 );

            GL.TexCoord2( 1, 1 );
            GL.Vertex2( 1, -1 );

            GL.TexCoord2( 0, 1 );
            GL.Vertex2( -1, -1 );

            GL.End();

            GL.Disable( EnableCap.Blend );

            GL.Disable( EnableCap.Texture2D );
        }
    }
}
