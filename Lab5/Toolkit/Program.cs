using OpenTK.Graphics.OpenGL4;

namespace Toolkit;

public class Program : IDisposable
{
    private readonly int _program;

    public Program()
    {
        _program = GL.CreateProgram();
    }

    public void AttachShader( int shader )
    {
        GL.AttachShader( _program, shader );
    }

    public void DetachShader( int shader )
    {
        GL.DetachShader( _program, shader );
    }

    public void Link()
    {
        GL.LinkProgram( _program );
    }

    public void Validate()
    {
        GL.ValidateProgram( _program );
    }
    
    public int GetParameter( GetProgramParameterName parameter )
    {
        GL.GetProgram( _program, parameter, out int param );
        return param;
    }

    public int GetUniformLocation( string name ) => GL.GetUniformLocation( _program, name );

    public int GetAttributeLocation( string name ) => GL.GetAttribLocation( _program, name );
    
    public int Get() => _program;

    public string GetInfoLog() => GL.GetProgramInfoLog( _program );

    #region Disposing

    private bool _disposed = false;

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

        GL.DeleteShader( _program );
        _disposed = true;
    }

    ~Program()
    {
        if ( _disposed == false )
        {
            Console.WriteLine( "GPU Resource leak! Did you forget to call Dispose()?" );
        }
    }

    #endregion
}
