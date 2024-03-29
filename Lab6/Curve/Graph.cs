﻿using OpenTK.Graphics.OpenGL4;
using Toolkit.Shaders;

namespace Curve;

public class Graph
{
    private const int VerticesCount = 500; 
        
    private readonly ShaderProgram _program;
    private readonly int _animationDurationLocation;
    private readonly int _timeLocation;

    private float[] _vertices = Array.Empty<float>();
    
    private int _vertexBufferObject;
    private int _vertexArrayObject;
    
    private bool _initialized;

    public float AnimationDurationInSeconds { get; set; } = 1.0f;

    public Graph( ShaderProgram program, int timeLocation, int animationDurationLocation )
    {
        _program = program;
        _timeLocation = timeLocation;
        _animationDurationLocation = animationDurationLocation;
    }
    
    public void Draw( float time, int width, int height )
    {
        if ( _program.Get() == 0 )
        {
            return;
        }
        
        if ( !_initialized )
        {
            Initialize();
        }
        
        _program.Use();
        
        GL.EnableVertexAttribArray( 0 );
        GL.BindVertexArray( _vertexArrayObject );
        
        GL.Uniform1( _animationDurationLocation, AnimationDurationInSeconds );
        
        time = time <= AnimationDurationInSeconds 
            ? time 
            : AnimationDurationInSeconds;
        GL.Uniform1( _timeLocation, time );
        
        GL.DrawArrays( PrimitiveType.LineStrip, 0, VerticesCount + 1 );

        GL.BindVertexArray( 0 );
        GL.DisableVertexAttribArray( 0 );
        
        _program.Disuse();
    }

    private void Initialize()
    {
        InitializeVertices();
        InitializeVertexBufferObject();
        
        _initialized = true;
    }
    
    private void InitializeVertices()
    {
        const int dimension = 2;
        const float step = 2.0f / VerticesCount;
        
        _vertices = new float[ VerticesCount * dimension + 1 ];

        float x = -1.0f;
        for ( int i = 0; x < 1.0f; i += 2 )
        {
            _vertices[ i ] = x;
            x += step;
        }

        _vertices[ VerticesCount * dimension ] = 1.0f;
    }

    private void InitializeVertexBufferObject()
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
    }
}
