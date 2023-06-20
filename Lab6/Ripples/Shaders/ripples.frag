#version 460 core

uniform sampler2D texture0;
uniform sampler2D texture1;

in vec2 texCoord;

out vec4 fragColor;

void main()
{
    if ( texCoord.x < 0.5 )
    {
        fragColor = texture(texture0, texCoord);
    }
    else
    {
        fragColor = texture(texture1, texCoord);
    }
}
