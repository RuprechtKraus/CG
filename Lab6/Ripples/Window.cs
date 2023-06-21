using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Toolkit;
using Toolkit.Shaders;
using Toolkit.Textures;

namespace Ripples;

[SuppressMessage( "Interoperability", "CA1416:Проверка совместимости платформы" )]
public class Window : GameWindow
{
    private readonly Vector2 _renderAreaResolution = new Vector2( 16, 9 );
    private readonly float _renderAreaRatio;
    
    private readonly Stopwatch _stopwatch = new Stopwatch();

    private readonly float[] _imageVertices =
    {
        //Position  Texture coordinates
        1.0f, 1.0f, 1.0f, 0.0f, // top right
        1.0f, -1.0f, 1.0f, 1.0f, // bottom right
        -1.0f, -1.0f, 0.0f, 1.0f, // bottom left
        -1.0f, 1.0f, 0.0f, 0.0f // top left
    };

    private readonly uint[] _imageIndices =
    {
        0, 1, 3,
        1, 2, 3
    };

    private int _vertexBufferObject;
    private int _vertexArrayObject;
    private int _elementBufferObject;

    private ShaderProgram _program = null!;
    private Texture _texture1 = null!;
    private Texture _texture2 = null!;
    private Vector2 _mouse;

    private int _projectionLocation;

    public Window( int width, int height, string title )
        : base( GameWindowSettings.Default, new NativeWindowSettings
        {
            Size = ( width, height ),
            Title = title
        } )
    {
        _renderAreaRatio = _renderAreaResolution.X / _renderAreaResolution.Y;
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor( Color.White );

        InitializeShaders();
        InitializeTextures();
        InitializeVertexObjects();
        InitializeElementBuffer();
        
        _program.Use();

        _projectionLocation = _program.GetUniformLocation( "projection" );
        _program.SetUniform2( "resolution", _renderAreaResolution );

        _program.Disuse();

        _stopwatch.Start();
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

    private void InitializeTextures()
    {
        _texture1 = TextureLoader.LoadTexture( "../../../Images/Texture 1.jpg" );
        _texture2 = TextureLoader.LoadTexture( "../../../Images/Texture 2.jpg" );

        AdjustImageVertices();

        _program.Use();

        _texture1.Use( TextureUnit.Texture0 );
        _texture2.Use( TextureUnit.Texture1 );

        _program.SetUniform1( "texture0", 0 );
        _program.SetUniform1( "texture1", 1 );

        _program.Disuse();

        void AdjustImageVertices()
        {
            float imageAspectRatio = _renderAreaResolution.X / _renderAreaResolution.Y;

            if ( imageAspectRatio > 1.0 )
            {
                _imageVertices[ 1 ] /= imageAspectRatio;
                _imageVertices[ 5 ] /= imageAspectRatio;
                _imageVertices[ 9 ] /= imageAspectRatio;
                _imageVertices[ 13 ] /= imageAspectRatio;
            }
            else
            {
                _imageVertices[ 0 ] *= imageAspectRatio;
                _imageVertices[ 4 ] *= imageAspectRatio;
                _imageVertices[ 8 ] *= imageAspectRatio;
                _imageVertices[ 12 ] *= imageAspectRatio;
            }
        }
    }

    private void InitializeVertexObjects()
    {
        _vertexBufferObject = GL.GenBuffer();
        _vertexArrayObject = GL.GenVertexArray();

        GL.BindVertexArray( _vertexArrayObject );

        GL.BindBuffer( BufferTarget.ArrayBuffer, _vertexBufferObject );
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _imageVertices.Length * sizeof(float),
            _imageVertices,
            BufferUsageHint.StaticDraw );

        var vertexLocation = _program.GetAttributeLocation( "aPosition" );
        GL.VertexAttribPointer( vertexLocation, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0 );

        var texCoordLocation = _program.GetAttributeLocation( "aTexCoord" );
        GL.VertexAttribPointer( texCoordLocation, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float),
            2 * sizeof(float) );
    }

    private void InitializeElementBuffer()
    {
        _elementBufferObject = GL.GenBuffer();

        GL.BindBuffer( BufferTarget.ElementArrayBuffer, _elementBufferObject );
        GL.BufferData(
            BufferTarget.ElementArrayBuffer,
            _imageIndices.Length * sizeof(uint),
            _imageIndices,
            BufferUsageHint.StaticDraw );
    }

    protected override void OnUpdateFrame( FrameEventArgs args )
    {
        base.OnUpdateFrame( args );

        float elapsedSeconds = AdvanceTime();

        const float animationDuration = 12.5f;
        if ( elapsedSeconds >= animationDuration )
        {
            _stopwatch.Reset();
            SwapTextures( _texture1, _texture2 );
            _stopwatch.Start();
        }

        _program.Use();
        _program.SetUniform2( "mouse", _mouse );
        _program.Disuse();
    }

    private float AdvanceTime()
    {
        float elapsedSeconds = (float) _stopwatch.ElapsedMilliseconds / 1000;

        _program.Use();
        _program.SetUniform1( "time", elapsedSeconds );
        _program.Disuse();

        return elapsedSeconds;
    }

    private void SwapTextures( Texture tex1, Texture tex2 )
    {
        TextureUnit unit1 = tex1.Unit;
        TextureUnit unit2 = tex2.Unit;
        _texture1.Use( unit2 );
        _texture2.Use( unit1 );
    }

    protected override void OnMouseDown( MouseButtonEventArgs e )
    {
        base.OnMouseDown( e );

        float x = ( MousePosition.X - Size.X / 2.0f ) / Size.X * 2.0f;
        float y = ( MousePosition.Y - Size.Y / 2.0f ) / Size.Y * 2.0f;
        float ratio = (float) Size.X / Size.Y;

        if ( ratio > _renderAreaRatio )
        {
            x *= ( ratio / _renderAreaRatio );
        }
        else
        {
            y /= ( ratio / _renderAreaRatio );
        }
        
        _mouse = new Vector2( x, y );
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

        GLExtensions.SetupLandscapeProjectionMatrix(
            _program.Get(),
            Size.X,
            Size.Y,
            _projectionLocation,
            16.0f / 9.0f );

        _program.Use();

        GL.EnableVertexAttribArray( 0 );
        GL.EnableVertexAttribArray( 1 );

        GL.BindVertexArray( _vertexArrayObject );
        GL.DrawElements( PrimitiveType.Triangles, _imageIndices.Length, DrawElementsType.UnsignedInt, 0 );

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
