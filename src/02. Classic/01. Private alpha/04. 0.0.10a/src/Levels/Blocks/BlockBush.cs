using Minecraft.Mathematics;
using Minecraft.Meshing;
using Minecraft.Physics;

namespace Minecraft.Levels.Blocks;

public class BlockBush : Block
{
    public BlockBush(int id) : base(id)
    {
        _tex = 15;
    }

    public override bool IsSolide()
    {
        return false;
    }

    public override AABB GetBounds(Vector3Int position)
    {
        return null!;
    }

    public override void Init(MeshQuad mesh, Level level, Vector3Int position)
    {
        Color[] colors = new Color[4];

        for (int i = 0; i < 4; i++)
        {
            colors[i] = new Color(1.0f, 1.0f, 1.0f);
        }

        float u0 = (float)(_tex % 16) / 16.0f;
        float v0 = (float)(_tex / 16) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        TexCoord[] texCoords = [
            new TexCoord(u0, v1),
            new TexCoord(u1, v1),
            new TexCoord(u1, v0),
            new TexCoord(u0, v0)
        ];

        byte rots = 2;

        for (int r = 0; r < rots; r++)
        {
            float xa = Mathf.Sin((float)r * Mathf.PI / (float)rots + Mathf.PI * 0.25f) * 0.5f;
            float ya = Mathf.Cos((float)r * Mathf.PI / (float)rots + Mathf.PI * 0.25f) * 0.5f;

            float x0 = (float)position.X + 0.5f - xa;
            float y0 = (float)position.Y + 0.5f - ya;
            float z0 = (float)position.Z + 0.0f;

            float x1 = (float)position.X + 0.5f + xa;
            float y1 = (float)position.Y + 0.5f + ya;
            float z1 = (float)position.Z + 1.0f;

            mesh.AddQuad(
                [
                    new Vector3(x0, y0, z0),
                    new Vector3(x1, y1, z0),
                    new Vector3(x1, y1, z1),
                    new Vector3(x0, y0, z1)
                ],
                colors,
                texCoords
            );

            mesh.AddQuad(
                [
                    new Vector3(x1, y1, z0),
                    new Vector3(x0, y0, z0),
                    new Vector3(x0, y0, z1),
                    new Vector3(x1, y1, z1)
                ],
                colors,
                texCoords
            );
        }
    }
}
