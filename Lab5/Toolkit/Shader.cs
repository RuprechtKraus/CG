using OpenTK.Graphics.OpenGL4;

namespace Toolkit;

public class Shader : IDisposable
{
    private readonly int _shader;
    
    public Shader( ShaderType type )
    {
        _shader = GL.CreateShader( type );
    }

    public void SetSource( string path )
    {
        string source = File.ReadAllText( path );
        GL.ShaderSource( _shader, source );
    }

    public void Compile()
    {
        GL.CompileShader( _shader );
    }

    public int GetParameter( ShaderParameter parameter )
    {
        GL.GetShader( _shader, parameter, out int param );
        return param;
    }

    public int Get() => _shader;

    public string GetInfoLog() => GL.GetShaderInfoLog( _shader );

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
        
        GL.DeleteShader( _shader );
        _disposed = true;
    }

    ~Shader()
    {
        if ( _disposed == false )
        {
            Console.WriteLine( "GPU Resource leak! Did you forget to call Dispose()?" );
        }
    }
    
    #endregion
}
