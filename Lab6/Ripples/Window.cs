using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Toolkit;
using Toolkit.Shaders;
using Toolkit.Textures;

namespace Ripples;

public class Window : GameWindow
{
    private ShaderProgram _program = null!;

    private float[] _vertices =
    {
        //Position   Texture coordinates
        1.0f, 1.0f, 1.0f, 0.0f, // top right
        1.0f, -1.0f, 1.0f, 1.0f, // bottom right
        -1.0f, -1.0f, 0.0f, 1.0f, // bottom left
        -1.0f, 1.0f, 0.0f, 0.0f // top left
    };

    private readonly uint[] _indices =
    {
        0, 1, 3,
        1, 2, 3
    };

    private int _vertexBufferObject;
    private int _vertexArrayObject;
    private int _elementBufferObject;
    private Texture _texture = null!;

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
        InitializeVertices();
    }

    private void InitializeShaders()
    {
        Shader vertexShader = ShaderLoader.LoadShader(
            ShaderType.VertexShader,
            @"../../../Shaders/ripples.vert" );
        Shader fragmentShader = ShaderLoader.LoadShader(
            ShaderType.FragmentShader,
            @"../../../Shaders/ripples.frag" );

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

    private void InitializeVertices()
    {
        _texture = TextureLoader.LoadTexture( "../../../Images/Texture 1.jpg" );
        float ar = _texture.AspectRatio;

        if ( ar > 1.0 )
        {
            _vertices[ 1 ] /= ar;
            _vertices[ 5 ] /= ar;
            _vertices[ 9 ] /= ar;
            _vertices[ 13 ] /= ar;
        }
        else
        {
            _vertices[ 0 ] *= ar;
            _vertices[ 4 ] *= ar;
            _vertices[ 8 ] *= ar;
            _vertices[ 12 ] *= ar;
        }

        // Vertices coordinates
        _vertexBufferObject = GL.GenBuffer();
        _vertexArrayObject = GL.GenVertexArray();

        GL.BindVertexArray( _vertexArrayObject );

        GL.BindBuffer( BufferTarget.ArrayBuffer, _vertexBufferObject );
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _vertices.Length * sizeof(float),
            _vertices,
            BufferUsageHint.StaticDraw );

        // Element buffer
        _elementBufferObject = GL.GenBuffer();

        GL.BindBuffer( BufferTarget.ElementArrayBuffer, _elementBufferObject );
        GL.BufferData(
            BufferTarget.ElementArrayBuffer,
            _indices.Length * sizeof(uint),
            _indices,
            BufferUsageHint.StaticDraw );

        var vertexLocation = _program.GetAttributeLocation( "aPosition" );
        GL.VertexAttribPointer( vertexLocation, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0 );

        // Texture coordinates
        var texCoordLocation = _program.GetAttributeLocation( "aTexCoord" );
        GL.VertexAttribPointer( texCoordLocation, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float),
            2 * sizeof(float) );
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

    private void DrawFrame()
    {
        GL.Clear( ClearBufferMask.ColorBufferBit );

        int projectionLocation = _program.GetUniformLocation( "projection" );
        GLExtensions.SetupLandscapeProjectionMatrix( _program.Get(), Size.X, Size.Y, projectionLocation, 16.0f / 9.0f );

        _texture.Use( TextureUnit.Texture0 );
        _program.Use();

        GL.EnableVertexAttribArray( 0 );
        GL.EnableVertexAttribArray( 1 );

        GL.BindVertexArray( _vertexArrayObject );
        GL.DrawElements( PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0 );

        GL.DisableVertexAttribArray( 1 );
        GL.DisableVertexAttribArray( 0 );

        _program.Disuse();

        SwapBuffers();
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        _program.Dispose();
    }
}
