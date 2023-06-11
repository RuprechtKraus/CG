using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Toolkit;
using Toolkit.Shaders;

namespace Curve;

public class Window : GameWindow
{
    private float[] _vertices = Array.Empty<float>();
    private int _vertexBufferObject;
    private int _vertexArrayObject;
    private ShaderProgram _program;

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
        FillVertices();

        InitVertexBufferObject();

        Shader vertexShader = ShaderLoader.LoadShader( 
            ShaderType.VertexShader, 
            @"../../../Shaders/shader.vert" );
        Shader fragmentShader = ShaderLoader.LoadShader( 
            ShaderType.FragmentShader, 
            @"../../../Shaders/shader.frag" );

        ShaderCompiler shaderCompiler = new();
        shaderCompiler.Compile( vertexShader );
        shaderCompiler.Compile( fragmentShader );

        _program = new ShaderProgram();

        _program.AttachShader( vertexShader.Get() );
        _program.AttachShader( fragmentShader.Get() );
        _program.Link();

        _program.DetachShader( vertexShader.Get() );
        _program.DetachShader( fragmentShader.Get() );

        vertexShader.Delete();
        fragmentShader.Delete();

        _program.Use();
    }

    private void InitVertexBufferObject()
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
        GL.EnableVertexAttribArray( 0 );
    }

    protected override void OnRenderFrame( FrameEventArgs args )
    {
        base.OnRenderFrame( args );
        DrawFrame();
    }

    protected override void OnResize( ResizeEventArgs e )
    {
        base.OnResize( e );

        GL.Viewport( 0, 0, e.Width, e.Height );
        DrawFrame();
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        _program.Dispose();
    }

    private void DrawFrame()
    {
        GL.Clear( ClearBufferMask.ColorBufferBit );

        GL.BindVertexArray( _vertexArrayObject );
        GL.DrawArrays( PrimitiveType.LineStrip, 0, 101 );

        SwapBuffers();
    }

    private void FillVertices()
    {
        const int count = 100;
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
