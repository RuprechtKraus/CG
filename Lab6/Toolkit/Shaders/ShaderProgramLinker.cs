using OpenTK.Graphics.OpenGL4;

namespace Toolkit.Shaders;

public class ShaderProgramLinker
{
    private readonly List<int> _programs = new();

    public void LinkProgram( int program )
    {
        GL.LinkProgram( program );
        _programs.Add( program );
    }

    public void LinkProgram( ShaderProgram program )
    {
        LinkProgram( program.Get() );
    }

    public void CheckStatus()
    {
        StringWriter sw = new();
        bool error = false;

        foreach ( int program in _programs )
        {
            GL.GetProgram( program, GetProgramParameterName.LinkStatus,  out int param );
            if ( param != 1 )
            {
                error = true;
                sw.WriteLine( $"Program {program} link failed: {GL.GetProgramInfoLog( program )}" );
            }
        }

        _programs.Clear();

        if ( error )
        {
            throw new ApplicationException( sw.ToString() );
        }
    }
}
