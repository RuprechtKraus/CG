using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;
using OpenTK.Graphics.OpenGL4;

namespace Toolkit.Textures;

[SupportedOSPlatform( "windows" )]
public static class TextureLoader
{
    public static Texture LoadTexture( string path )
    {
        if ( string.IsNullOrWhiteSpace( path ) )
        {
            throw new ArgumentException( $"File path is null or empty", nameof( path ) );
        }

        var bitmap = new Bitmap( path );

        GL.Hint( HintTarget.PerspectiveCorrectionHint, HintMode.Nicest );

        int texture = GL.GenTexture();
        GL.BindTexture( TextureTarget.Texture2D, texture );

        BitmapData data = bitmap.LockBits( new Rectangle( 0, 0, bitmap.Width, bitmap.Height ), ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb );

        GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
            OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0 );
        
        GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Linear );
        GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear );
        GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat );
        GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat );
        
        bitmap.UnlockBits( data );
        GL.BindTexture( TextureTarget.Texture2D, 0 );

        return new Texture( texture );
    }
}
