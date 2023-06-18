#version 460 core

in vec2 vPosition;

out vec4 fragColor;

const vec2[10] points = vec2[](
    vec2(0.0, 1.0),
    vec2(0.25, 0.3),
    vec2(1.0, 0.3),
    vec2(0.4, -0.15),
    vec2(0.65, -1.0),
    vec2(0, -0.45),
    vec2(-0.65, -1.0),
    vec2(-0.4, -0.15),
    vec2(-1.0, 0.3),
    vec2(-0.25, 0.3));

bool doIntersect(vec2 p1, vec2 q1, vec2 p2, vec2 q2);
int orientation(vec2 p, vec2 q, vec2 r);

void main()
{
    vec2 v = vPosition;
    int count = 0;

    for (int i = 0; i < points.length() - 1; i++)
    {
        count = count + (doIntersect(points[i], points[i + 1], v, vec2(v.y, 1.0)) ? 1 : 0);
    }
    count = count + (doIntersect(points[9], points[0], v, vec2(v.y, 1.0)) ? 1 : 0);

    if (mod(count, 2) == 1)
    {
        fragColor = vec4(1.0, 0.0, 0.0, 1.0);
    }
    else
    {
        fragColor = vec4(1.0, 1.0, 1.0, 1.0);
    }
}

bool doIntersect(vec2 p1, vec2 q1, vec2 p2, vec2 q2)
{
    int o1 = orientation(p1, q1, p2);
    int o2 = orientation(p1, q1, q2);
    int o3 = orientation(p2, q2, p1);
    int o4 = orientation(p2, q2, q1);

    return o1 != o2 && o3 != o4;
}

int orientation(vec2 p, vec2 q, vec2 r)
{
    float val = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);

    if (val == 0)
    {
        return 0;
    }

    return (val > 0) ? 1 : 2;
}
