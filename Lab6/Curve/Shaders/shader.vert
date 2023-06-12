#version 460 core

layout (location = 0) in vec2 position;

uniform float time;
uniform float animationDurationInSeconds;

void main()
{
    float x = position.x * 10;
    float y = sin(x) / x;
    float dist = position.y + y;
    float dy = time * dist / animationDurationInSeconds;
    
    gl_Position = vec4(position.x, position.y + dy, 0.0, 1.0);
}
