using OpenTK;
using OpenTK.Graphics;

namespace Maze
{
    internal class Maze
    {
        private const int MazeRows = 17;
        private const int MazeCols = 17;

        private readonly char[,] _pattern = new char[ MazeRows, MazeCols ]
        {
            { '*','*','*','*','*','*','*',' ','*','*','*','*','*','*','*','*','*' },
            { '*',' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ',' ',' ','*',' ','*' },
            { '*',' ','*','*','*',' ','*',' ','*','*','*','*','*',' ','*',' ','*' },
            { '*',' ','*',' ',' ',' ','*',' ',' ',' ',' ',' ','*',' ','*',' ','*' },
            { '*','*','*',' ','*','*','*',' ','*','*','*','*','*',' ','*',' ','*' },
            { '*',' ',' ',' ','*',' ',' ',' ','*',' ',' ',' ','*',' ','*',' ','*' },
            { '*',' ','*',' ','*',' ','*','*','*','*','*',' ','*',' ','*',' ','*' },
            { '*',' ','*',' ','*',' ','*',' ','*',' ',' ',' ','*',' ','*',' ','*' },
            { '*',' ','*','*','*',' ','*',' ','*',' ','*','*','*',' ','*',' ','*' },
            { '*',' ',' ',' ',' ',' ',' ',' ','*',' ',' ',' ','*',' ','*',' ','*' },
            { '*',' ','*','*','*','*','*','*','*','*','*',' ','*',' ','*',' ','*' },
            { '*',' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ','*',' ','*',' ','*' },
            { '*','*','*','*','*',' ','*',' ','*','*','*','*','*',' ','*',' ','*' },
            { '*',' ','*',' ',' ',' ','*',' ',' ',' ',' ',' ',' ',' ','*',' ','*' },
            { '*',' ','*',' ','*','*','*','*','*','*','*','*','*','*','*',' ','*' },
            { '*',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','*' },
            { '*','*','*','*','*','*','*','*','*',' ','*','*','*','*','*','*','*' },
        };

        private readonly Wall[] _walls = new Wall[ MazeRows * MazeCols ];
        private readonly Wall _floor;
        private readonly Wall _ceil;

        public Maze()
        {
            _floor = new Wall( -MazeRows, -MazeCols, 0, 0, -0.5f, 0, Color4.Turquoise );
            InitWalls();
        }

        private void InitWalls()
        {
            int k = -1;

            for ( int i = 0; i < MazeRows; i++ )
            {
                for ( int j = 0; j < MazeCols; j++ )
                {
                    k++;

                    if ( _pattern[ i, j ] == ' ' )
                    {
                        continue;
                    }

                    _walls[ k ] = new Wall(
                        j - MazeRows,
                        i - MazeCols,
                        j - MazeRows + 1,
                        i - MazeCols + 1,
                        0,
                        1,
                        Color4.White );
                }
            }
        }

        public virtual void Draw()
        {
            _floor.Draw();

            for ( int i = 0; i < MazeRows * MazeCols; i++ )
            {
                if ( _walls[ i ] == null )
                {
                    continue;
                }

                _walls[ i ].Draw();
            }
        }

        public bool CheckCollision( Vector3 obj, float objWith, float objHeight )
        {
            if ( _floor.CheckCollision( obj, objWith, objHeight ) )
            {
                return true;
            }

            for ( int i = 0; i < MazeRows * MazeCols; i++ )
            {
                if ( _walls[ i ]?.CheckCollision( obj, objWith, objHeight ) == true )
                {
                    return true;
                }
            }

            return false;
        }
    }
}
