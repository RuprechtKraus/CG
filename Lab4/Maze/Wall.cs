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
        }

        public void Draw()
        {
            GL.PushMatrix();

            GL.Color4( _color );
            GL.Begin( PrimitiveType.Quads );
            // Bottom
            GL.Vertex3( _x2, 0, _z1 );
            GL.Vertex3( _x2, 0, _z2 );
            GL.Vertex3( _x1, 0, _z2 );
            GL.Vertex3( _x1, 0, _z1 );

            // Left
            GL.Vertex3( _x1, 0, _z1 );
            GL.Vertex3( _x1, 0, _z2 );
            GL.Vertex3( _x1, _height, _z2 );
            GL.Vertex3( _x1, _height, _z1 );

            // Back
            GL.Vertex3( _x2, 0, _z2 );
            GL.Vertex3( _x2, _height, _z2 );
            GL.Vertex3( _x1, _height, _z2 );
            GL.Vertex3( _x1, 0, _z2 );

            // Right
            GL.Vertex3( _x2, 0, _z1 );
            GL.Vertex3( _x2, _height, _z1 );
            GL.Vertex3( _x2, _height, _z2 );
            GL.Vertex3( _x2, 0, _z2 );

            // Front
            GL.Vertex3( _x1, 0, _z1 );
            GL.Vertex3( _x1, _height, _z1 );
            GL.Vertex3( _x2, _height, _z1 );
            GL.Vertex3( _x2, 0, _z1 );

            // Top
            GL.Vertex3( _x1, _height, _z1 );
            GL.Vertex3( _x1, _height, _z2 );
            GL.Vertex3( _x2, _height, _z2 );
            GL.Vertex3( _x2, _height, _z1 );
            GL.End();

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
    }
}
