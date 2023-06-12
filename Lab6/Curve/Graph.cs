using OpenTK.Graphics.OpenGL4;
using Toolkit.Shaders;

namespace Curve;

public class Graph
{
    private readonly ShaderProgram _program;
    private readonly int _animationDurationLocation;
    private readonly int _timeLocation;

    private float[] _vertices = Array.Empty<float>();
    private int _vertexBufferObject;
    private int _vertexArrayObject;
    private bool _initialized;

    public float AnimationDurationInSeconds { get; set; } = 1.0f;

    public Graph( ShaderProgram program, int timeLocation, int animationDurationLocation )
    {
        _program = program;
        _timeLocation = timeLocation;
        _animationDurationLocation = animationDurationLocation;
    }
    
    public void Draw( float time )
    {
        if ( !_initialized )
        {
            InitializeVertices();
            InitializeVertexBufferObject();
            _initialized = true;
        }
        
        _program.Use();
        
        GL.EnableVertexAttribArray( 0 );
        GL.BindVertexArray( _vertexArrayObject );
        
        GL.Uniform1( _animationDurationLocation, AnimationDurationInSeconds );
        
        time = time <= AnimationDurationInSeconds 
            ? time 
            : AnimationDurationInSeconds;
        GL.Uniform1( _timeLocation, time );
        
        GL.DrawArrays( PrimitiveType.LineStrip, 0, 501 );

        GL.BindVertexArray( 0 );
        GL.DisableVertexAttribArray( 0 );
        
        _program.Disuse();
    }

    private void InitializeVertexBufferObject()
    {
        _vertexArrayObject = GL.GenVertexArray();
        _vertexBufferObject = GL.GenBuffer();

        GL.BindVertexArray( _vertexArrayObject );

        GL.BindBuffer( BufferTarget.ArrayBuffer, _vertexBufferObject );
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _vertices.Length * sizeof(float),
            _vertices,
            BufferUsageHint.StreamDraw );
        
        GL.VertexAttribPointer( 0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0 );
    }

    private void InitializeVertices()
    {
        const int count = 500;
        const int dimension = 2;
        const float step = 2.0f / count;
        _vertices = new float[ count * dimension + 1 ];

        float x = -1.0f;
        for ( int i = 0; x < 1.0f; i += 2 )
        {
            _vertices[ i ] = x;
            x += step;
        }

        _vertices[ count * dimension ] = 1.0f;
    }
}
