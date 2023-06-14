﻿using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Toolkit.Shaders;

namespace Ripples;

public class Window : GameWindow
{
    private ShaderProgram _program = null!;
    
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

    protected override void OnRenderFrame( FrameEventArgs args )
    {
        base.OnRenderFrame( args );
    }

    protected override void OnResize( ResizeEventArgs e )
    {
        base.OnResize( e );
        
        GL.Viewport( 0, 0, e.Width, e.Height );
    }
}
