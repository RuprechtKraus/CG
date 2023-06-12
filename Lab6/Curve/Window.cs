using System.Diagnostics;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Toolkit.Shaders;

namespace Curve;

public class Window : GameWindow
{
    private Graph _graph;
    private ShaderProgram _program;

    private float _time;

    private readonly Stopwatch _stopwatch = new();

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

        #region Shaders

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

        #endregion

        _graph = new Graph(
            program: _program,
            timeLocation: GL.GetUniformLocation(
                _program.Get(),
                "time" ),
            animationDurationLocation: GL.GetUniformLocation(
                _program.Get(),
                "animationDuration" )
        );
        _graph.AnimationDurationInSeconds = 2.0f;

        _stopwatch.Start();
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
