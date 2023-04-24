using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Maze
{
    internal class Wall
    {
        private Vector3 _v1;
        private Vector3 _v2;
        private Vector3 _v3;
        private Vector3 _v4;
        private Color4 _color;

        public Wall( Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Color4 color )
        {
            _v1 = v1;
            _v2 = v2;
            _v3 = v3;
            _v4 = v4;
            _color = color;
        }

        public void Draw()
        {
            GL.PushMatrix();

            GL.Color4( _color );
            GL.Begin( PrimitiveType.Quads );
            GL.Vertex3( _v1 );
            GL.Vertex3( _v2 );
            GL.Vertex3( _v3 );
            GL.Vertex3( _v4 );
            GL.End();

            GL.PopMatrix();
        }
    }
}
