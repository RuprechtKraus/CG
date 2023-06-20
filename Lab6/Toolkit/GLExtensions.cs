using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Toolkit;

public static class GLExtensions
{
    public static void SetupProjectionMatrix( int program, float width, float height, int projectionLocation )
    {
        GL.UseProgram( program );

        float aspectRatio = width / height;
        float w = 2.0f;
        float h = 2.0f;

        if ( aspectRatio > 1.0f )
        {
            w *= aspectRatio;
        }
        else
        {
            h /= aspectRatio;
        }

        Matrix4 ortho = Matrix4.CreateOrthographic( w, h, -1.0f, 1.0f );
        GL.UniformMatrix4( projectionLocation, true, ref ortho );

        GL.UseProgram( 0 );
    }

    public static void SetupLandscapeProjectionMatrix( int program, float width, float height, int projectionLocation,
        float landscapeRatio )
    {
        GL.UseProgram( program );

        float aspectRatio = width / height;
        float w = 2.0f;
        float h = 2.0f;

        if ( aspectRatio > landscapeRatio )
        {
            w *= ( aspectRatio / landscapeRatio );
            h /= landscapeRatio;
        }
        else
        {
            h /= aspectRatio;
        }

        Matrix4 ortho = Matrix4.CreateOrthographic( w, h, -1.0f, 1.0f );
        GL.UniformMatrix4( projectionLocation, true, ref ortho );

        GL.UseProgram( 0 );
    }
}
