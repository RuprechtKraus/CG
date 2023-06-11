using OpenTK.Graphics.OpenGL4;

namespace Toolkit.Shaders;

public static class ShaderLoader
{
    public static Shader LoadShader( ShaderType type, string filePath )
    {
        string source = File.ReadAllText( filePath );
        
        var shader = new Shader( type );
        shader.SetSource( source );

        return shader;
    }
}
