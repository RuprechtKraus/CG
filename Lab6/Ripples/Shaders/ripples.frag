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
    uv.x /= (resolution.y / resolution.x);
    
    vec2 texCoord = texCoord;

    vec4 tex1 = texture(texture0, texCoord);
    vec4 tex2 = texture(texture1, texCoord);

    float len = length(uv);

    if (len <= (distance(texCoord, vec2(0.5, 0.5)) * 0.8 + time * 0.2) &&
    len >= (distance(texCoord, vec2(0.5, 0.5)) * 0.1 + time * 0.15))
    {
        vec2 wave = (uv / len) * sin(len * 30 - time * 10) * 0.03;
        texCoord += wave;

        tex1 = texture(texture0, texCoord);
        tex2 = texture(texture1, texCoord);

        tex1 *= 1.1;
    }

    len = smoothstep(time * 0.15, time * 0.2, len);
    vec4 col = vec4(
        mix(tex2.x, tex1.x, len),
        mix(tex2.y, tex1.y, len),
        mix(tex2.z, tex1.z, len),
        1.0);
    fragColor = col;
}
