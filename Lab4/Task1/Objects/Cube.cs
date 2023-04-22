using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Task1.Objects
{
    internal enum CubeSide
    {
        NEGATIVE_X,
        POSITIVE_X,
        NEGATIVE_Y,
        POSITIVE_Y,
        NEGATIVE_Z,
        POSITIVE_Z,

        MIN_CUBE_SIDE_INDEX = NEGATIVE_X,
        MAX_CUBE_SIDE_INDEX = POSITIVE_Z,

    }

    internal class Cube
    {
        private static readonly float[,] Vertices = new float[ 8, 3 ]
        {
            {-1, -1, -1},	// 0
		    {+1, -1, -1},	// 1
		    {+1, +1, -1},	// 2
		    {-1, +1, -1},	// 3
		    {-1, -1, +1},	// 4
		    {+1, -1, +1},	// 5
		    {+1, +1, +1},	// 6
		    {-1, +1, +1},   // 7
        };
        private static readonly int[,] Faces = new int[ 6, 4 ]
        {
            {4, 7, 3, 0},	// грань x<0
		    {5, 1, 2, 6},	// грань x>0
		    {4, 0, 1, 5},	// грань y<0
		    {7, 6, 2, 3},	// грань y>0
		    {0, 3, 2, 1},	// грань z<0
		    {4, 5, 6, 7},	// грань z>0
        };

        private readonly float Size;

        private float[,] _sideColors = new float[ 6, 4 ];

        public Cube( float size )
        {
            Size = size;

            SetSideColor( CubeSide.NEGATIVE_X, 1, 1, 1, 1 );
            SetSideColor( CubeSide.POSITIVE_X, 1, 1, 1, 1 );
            SetSideColor( CubeSide.NEGATIVE_Y, 1, 1, 1, 1 );
            SetSideColor( CubeSide.POSITIVE_Y, 1, 1, 1, 1 );
            SetSideColor( CubeSide.NEGATIVE_Z, 1, 1, 1, 1 );
            SetSideColor( CubeSide.POSITIVE_Z, 1, 1, 1, 1 );
        }

        public void SetSideColor( CubeSide side, float r, float g, float b, float a )
        {
            if ( side < CubeSide.MIN_CUBE_SIDE_INDEX || side > CubeSide.MAX_CUBE_SIDE_INDEX )
            {
                throw new ArgumentException( "Invalid side", nameof( side ) );
            }

            _sideColors[ (int) side, 0 ] = r;
            _sideColors[ (int) side, 1 ] = g;
            _sideColors[ (int) side, 2 ] = b;
            _sideColors[ (int) side, 3 ] = a;
        }

        public bool CheckCollision( Vector3 obj )
        {
            if ( obj.X >= -1 * Size - 0.02f && obj.Y >= -1 * Size - 0.02f && obj.Z <= 1 * Size + 0.02f && 
                 obj.X <= 1 * Size + 0.02f && obj.Y <= 1 * Size + 0.02f && obj.Z >= -1 * Size - 0.02f )
            {
                return true;
            }

            return false;
        }

        public void Draw()
        {
            GL.PushMatrix();
            GL.Scale( Size, Size, Size );

            GL.Begin( PrimitiveType.Quads );
            for ( int face = 0; face < Faces.GetLength( 0 ); face++ )
            {
                GL.Color4(
                    _sideColors[ face, 0 ],
                    _sideColors[ face, 1 ],
                    _sideColors[ face, 2 ],
                    _sideColors[ face, 3 ] );

                for ( int i = 0; i < 4; i++ )
                {
                    GL.Vertex3(
                        Vertices[ Faces[ face, i ], 0 ],
                        Vertices[ Faces[ face, i ], 1 ],
                        Vertices[ Faces[ face, i ], 2 ] );
                }
            }
            GL.End();

            GL.PopMatrix();
        }
    }
}
