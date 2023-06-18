#version 460 core

uniform sampler2D texture0;
uniform sampler2D texture1;

in vec2 texCoord;

out vec4 fragColor;

void main()
{
    fragColor = texture(texture0, texCoord);
}
