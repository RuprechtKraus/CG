using System.Diagnostics;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Toolkit.Shaders;

namespace Curve;

public class Window : GameWindow
{
    private readonly Stopwatch _stopwatch = new();

    private Graph _graph = null!;
    private ShaderProgram _program = null!;
    private float _time;

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
        InitializeGraph();

        _stopwatch.Start();
    }

    private void InitializeShaders()
    {
        Shader vertexShader = ShaderLoader.LoadShader(
            ShaderType.VertexShader,
            @"../../../Shaders/shader.vert" );
        Shader fragmentShader = ShaderLoader.LoadShader(
            ShaderType.FragmentShader,
            @"../../../Shaders/shader.frag" );

        ShaderCompiler shaderCompiler = new();
        shaderCompiler.CompileShader( vertexShader );
        shaderCompiler.CompileShader( fragmentShader );

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

    private void InitializeGraph()
    {
        _graph = new Graph(
            _program,
            _program.GetUniformLocation( "time" ),
            _program.GetUniformLocation( "animationDurationInSeconds" )
        );
        _graph.AnimationDurationInSeconds = 2.0f;
    }

    protected override void OnUpdateFrame( FrameEventArgs args )
    {
        base.OnUpdateFrame( args );

        if ( _stopwatch.IsRunning )
        {
            _time = _stopwatch.ElapsedMilliseconds / 1000f;
        }
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

        _graph.Draw( _time );

        SwapBuffers();
    }
}
