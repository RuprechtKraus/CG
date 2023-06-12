using OpenTK.Graphics.OpenGL4;

namespace Toolkit.Shaders;

public class ShaderCompiler
{
    private readonly List<int> _shaders = new();

    public void CompileShader( int shader )
    {
        GL.CompileShader( shader );
        _shaders.Add( shader );
    }

    public void CompileShader( Shader shader )
    {
        CompileShader( shader.Get() );
    }

    public void CheckStatus()
    {
        StringWriter sw = new();
        bool error = false;

        foreach ( int shader in _shaders )
        {
            GL.GetShader( shader, ShaderParameter.CompileStatus, out int param );
            if ( param != 1 )
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
