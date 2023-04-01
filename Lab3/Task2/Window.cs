using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

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
            const float step = (float) Math.PI / 180;

            GL.Color3( 0.376, 0.369, 0.361 );

            GL.Begin( PrimitiveType.QuadStrip );
            for ( float angle = 0; angle < 2 * Math.PI; )
            {
                GL.Vertex2(
                    0.6f * (float) Math.Cos( angle ),
                    0.6f * (float) Math.Sin( angle ) + 0.05 );

                angle += step;

                GL.Vertex2(
                    0.55f * (float) Math.Cos( angle ),
                    0.55f * (float) Math.Sin( angle ) + 0.05 );

                angle += step;
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
