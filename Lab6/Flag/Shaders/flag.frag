#version 460 core

in vec2 vPosition;

out vec4 fragColor;

void main()
{
    if (length(vPosition) <= 0.2)
    {
        fragColor = vec4(1.0, 0.0, 0.0, 1.0);
    }
    else
    {
        fragColor = vec4(1.0, 1.0, 1.0, 1.0);
    }
}
