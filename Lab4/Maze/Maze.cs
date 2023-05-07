using OpenTK;

namespace Maze
{
    internal class Maze
    {
        private const int MazeRows = 17;
        private const int MazeCols = 17;

        private readonly Texture _wallTexture;
        private readonly Texture _floorTexture;
        private readonly Texture _ceilTexture;

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
        private readonly Wall[] _floor = new Wall[ MazeRows * MazeCols ];
        private readonly Wall[] _ceil = new Wall[ MazeRows * MazeCols ];

        public Maze()
        {
            _wallTexture = new Texture( @"..\..\Textures\bricks.jpg" );
            _floorTexture = new Texture( @"..\..\Textures\wood.jpg" );
            _ceilTexture = new Texture( @"..\..\Textures\wood.jpg" );

            InitMaze();
        }

        private void InitMaze()
        {
            int k = -1;

            for ( int i = 0; i < MazeRows; i++ )
            {
                for ( int j = 0; j < MazeCols; j++ )
                {
                    k++;

                    _ceil[ k ] = new Wall(
                        j - MazeRows,
                        i - MazeCols,
                        j - MazeRows + 1,
                        i - MazeCols + 1,
                        1.0f,
                        2.0f,
                        _ceilTexture );

                    _floor[ k ] = new Wall(
                        j - MazeRows,
                        i - MazeCols,
                        j - MazeRows + 1,
                        i - MazeCols + 1,
                        -1.0f,
                        0,
                        _floorTexture );

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
                        _wallTexture );
                }
            }
        }

        public virtual void Draw()
        {
            for ( int i = 0; i < MazeRows * MazeCols; i++ )
            {
                _floor[ i ].Draw();
                _ceil[ i ].Draw();

                if ( _walls[ i ] == null )
                {
                    continue;
                }

                _walls[ i ].Draw();
            }
        }

        public bool CheckCollision( Vector3 obj, float objWith, float objHeight )
        {

            for ( int i = 0; i < MazeRows * MazeCols; i++ )
            {
                if ( _walls[ i ]?.CheckCollision( obj, objWith, objHeight ) == true ||
                     _floor[ i ]?.CheckCollision( obj, objWith, objHeight ) == true ||
                     _ceil[ i ]?.CheckCollision( obj, objWith, objHeight ) == true )
                {
                    return true;
                }
            }

            return false;
        }
    }
}
