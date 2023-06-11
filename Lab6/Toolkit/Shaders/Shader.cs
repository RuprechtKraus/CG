using OpenTK.Graphics.OpenGL4;

namespace Toolkit.Shaders;

public class Shader
{
    private int _shader;
    
    public Shader( ShaderType type )
    {
        _shader = GL.CreateShader( type );
    }

    public void SetSource( string source )
    {
        GL.ShaderSource( _shader, source );
    }

    public void Compile()
    {
        GL.CompileShader( _shader );
    }

    public void Delete()
    {
        GL.DeleteShader( _shader );
        _shader = 0;
    }

    public int GetParameter( ShaderParameter parameter )
    {
        GL.GetShader( _shader, parameter, out int param );
        return param;
    }

    public int Get() => _shader;

    public string GetInfoLog() => GL.GetShaderInfoLog( _shader );

    private void AssertShader()
    {
        if ( _shader == 0 )
        {
            throw new ApplicationException( "Shader was deleted!" );
        }
    }
}
