﻿using System.Drawing;
using OpenTK.Graphics.OpenGL4;

namespace Toolkit.Textures;

public class Texture
{
    private readonly int _handle;

    public Texture( int texture )
    {
        _handle = texture;
    }

    public void Use( TextureUnit unit )
    {
        GL.ActiveTexture( unit );
        GL.BindTexture( TextureTarget.Texture2D, _handle );
    }

    public int Get() => _handle;
}
