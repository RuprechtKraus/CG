#version 460 core

uniform float time;
uniform vec2 resolution;

uniform sampler2D texture0;
uniform sampler2D texture1;

in vec2 texCoord;

out vec4 fragColor;

void main()
{
    vec2 uv = -1.0 + 2.0 * texCoord;
    uv.x *= (resolution.x / resolution.y);
    
    if ( length(uv) > 0.2 )
    {
        fragColor = texture(texture0, texCoord);
    }
    else
    {
        fragColor = texture(texture1, texCoord);
    }
}
