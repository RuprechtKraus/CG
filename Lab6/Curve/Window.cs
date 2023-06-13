using System.Diagnostics;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Toolkit;
using Toolkit.Shaders;

namespace Curve;

public class Window : GameWindow
{
    private readonly Stopwatch _stopwatch = new();

    private Plot _plot = null!;
    private Graph _graph = null!;
    private ShaderProgram _graphProgram = null!;
    private ShaderProgram _plotProgram = null!;
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

        InitializeGraphShaders();
        InitializePlotShaders();
        
        _graph = new Graph(
            _graphProgram,
            _graphProgram.GetUniformLocation( "time" ),
            _graphProgram.GetUniformLocation( "animationDurationInSeconds" )
        );
        _graph.AnimationDurationInSeconds = 2.0f;
        
        _plot = new Plot( _plotProgram );

        _stopwatch.Start();
    }

    private void InitializeGraphShaders()
    {
        Shader vertexShader = ShaderLoader.LoadShader(
            ShaderType.VertexShader,
            @"../../../Shaders/graph.vert" );
        Shader fragmentShader = ShaderLoader.LoadShader(
            ShaderType.FragmentShader,
            @"../../../Shaders/shader.frag" );

        ShaderCompiler shaderCompiler = new();
        shaderCompiler.CompileShader( vertexShader );
        shaderCompiler.CompileShader( fragmentShader );
        shaderCompiler.CheckStatus();
        
        _graphProgram = new ShaderProgram();
        _graphProgram.AttachShader( vertexShader );
        _graphProgram.AttachShader( fragmentShader );

        ShaderProgramLinker linker = new();
        linker.LinkProgram( _graphProgram );
        linker.CheckStatus();

        _graphProgram.DetachShader( vertexShader );
        _graphProgram.DetachShader( fragmentShader );

        vertexShader.Delete();
        fragmentShader.Delete();
    }
    
    private void InitializePlotShaders()
    {
        Shader vertexShader = ShaderLoader.LoadShader(
            ShaderType.VertexShader,
            @"../../../Shaders/plot.vert" );

        ShaderCompiler shaderCompiler = new();
        shaderCompiler.CompileShader( vertexShader );
        shaderCompiler.CheckStatus();
        
        _plotProgram = new ShaderProgram();
        _plotProgram.AttachShader( vertexShader );

        ShaderProgramLinker linker = new();
        linker.LinkProgram( _plotProgram );
        linker.CheckStatus();

        _plotProgram.DetachShader( vertexShader );

        vertexShader.Delete();
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

        _graphProgram.Dispose();
    }

    private void DrawFrame()
    {
        GL.Clear( ClearBufferMask.ColorBufferBit );

        int graphProjectionLocation = _graphProgram.GetUniformLocation( "projection" );
        GLExtensions.SetupProjectionMatrix( _graphProgram.Get(), Size.X, Size.Y, graphProjectionLocation );
        
        int plotProjectionLocation = _plotProgram.GetUniformLocation( "projection" );
        GLExtensions.SetupProjectionMatrix( _plotProgram.Get(), Size.X, Size.Y, plotProjectionLocation );
        
        _graph.Draw( _time, Size.X, Size.Y );
        _plot.Draw();

        SwapBuffers();
    }
}
