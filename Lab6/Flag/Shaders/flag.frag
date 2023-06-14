#version 460 core

in vec2 vPosition;

out vec4 fragColor;

bool doIntersect(vec2 p1, vec2 q1, vec2 p2, vec2 q2);
int orientation(vec2 p, vec2 q, vec2 r);
bool onSegment(vec2 p, vec2 q, vec2 r);

void main(void)
{
    vec2 v = vPosition;

    vec2 p1 = vec2(0.0, 1.0);
    vec2 p2 = vec2(0.25, 0.3);
    vec2 p3 = vec2(1.0, 0.3);
    vec2 p4 = vec2(0.4, -0.15);
    vec2 p5 = vec2(0.65, -1.0);
    vec2 p6 = vec2(0, -0.45);
    vec2 p7 = vec2(-0.65, -1.0);
    vec2 p8 = vec2(-0.4, -0.15);
    vec2 p9 = vec2(-1.0, 0.3);
    vec2 p10 = vec2(-0.25, 0.3);

    int count = 0;
    count = count + (doIntersect(p1, p2, v, vec2(v.y, 1.0)) ? 1 : 0);
    count = count + (doIntersect(p2, p3, v, vec2(v.y, 1.0)) ? 1 : 0);
    count = count + (doIntersect(p3, p4, v, vec2(v.y, 1.0)) ? 1 : 0);
    count = count + (doIntersect(p4, p5, v, vec2(v.y, 1.0)) ? 1 : 0);
    count = count + (doIntersect(p5, p6, v, vec2(v.y, 1.0)) ? 1 : 0);
    count = count + (doIntersect(p6, p7, v, vec2(v.y, 1.0)) ? 1 : 0);
    count = count + (doIntersect(p7, p8, v, vec2(v.y, 1.0)) ? 1 : 0);
    count = count + (doIntersect(p8, p9, v, vec2(v.y, 1.0)) ? 1 : 0);
    count = count + (doIntersect(p9, p10, v, vec2(v.y, 1.0)) ? 1 : 0);
    count = count + (doIntersect(p10, p1, v, vec2(v.y, 1.0)) ? 1 : 0);
    
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

    return (val > 0) ? 1: 2;
}
