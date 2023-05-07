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
        private Texture _texture;

        public Wall( float x1, float z1, float x2, float z2, float y1, float y2, Texture texture )
        {
            _x1 = x1;
            _z1 = z1;
            _x2 = x2;
            _z2 = z2;
            _y1 = y1;
            _y2 = y2;
            _texture = texture;

            NormalizeCoordinates();
        }

        private void NormalizeCoordinates()
        {
            if ( _x1 > _x2 )
            {
                (_x1, _x2) = (_x2, _x1);
            }

            if ( _z1 > _z2 )
            {
                (_z1, _z2) = (_z2, _z1);
            }

            if ( _y1 > _y2 )
            {
                (_y1, _y2) = (_y2, _y1);
            }
        }

        public void Draw()
        {
            GL.PushMatrix();

            GL.Color4( Color4.White );
            _texture.Bind();

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

            _texture.Unbind();
            GL.PopMatrix();
        }

        public bool CheckCollision( Vector3 obj, float objWith, float objHeight )
        {
            if ( obj.X >= _x1 - objWith && obj.Y > _y1 - 0.05f && obj.Z <= _z2 + objWith &&
                 obj.X <= _x2 + objWith && obj.Y < _y2 + objHeight && obj.Z >= _z1 - objWith )
            {
                return true;
            }

            return false;
        }
    }
}
