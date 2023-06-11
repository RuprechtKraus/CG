using OpenTK.Graphics.OpenGL4;

namespace Toolkit.Shaders;

public class ShaderCompiler
{
    private readonly List<int> _shaders = new();

    public void Compile( int shader )
    {
        GL.CompileShader( shader );
        _shaders.Add( shader );
    }

    public void Compile( Shader shader )
    {
        int id = shader.Get();
        
        GL.CompileShader( id );
        _shaders.Add( id );
    }

    public void CheckStatus()
    {
        StringWriter sw = new();
        bool error = false;

        foreach ( int shader in _shaders )
        {
            GL.GetShader( shader, ShaderParameter.CompileStatus, out int param );
            if ( param != 0 )
            {
                error = true;
                sw.WriteLine( $"Shader {shader} compilation failed: {GL.GetShaderInfoLog( shader )}" );
            }
        }

        _shaders.Clear();

        if ( error )
        {
            throw new ApplicationException( sw.ToString() );
        }
    }
}
