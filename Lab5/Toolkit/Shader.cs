using OpenTK.Graphics.OpenGL4;

namespace Toolkit;

public class Shader : IDisposable
{
    private readonly int _shader;
    
    public Shader( int shader )
    {
        _shader = shader;
    }

    public void SetSource( string path )
    {
        string source = File.ReadAllText( path );
        GL.ShaderSource( _shader, source );
    }

    public int GetParameter( ShaderParameter parameter )
    {
        GL.GetShader( _shader, parameter, out int param );
        return param;
    }

    public string GetInfoLog()
    {
        return GL.GetShaderInfoLog( _shader );
    }

    public int Get()
    {
        return _shader;
    }

    public void Compile()
    {
        GL.CompileShader( _shader );
    }
    
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
}
