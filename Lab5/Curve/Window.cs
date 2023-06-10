using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Toolkit;

namespace Curve;

public class Window : GameWindow
{
    private readonly Shader _vertexShader = null!;
    
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
    }

    protected override void OnRenderFrame( FrameEventArgs args )
    {
        base.OnRenderFrame( args );
        DrawFrame();
    }

    protected override void OnResize( ResizeEventArgs e )
    {
        base.OnResize( e );

        GL.Viewport( 0, 0, e.Width, e.Width );
        DrawFrame();
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        
        _vertexShader.Dispose();
    }

    private void DrawFrame()
    {
        GL.Clear( ClearBufferMask.ColorBufferBit );
        SwapBuffers();
    }
}
