using Minecraft.Mathematics;
using Minecraft.Meshing;
using Minecraft.Rendering;

namespace Minecraft.Gui;

public class GuiFont
{
    private int[] _charWidhts = new int[256];

    private Texture _texture = null!;

    private MeshQuad _mesh = null!;
    private MeshRender _meshRender = null!;

    public GuiFont(MeshQuad mesh)
    {
        _texture = new Texture("default");

        _mesh = mesh;
        _meshRender = new MeshRender();

        int w = _texture.Width;
        int h = _texture.Height;
        byte[] rawPixels = _texture.Data;

        for (int i = 0; i < 128; i++)
        {
            int xt = i % 16;
            int yt = i / 16;
            int x = 0;

            for (bool emptyColumn = false; x < 8 && !emptyColumn; x++)
            {
                int xPixel = xt * 8 + x;
                emptyColumn = true;

                for (int y = 0; y < 8 && emptyColumn; y++)
                {
                    int yPixel = (yt * 8 + y) * w;
                    int pixel = rawPixels[(xPixel + yPixel) * 4];

                    if (pixel > 128)
                    {
                        emptyColumn = false;
                    }
                }
            }

            if (i == 32)
            {
                x = 4;
            }

            _charWidhts[i] = x;
        }        
    }

    public void Render(Shader shader)
    {
        Matrix4 model = Matrix4.Identity;
        shader.SetUniform("uModel", model);

        _texture.Bind();

        _meshRender.Draw(shader);
    }

    public void DrawShadow(string str, Vector2 position, int color)
    {
        // _mesh.Clear();

        Draw(str, new Vector2(position.X + 1, position.Y - 1), color, true);
        Draw(str, new Vector2(position.X, position.Y), color);

        // _meshRender.Mesh = _mesh;
    }

    public void Draw(string str, Vector2 position, int color)
    {
        Draw(str, position, color, false);
    }

    public void Draw(string str, Vector2 position, int color, bool darken)
    {
        char[] chars = str.ToCharArray();

        if (darken)
        {
            color = (color & 16579836) >> 2;
        }

        // _mesh.Clear();

        int xo = 0;

        for (int i = 0; i < chars.Length; i++)
        {
            int ix;
            int iy;

            if (chars[i] == 38)
            {
                ix = "0123456789abcdef".IndexOf(chars[i + 1]);
                iy = (ix & 8) * 8;

                int r = ((ix & 4) >> 2) * 191 + iy;
                int g = ((ix & 2) >> 1) * 191 + iy;
                int b = ( ix & 1)       * 191 + iy;

                color = r << 16 | g << 8 | b;

                i += 2;

                if (darken)
                {
                    color = (color & 16579836) >> 2;
                }
            }

            ix = chars[i] % 16 * 8;
            iy = chars[i] / 16 * 8;

            float x0 = (float)(position.X + xo);
            float y0 = (float)position.Y;

            float x1 = (float)(position.X + xo + 8);
            float y1 = (float)(position.Y + 8);

            float u0 = (float)ix / 128.0f;
            float v0 = (float)iy / 128.0f;

            float u1 = (float)(ix + 8) / 128.0f;
            float v1 = (float)(iy + 8) / 128.0f;

            _mesh.AddQuad(
                [
                    new Vector3(x0, y0, 0.0f),
                    new Vector3(x1, y0, 0.0f),
                    new Vector3(x1, y1, 0.0f),
                    new Vector3(x0, y1, 0.0f)
                ],
                [
                    Color.FromDec(color),
                    Color.FromDec(color),
                    Color.FromDec(color),
                    Color.FromDec(color)
                ],
                [
                    new TexCoord(u0, v1),
                    new TexCoord(u1, v1),
                    new TexCoord(u1, v0),
                    new TexCoord(u0, v0)
                ]
            );

            xo += _charWidhts[chars[i]];
        }

        _meshRender.Mesh = _mesh;
    }
}
