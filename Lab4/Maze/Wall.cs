using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Maze
{
    internal class Wall
    {
        private const float MinDistanceToWall = 0.02f;
        private float _x1;
        private float _z1;
        private float _x2;
        private float _z2;
        private float _height;
        private Color4 _color;
        private int _texture;

        public Wall( float x1, float z1, float x2, float z2, float height, Color4 color )
        {
            if ( x1 > x2 )
            {
                (x1, x2) = (x2, x1);
            }

            if ( z1 > z2 )
            {
                (z1, z2) = (z2, z1);
            }

            _x1 = x1;
            _z1 = z1;
            _x2 = x2;
            _z2 = z2;
            _height = height;
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
            GL.Vertex3( _x2, 0, _z1 );
            GL.TexCoord2( 1.0f, 0.0f );
            GL.Vertex3( _x2, 0, _z2 );
            GL.TexCoord2( 1.0f, 1.0f );
            GL.Vertex3( _x1, 0, _z2 );
            GL.TexCoord2( 0.0f, 1.0f );
            GL.Vertex3( _x1, 0, _z1 );
            GL.End();

            // Left
            GL.Begin( PrimitiveType.Quads );
            GL.TexCoord2( 0.0f, 1.0f );
            GL.Vertex3( _x1, 0, _z2 );
            GL.TexCoord2( 0.0f, 0.0f );
            GL.Vertex3( _x1, _height, _z2 );
            GL.TexCoord2( 1.0f, 0.0f );
            GL.Vertex3( _x1, _height, _z1 );
            GL.TexCoord2( 1.0f, 1.0f );
            GL.Vertex3( _x1, 0, _z1 );
            GL.End();

            // Back
            GL.Begin( PrimitiveType.Quads );
            GL.TexCoord2( 1.0f, 1.0f );
            GL.Vertex3( _x2, 0, _z2 );
            GL.TexCoord2( 1.0f, 0.0f );
            GL.Vertex3( _x2, _height, _z2 );
            GL.TexCoord2( 0.0f, 0.0f );
            GL.Vertex3( _x1, _height, _z2 );
            GL.TexCoord2( 0.0f, 1.0f );
            GL.Vertex3( _x1, 0, _z2 );
            GL.End();

            // Right
            GL.Begin( PrimitiveType.Quads );
            GL.TexCoord2( 0.0f, 1.0f );
            GL.Vertex3( _x2, 0, _z1 );
            GL.TexCoord2( 0.0f, 0.0f );
            GL.Vertex3( _x2, _height, _z1 );
            GL.TexCoord2( 1.0f, 0.0f );
            GL.Vertex3( _x2, _height, _z2 );
            GL.TexCoord2( 1.0f, 1.0f );
            GL.Vertex3( _x2, 0, _z2 );
            GL.End();

            // Front
            GL.Begin( PrimitiveType.Quads );
            GL.TexCoord2( 0.0f, 1.0f );
            GL.Vertex3( _x1, 0, _z1 );
            GL.TexCoord2( 0.0f, 0.0f );
            GL.Vertex3( _x1, _height, _z1 );
            GL.TexCoord2( 1.0f, 0.0f );
            GL.Vertex3( _x2, _height, _z1 );
            GL.TexCoord2( 1.0f, 1.0f );
            GL.Vertex3( _x2, 0, _z1 );
            GL.End();

            // Top
            GL.Begin( PrimitiveType.Quads );
            GL.TexCoord2( 0.0f, 0.0f );
            GL.Vertex3( _x1, _height, _z1 );
            GL.TexCoord2( 0.0f, 1.0f );
            GL.Vertex3( _x1, _height, _z2 );
            GL.TexCoord2( 1.0f, 1.0f );
            GL.Vertex3( _x2, _height, _z2 );
            GL.TexCoord2( 1.0f, 0.0f );
            GL.Vertex3( _x2, _height, _z1 );
            GL.End();

            GL.Disable( EnableCap.Blend );
            GL.Disable( EnableCap.Texture2D );

            GL.PopMatrix();
        }

        public bool CheckCollision( Vector3 obj )
        {
            if ( obj.X >= _x1 - MinDistanceToWall && obj.Y >= 0 - MinDistanceToWall && obj.Z <= _z2 + MinDistanceToWall &&
                 obj.X <= _x2 + MinDistanceToWall && obj.Y <= _height + MinDistanceToWall && obj.Z >= _z1 - MinDistanceToWall )
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
