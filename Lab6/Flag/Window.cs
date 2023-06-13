using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Flag;

public class Window : GameWindow
{
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
    }

    protected override void OnResize( ResizeEventArgs e )
    {
        base.OnResize( e );
    }
}
