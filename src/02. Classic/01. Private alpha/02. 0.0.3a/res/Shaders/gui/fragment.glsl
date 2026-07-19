#version 330 core
out vec4 FragColor;

vec4 result;
uniform vec4 uColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);

void ProcessWireframe();
uniform bool hasWireframe = false;

void ProcessColor();
uniform bool hasColor = false;
in vec4 fColor;

void ProcessTexture();
uniform bool hasTexture = false;
in vec2 fTexCoord;
uniform sampler2D uTexture;

void main()
{
    result = uColor;

    ProcessWireframe();
    ProcessColor();
    ProcessTexture();

    FragColor = result;
}

void ProcessWireframe()
{
    if (hasWireframe)
    {
        result = vec4(0.0f, 0.0f, 0.0f, 1.0f);
    }
}

void ProcessColor()
{
    if (hasColor)
    {
        result *= fColor;
    }
}

void ProcessTexture()
{
    if (hasTexture)
    {
        vec4 texColor = texture(uTexture, fTexCoord);
        if (texColor.a < 0.1f)
        {
            discard;
        }

        result *= texColor;
    }
}
