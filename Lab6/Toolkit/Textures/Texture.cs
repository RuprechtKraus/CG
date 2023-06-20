using System.Drawing;
using OpenTK.Graphics.OpenGL4;

namespace Toolkit.Textures;

public class Texture
{
    private readonly int _handle;
    
    public TextureUnit Unit { get; private set; }

    public Texture( int texture )
    {
        _handle = texture;
    }

    public void Use( TextureUnit unit )
    {
        Unit = unit;
        GL.ActiveTexture( unit );
        GL.BindTexture( TextureTarget.Texture2D, _handle );
    }

    public int Get() => _handle;
}
