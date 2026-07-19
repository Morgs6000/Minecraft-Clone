#version 330 core
layout (location = 0) in vec3 vPos;
layout (location = 1) in vec4 vColor;
layout (location = 2) in vec2 vTexCoord;
  
uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

out vec4 fColor;
out vec2 fTexCoord;

void main()
{
    gl_Position = uProjection * uView * uModel * vec4(vPos, 1.0f);
    fColor = vColor;
    fTexCoord = vTexCoord;
}
