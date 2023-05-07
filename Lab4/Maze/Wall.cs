using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Maze
{
    internal class Wall
    {
        private float _x1;
        private float _z1;
        private float _x2;
        private float _z2;
        private float _y1;
        private float _y2;
        private Color4 _color;
        private int _texture;

        public Wall( float x1, float z1, float x2, float z2, float y1, float y2, Color4 color )
        {
            if ( x1 > x2 )
            {
                (x1, x2) = (x2, x1);
            }

            if ( z1 > z2 )
            {
                (z1, z2) = (z2, z1);
            }

            if ( y1 > y2 )
            {
                (y1, y2) = (y2, y1);
            }

            _x1 = x1;
            _z1 = z1;
            _x2 = x2;
            _z2 = z2;
            _y1 = y1;
            _y2 = y2;
            _color = color;

            LoadTexture();
        }

        public void Draw()
        {
            GL.PushMatrix();

            GL.Enable( EnableCap.Texture2D );
            GL.BindTexture( TextureTarget.Texture2D, _texture );

            GL.Enable( EnableCap.Blend );
            GL.BlendFunc( BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha );

            GL.Color4( _color );

            // Bottom
            GL.Begin( PrimitiveType.Quads );
            GL.TexCoord2( 0.0f, 0.0f );
            GL.Vertex3( _x2, _y1, _z1 );
            GL.TexCoord2( 1.0f, 0.0f );
            GL.Vertex3( _x2, _y1, _z2 );
            GL.TexCoord2( 1.0f, 1.0f );
            GL.Vertex3( _x1, _y1, _z2 );
            GL.TexCoord2( 0.0f, 1.0f );
            GL.Vertex3( _x1, _y1, _z1 );
            GL.End();

            // Left
            GL.Begin( PrimitiveType.Quads );
            GL.TexCoord2( 0.0f, 1.0f );
            GL.Vertex3( _x1, _y1, _z2 );
            GL.TexCoord2( 0.0f, 0.0f );
            GL.Vertex3( _x1, _y2, _z2 );
            GL.TexCoord2( 1.0f, 0.0f );
            GL.Vertex3( _x1, _y2, _z1 );
            GL.TexCoord2( 1.0f, 1.0f );
            GL.Vertex3( _x1, _y1, _z1 );
            GL.End();

            // Back
            GL.Begin( PrimitiveType.Quads );
            GL.TexCoord2( 1.0f, 1.0f );
            GL.Vertex3( _x2, _y1, _z2 );
            GL.TexCoord2( 1.0f, 0.0f );
            GL.Vertex3( _x2, _y2, _z2 );
            GL.TexCoord2( 0.0f, 0.0f );
            GL.Vertex3( _x1, _y2, _z2 );
            GL.TexCoord2( 0.0f, 1.0f );
            GL.Vertex3( _x1, _y1, _z2 );
            GL.End();

            // Right
            GL.Begin( PrimitiveType.Quads );
            GL.TexCoord2( 0.0f, 1.0f );
            GL.Vertex3( _x2, _y1, _z1 );
            GL.TexCoord2( 0.0f, 0.0f );
            GL.Vertex3( _x2, _y2, _z1 );
            GL.TexCoord2( 1.0f, 0.0f );
            GL.Vertex3( _x2, _y2, _z2 );
            GL.TexCoord2( 1.0f, 1.0f );
            GL.Vertex3( _x2, _y1, _z2 );
            GL.End();

            // Front
            GL.Begin( PrimitiveType.Quads );
            GL.TexCoord2( 0.0f, 1.0f );
            GL.Vertex3( _x1, _y1, _z1 );
            GL.TexCoord2( 0.0f, 0.0f );
            GL.Vertex3( _x1, _y2, _z1 );
            GL.TexCoord2( 1.0f, 0.0f );
            GL.Vertex3( _x2, _y2, _z1 );
            GL.TexCoord2( 1.0f, 1.0f );
            GL.Vertex3( _x2, _y1, _z1 );
            GL.End();

            // Top
            GL.Begin( PrimitiveType.Quads );
            GL.TexCoord2( 0.0f, 0.0f );
            GL.Vertex3( _x1, _y2, _z1 );
            GL.TexCoord2( 0.0f, 1.0f );
            GL.Vertex3( _x1, _y2, _z2 );
            GL.TexCoord2( 1.0f, 1.0f );
            GL.Vertex3( _x2, _y2, _z2 );
            GL.TexCoord2( 1.0f, 0.0f );
            GL.Vertex3( _x2, _y2, _z1 );
            GL.End();

            GL.Disable( EnableCap.Blend );
            GL.Disable( EnableCap.Texture2D );

            GL.PopMatrix();
        }

        public bool CheckCollision( Vector3 obj, float objWith, float objHeight )
        {
            if ( obj.X >= _x1 - objWith && obj.Y >= _y1 - objHeight && obj.Z <= _z2 + objWith &&
                 obj.X <= _x2 + objWith && obj.Y <= _y2 + objHeight && obj.Z >= _z1 - objWith )
            {
                return true;
            }

            return false;
        }

        private void LoadTexture()
        {
            Bitmap bitmap = new Bitmap( @"..\..\Textures\brick_wall.jpg" );

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
    }
}
