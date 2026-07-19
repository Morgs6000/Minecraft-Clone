using RubyDung.Mathematics;

namespace RubyDung.Meshing;

public class MeshCube : MeshQuad
{
    public Vector2Int TextureSize;

    public Vector4Int UVRect;

    public void AddCube(Vector3 position, Vector3 size)
    {
        float x0 = position.X;
        float y0 = position.Y;
        float z0 = position.Z;

        float x1 = x0 + size.X;
        float y1 = y0 + size.Y;
        float z1 = z0 + size.Z;

        float u0 = (float)UVRect.X / (float)TextureSize.X;
        float v0 = (float)UVRect.Y / (float)TextureSize.Y;

        float u1 = u0 + ((float)UVRect.Z / (float)TextureSize.X);
        float v1 = v0 + ((float)UVRect.W / (float)TextureSize.Y);

        TexCoord [] texCoords = [
            new TexCoord(u0, v1),
            new TexCoord(u1, v1),
            new TexCoord(u1, v0),
            new TexCoord(u0, v0)
        ];

        AddQuad(
            [
                new Vector3(x0, y1, z0),
                new Vector3(x0, y0, z0),
                new Vector3(x0, y0, z1),
                new Vector3(x0, y1, z1)
            ],
            texCoords
        );

        AddQuad(
            [
                new Vector3(x1, y0, z0),
                new Vector3(x1, y1, z0),
                new Vector3(x1, y1, z1),
                new Vector3(x1, y0, z1)
            ],
            texCoords
        );

        AddQuad(
            [
                new Vector3(x0, y0, z0),
                new Vector3(x1, y0, z0),
                new Vector3(x1, y0, z1),
                new Vector3(x0, y0, z1)
            ],
            texCoords
        );

        AddQuad(
            [
                new Vector3(x1, y1, z0),
                new Vector3(x0, y1, z0),
                new Vector3(x0, y1, z1),
                new Vector3(x1, y1, z1)
            ],
            texCoords
        );

        AddQuad(
            [
                new Vector3(x0, y1, z0),
                new Vector3(x1, y1, z0),
                new Vector3(x1, y0, z0),
                new Vector3(x0, y0, z0)
            ],
            texCoords
        );

        AddQuad(
            [
                new Vector3(x0, y0, z1),
                new Vector3(x1, y0, z1),
                new Vector3(x1, y1, z1),
                new Vector3(x0, y1, z1)
            ],
            texCoords
        );
    }
}
