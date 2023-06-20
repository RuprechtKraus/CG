using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Toolkit.Shaders;

public class ShaderProgram : IDisposable
{
    private readonly int _program;

    public ShaderProgram()
    {
        _program = GL.CreateProgram();
    }

    public void AttachShader( int shader )
    {
        GL.AttachShader( _program, shader );
    }

    public void AttachShader( Shader shader )
    {
        AttachShader( shader.Get() );
    }

    public void DetachShader( int shader )
    {
        GL.DetachShader( _program, shader );
    }

    public void DetachShader( Shader shader )
    {
        DetachShader( shader.Get() );
    }

    public void Link()
    {
        GL.LinkProgram( _program );
    }

    public void Validate()
    {
        GL.ValidateProgram( _program );
    }

    public void Use()
    {
        GL.UseProgram( _program );
    }

    public void Disuse()
    {
        GL.UseProgram( 0 );
    }

    public int GetParameter( GetProgramParameterName parameter )
    {
        GL.GetProgram( _program, parameter, out int param );
        return param;
    }

    public int GetUniformLocation( string name ) => GL.GetUniformLocation( _program, name );

    public int GetAttributeLocation( string name ) => GL.GetAttribLocation( _program, name );

    public int Get() => _program;

    public void SetUniform1( string name, int value )
    {
        int location = GetUniformLocation( name );
        GL.Uniform1( location, value );
    }
    
    public void SetUniform2( string name, Vector2 value )
    {
        int location = GetUniformLocation( name );
        GL.Uniform2( location, value );
    }

    public string GetInfoLog() => GL.GetProgramInfoLog( _program );

    #region Disposing

    private bool _disposed;

    public void Dispose()
    {
        Dispose( true );
        GC.SuppressFinalize( this );
    }

    protected virtual void Dispose( bool disposing )
    {
        if ( _disposed )
        {
            return;
        }

        GL.DeleteProgram( _program );
        _disposed = true;
    }

    ~ShaderProgram()
    {
        if ( _disposed == false )
        {
            Console.WriteLine( "GPU Resource leak! Did you forget to call Dispose()?" );
        }
    }

    #endregion
}
