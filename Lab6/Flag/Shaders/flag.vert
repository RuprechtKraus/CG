#version 460 core

layout (location = 0) in vec2 aPosition;

uniform mat4 projection; 

out vec2 vPosition;

void main()
{
    vPosition = aPosition;
    gl_Position = projection * vec4(aPosition, 0.0, 1.0);
}
