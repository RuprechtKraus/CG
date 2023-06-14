using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Toolkit;
using Toolkit.Shaders;

namespace Flag;

public class Window : GameWindow
{
    private readonly float[] _vertices =
    {
        -1.0f, -1.0f,
        -1.0f, 1.0f,
        1.0f, 1.0f,
        1.0f, -1.0f
    };

    private readonly uint[] _indices =
    {
        0, 1, 2,
        0, 2, 3
    };

    private ShaderProgram _program = null!;

    private int _vertexBufferObject;
    private int _vertexArrayObject;
    private int _elementBufferObject;

    public Window( int width, int height, string title )
        : base( GameWindowSettings.Default, new NativeWindowSettings
        {
            Size = ( width, height ),
            Title = title
        } )
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor( Color.White );

        InitializeShaders();
        InitializeBuffers();
    }
    
    private void InitializeShaders()
    {
        Shader vertexShader = ShaderLoader.LoadShader(
            ShaderType.VertexShader,
            @"../../../Shaders/flag.vert" );
        Shader fragmentShader = ShaderLoader.LoadShader(
            ShaderType.FragmentShader,
            @"../../../Shaders/flag.frag" );

        ShaderCompiler shaderCompiler = new();
        shaderCompiler.CompileShader( vertexShader );
        shaderCompiler.CompileShader( fragmentShader );
        shaderCompiler.CheckStatus();

        _program = new ShaderProgram();
        _program.AttachShader( vertexShader );
        _program.AttachShader( fragmentShader );

        ShaderProgramLinker linker = new();
        linker.LinkProgram( _program );
        linker.CheckStatus();

        _program.DetachShader( vertexShader );
        _program.DetachShader( fragmentShader );

        vertexShader.Delete();
        fragmentShader.Delete();
    }

    private void InitializeBuffers()
    {
        _vertexBufferObject = GL.GenBuffer();
        _vertexArrayObject = GL.GenVertexArray();

        GL.BindVertexArray(_vertexArrayObject);

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _vertices.Length * sizeof(float),
            _vertices,
            BufferUsageHint.StaticDraw);

        _elementBufferObject = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
        GL.BufferData(
            BufferTarget.ElementArrayBuffer,
            _indices.Length * sizeof(uint),
            _indices,
            BufferUsageHint.StaticDraw);

        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
    }

    protected override void OnRenderFrame( FrameEventArgs args )
    {
        base.OnRenderFrame( args );

        DrawFrame();
    }

    protected override void OnResize( ResizeEventArgs e )
    {
        base.OnResize( e );

        GL.Viewport( e.Width / 4, e.Height / 4, e.Width / 2, e.Height / 2 );
        DrawFrame();
    }

    private void DrawFrame()
    {
        GL.Clear( ClearBufferMask.ColorBufferBit );

        int projectionLocation = _program.GetUniformLocation( "projection" );
        GLExtensions.SetupProjectionMatrix( _program.Get(), Size.X, Size.Y, projectionLocation );

        _program.Use();

        GL.BindVertexArray( _vertexArrayObject );
        GL.DrawElements( PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0 );

        _program.Disuse();

        SwapBuffers();
    }
}
