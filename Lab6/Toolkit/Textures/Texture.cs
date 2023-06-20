using System.Drawing;
using OpenTK.Graphics.OpenGL4;

namespace Toolkit.Textures;

public class Texture
{
    private readonly int _handle;

    public float AspectRatio { get; }

    public Texture( int texture, float aspectRatio )
    {
        _handle = texture;
        AspectRatio = aspectRatio;
    }

    public void Use( TextureUnit unit )
    {
        GL.ActiveTexture( unit );
        GL.BindTexture( TextureTarget.Texture2D, _handle );
    }

    public int Get() => _handle;
}
