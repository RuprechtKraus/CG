#version 460 core

uniform float time;
uniform vec2 resolution;
uniform vec2 mouse;

uniform sampler2D texture0;
uniform sampler2D texture1;

in vec2 texCoord;

out vec4 fragColor;

const float speed = 2.5;

void main()
{    
    if (time < 0)
    {
        fragColor = texture(texture0, texCoord);
        return;
    }
    
    float time = time * speed;
    
    vec2 uv =  -1.0 + 2.0 * texCoord;
    uv.x *= resolution.x / resolution.y;

    vec2 texCoord = texCoord;
    vec2 waveCenter = mouse;
    waveCenter.x *= resolution.x / resolution.y;

    vec4 tex1 = texture(texture0, texCoord);
    vec4 tex2 = texture(texture1, texCoord);

    float len = distance(uv, waveCenter);
    float outerCircleDistance = distance(texCoord / resolution, waveCenter + 0.2) * 0.05 + time * 0.3;
    float innerCircleDistance = distance(texCoord, waveCenter + 0.2) * 0.05 + time * 0.15;

    if (len <= outerCircleDistance && len >= innerCircleDistance)
    {
        vec2 wave = (uv / len) * sin(len * 30 - time * 10) * 0.03;
        texCoord += wave;

        tex1 = texture(texture0, texCoord);
        tex2 = texture(texture1, texCoord);

        tex1 *= 1.1;
    }

    len = smoothstep(innerCircleDistance, innerCircleDistance + 0.05, len);
    vec4 col = vec4(
        mix(tex2.x, tex1.x, len),
        mix(tex2.y, tex1.y, len),
        mix(tex2.z, tex1.z, len),
        1.0);
    fragColor = col;
}
