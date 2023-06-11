#version 460 core

layout (location = 0) in vec2 position;

uniform float time;

void main()
{
    float animationTime = 2.0;
    float y = sin(position.x * 10) / position.x * 0.1;
    float dist = position.y + y;
    float dy = time * dist / animationTime;
    
    gl_Position = vec4(position.x, position.y + dy, 0.0, 1.0);
}
