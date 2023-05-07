using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace Maze
{
    internal class Texture
    {
        private readonly int _texture;

        public Texture( string path )
        {
            if ( path == null )
            {
                throw new ArgumentNullException( nameof( path ), "Argument is null" );
            }

            Bitmap bitmap = new Bitmap( path );

            GL.Hint( HintTarget.PerspectiveCorrectionHint, HintMode.Nicest );

            _texture = GL.GenTexture();
            GL.BindTexture( TextureTarget.Texture2D, _texture );

            BitmapData data = bitmap.LockBits( new Rectangle( 0, 0, bitmap.Width, bitmap.Height ),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb );

            GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
               OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0 );
            bitmap.UnlockBits( data );

            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Nearest );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Nearest );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat );
        }

        public void Bind()
        {
            if ( _texture == 0 )
            {
                return;
            }

            GL.Enable( EnableCap.Texture2D );
            GL.BindTexture( TextureTarget.Texture2D, _texture );

            GL.Enable( EnableCap.Blend );
            GL.BlendFunc( BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha );
        }

        public void Unbind()
        {
            GL.Disable( EnableCap.Blend );
            GL.Disable( EnableCap.Texture2D );
        }
    }
}
