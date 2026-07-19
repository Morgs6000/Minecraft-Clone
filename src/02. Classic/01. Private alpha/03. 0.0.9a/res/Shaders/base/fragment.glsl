#version 330 core
out vec4 FragColor;

vec4 result;
uniform vec4 uColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);

void ProcessWireframe();
uniform bool useWireframe = false;

void ProcessColor();
uniform bool useColor = false;
in vec4 fColor;

void ProcessTexture();
uniform bool useTexture = false;
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
    if (useWireframe)
    {
        result = vec4(0.0f, 0.0f, 0.0f, 1.0f);
    }
}

void ProcessColor()
{
    if (useColor)
    {
        result *= fColor;
    }
}

void ProcessTexture()
{
    if (useTexture)
    {
        vec4 texColor = texture(uTexture, fTexCoord);
        if (texColor.a < 0.1f)
        {
            discard;
        }

        result *= texColor;
    }
}
