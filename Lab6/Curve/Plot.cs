using OpenTK.Graphics.OpenGL4;
using Toolkit.Shaders;

namespace Curve;

public class Plot
{
    private const int MarksCount = 10;

    private readonly ShaderProgram _program;

    private float[] _axes = Array.Empty<float>();
    private float[] _arrows = Array.Empty<float>();
    private float[] _marks = Array.Empty<float>();

    private int _axesBufferObject;
    private int _axesArrayObject;

    private int _arrowsBufferObject;
    private int _arrowsArrayObject;

    private int _marksBufferObject;
    private int _marksArrayObject;

    private bool _initialized;

    public Plot( ShaderProgram program )
    {
        _program = program;
    }

    public void Draw()
    {
        if ( !_initialized )
        {
            Initialize();
        }

        _program.Use();

        DrawAxes();
        DrawArrows();
        DrawMarks();

        _program.Disuse();
    }

    private void DrawAxes()
    {
        BindAxes();

        GL.EnableVertexAttribArray( 0 );
        GL.BindVertexArray( _axesArrayObject );

        GL.DrawArrays( PrimitiveType.Lines, 0, 4 );

        GL.BindVertexArray( 0 );
        GL.DisableVertexAttribArray( 0 );
    }

    private void DrawArrows()
    {
        BindArrows();

        GL.EnableVertexAttribArray( 0 );
        GL.BindVertexArray( _arrowsArrayObject );

        GL.DrawArrays( PrimitiveType.Triangles, 0, 6 );

        GL.BindVertexArray( 0 );
        GL.DisableVertexAttribArray( 0 );
    }

    private void DrawMarks()
    {
        BindMarks();

        GL.EnableVertexAttribArray( 0 );
        GL.BindVertexArray( _marksArrayObject );

        GL.DrawArrays( PrimitiveType.Lines, 0, 80 );

        GL.BindVertexArray( 0 );
        GL.DisableVertexAttribArray( 0 );
    }

    private void Initialize()
    {
        InitializeAxes();
        InitializeArrows();
        InitializeMarks();

        _initialized = true;
    }

    private void InitializeAxes()
    {
        _axes = new[]
        {
            -1.0f, 0.0f, 1.0f, 0.0f,
            0.0f, -1.0f, 0.0f, 1.0f
        };

        _axesArrayObject = GL.GenVertexArray();
        _axesBufferObject = GL.GenBuffer();
        BindAxes();
    }

    private void BindAxes()
    {
        GL.BindVertexArray( _axesArrayObject );
        LoadAxes();
        GL.VertexAttribPointer( 0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0 );
    }

    private void LoadAxes()
    {
        GL.BindBuffer( BufferTarget.ArrayBuffer, _axesBufferObject );
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _axes.Length * sizeof(float),
            _axes,
            BufferUsageHint.DynamicDraw );
    }

    private void InitializeArrows()
    {
        _arrows = new[]
        {
            0.98f, 0.02f,
            0.98f, -0.02f,
            1.0f, 0.0f,
            -0.02f, 0.98f,
            0.02f, 0.98f,
            0.0f, 1.0f
        };

        _arrowsArrayObject = GL.GenVertexArray();
        _arrowsBufferObject = GL.GenBuffer();
        BindArrows();
    }

    private void BindArrows()
    {
        GL.BindVertexArray( _arrowsArrayObject );
        LoadArrows();
        GL.VertexAttribPointer( 0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0 );
    }

    private void LoadArrows()
    {
        GL.BindBuffer( BufferTarget.ArrayBuffer, _arrowsBufferObject );
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _arrows.Length * sizeof(float),
            _arrows,
            BufferUsageHint.DynamicDraw );
    }

    private void InitializeMarks()
    {
        const float step = 1.0f / MarksCount;

        _marks = new float[ MarksCount * 4 * 2 * 2 ];

        int i = 0;
        for ( float x = 0; x < 1.0; x += step )
        {
            // X axis marks
            _marks[ i++ ] = x;
            _marks[ i++ ] = 0.02f;

            _marks[ i++ ] = x;
            _marks[ i++ ] = -0.02f;

            _marks[ i++ ] = -x;
            _marks[ i++ ] = 0.02f;

            _marks[ i++ ] = -x;
            _marks[ i++ ] = -0.02f;

            // Y axis marks
            _marks[ i++ ] = 0.02f;
            _marks[ i++ ] = x;

            _marks[ i++ ] = -0.02f;
            _marks[ i++ ] = x;

            _marks[ i++ ] = 0.02f;
            _marks[ i++ ] = -x;

            _marks[ i++ ] = -0.02f;
            _marks[ i++ ] = -x;
        }

        _marksArrayObject = GL.GenVertexArray();
        _marksBufferObject = GL.GenBuffer();
        BindMarks();
    }

    private void BindMarks()
    {
        GL.BindVertexArray( _marksArrayObject );
        LoadMarks();
        GL.VertexAttribPointer( 0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0 );
    }

    private void LoadMarks()
    {
        GL.BindBuffer( BufferTarget.ArrayBuffer, _marksBufferObject );
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _marks.Length * sizeof(float),
            _marks,
            BufferUsageHint.DynamicDraw );
    }
}
