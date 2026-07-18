using RubyDung.Mathematics;
using RubyDung.Meshing;

namespace RubyDung.Levels;

public enum BlockFace
{
    NegativeX = 0,
    PositiveX = 1,
    NegativeY = 2,
    PositiveY = 3,
    NegativeZ = 4,
    PositiveZ = 5
}

public class Block
{
    public static Block Rock = new Block(0);
    public static Block Grass = new Block(1);

    private int _tex = 0;

    public Block(int tex)
    {
        _tex = tex;
    }

    public void Init(MeshQuad mesh, Level level, Vector3Int position)
    {
        float x0 = (float)position.X + 0.0f;
        float y0 = (float)position.Y + 0.0f;
        float z0 = (float)position.Z + 0.0f;

        float x1 = (float)position.X + 1.0f;
        float y1 = (float)position.Y + 1.0f;
        float z1 = (float)position.Z + 1.0f;

        float c1 = 0.6f;
        float c2 = 0.8f;
        float c3 = 1.0f;

        float br;

        float u0 = (float)_tex / 16.0f;
        float v0 = 0.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        TexCoord[] texCoords = [
            new TexCoord(u0, v1),
            new TexCoord(u1, v1),
            new TexCoord(u1, v0),
            new TexCoord(u0, v0)
        ];

        if (!level.IsSolidBlock(new Vector3Int(
            position.X - 1, 
            position.Y,
            position.Z
        )))
        {
            br = level.GetBrightness(new Vector3Int(
                position.X - 1,
                position.Y,
                position.Z
            )) * c1;

            mesh.AddQuad(
                [
                    new Vector3(x0, y1, z0),
                    new Vector3(x0, y0, z0),
                    new Vector3(x0, y0, z1),
                    new Vector3(x0, y1, z1)
                ],
                [
                    new Color(br, br, br),
                    new Color(br, br, br),
                    new Color(br, br, br),
                    new Color(br, br, br)
                ],
                texCoords
            );
        }
        if (!level.IsSolidBlock(new Vector3Int(
            position.X + 1, 
            position.Y, 
            position.Z
        )))
        {
            br = level.GetBrightness(new Vector3Int(
                position.X + 1,
                position.Y,
                position.Z
            )) * c1;

            mesh.AddQuad(
                [
                    new Vector3(x1, y0, z0),
                    new Vector3(x1, y1, z0),
                    new Vector3(x1, y1, z1),
                    new Vector3(x1, y0, z1)
                ],
                [
                    new Color(br, br, br),
                    new Color(br, br, br),
                    new Color(br, br, br),
                    new Color(br, br, br)
                ],
                texCoords
            );
        }
        if (!level.IsSolidBlock(new Vector3Int(
            position.X, 
            position.Y - 1, 
            position.Z
        )))
        {
            br = level.GetBrightness(new Vector3Int(
                position.X,
                position.Y - 1,
                position.Z
            )) * c2;

            mesh.AddQuad(
                [
                    new Vector3(x0, y0, z0),
                    new Vector3(x1, y0, z0),
                    new Vector3(x1, y0, z1),
                    new Vector3(x0, y0, z1)
                ],
                [
                    new Color(br, br, br),
                    new Color(br, br, br),
                    new Color(br, br, br),
                    new Color(br, br, br)
                ],
                texCoords
            );
        }
        if (!level.IsSolidBlock(new Vector3Int(
            position.X, 
            position.Y + 1, 
            position.Z
        )))
        {
            br = level.GetBrightness(new Vector3Int(
                position.X,
                position.Y + 1,
                position.Z
            )) * c2;

            mesh.AddQuad(
                [            
                    new Vector3(x1, y1, z0),
                    new Vector3(x0, y1, z0),
                    new Vector3(x0, y1, z1),
                    new Vector3(x1, y1, z1)
                ],
                [
                    new Color(br, br, br),
                    new Color(br, br, br),
                    new Color(br, br, br),
                    new Color(br, br, br)
                ],
                texCoords
            );
        }
        if (!level.IsSolidBlock(new Vector3Int(
            position.X, 
            position.Y, 
            position.Z - 1
        )))
        {
            br = level.GetBrightness(new Vector3Int(
                position.X,
                position.Y,
                position.Z - 1
            )) * c3;

            mesh.AddQuad(
                [
                    new Vector3(x0, y1, z0),
                    new Vector3(x1, y1, z0),
                    new Vector3(x1, y0, z0),
                    new Vector3(x0, y0, z0)
                ],
                [
                    new Color(br, br, br),
                    new Color(br, br, br),
                    new Color(br, br, br),
                    new Color(br, br, br)
                ],
                texCoords
            );
        }
        if (!level.IsSolidBlock(new Vector3Int(
            position.X, 
            position.Y, 
            position.Z + 1
        )))
        {
            br = level.GetBrightness(new Vector3Int(
                position.X,
                position.Y,
                position.Z + 1
            )) * c3;

            mesh.AddQuad(
                [
                    new Vector3(x0, y0, z1),
                    new Vector3(x1, y0, z1),
                    new Vector3(x1, y1, z1),
                    new Vector3(x0, y1, z1)
                ],
                [
                    new Color(br, br, br),
                    new Color(br, br, br),
                    new Color(br, br, br),
                    new Color(br, br, br)
                ],
                texCoords
            );
        }
    }

    public void AddFace(MeshQuad mesh, Vector3Int position, BlockFace face)
    {
        float x0 = (float)position.X + 0.0f;
        float y0 = (float)position.Y + 0.0f;
        float z0 = (float)position.Z + 0.0f;

        float x1 = (float)position.X + 1.0f;
        float y1 = (float)position.Y + 1.0f;
        float z1 = (float)position.Z + 1.0f;

        if (face == BlockFace.NegativeX)
        {
            mesh.AddQuad(
                [
                    new Vector3(x0, y1, z0),
                    new Vector3(x0, y0, z0),
                    new Vector3(x0, y0, z1),
                    new Vector3(x0, y1, z1)
                ]
            );
        }
        if (face == BlockFace.PositiveX)
        {
            mesh.AddQuad(
                [
                    new Vector3(x1, y0, z0),
                    new Vector3(x1, y1, z0),
                    new Vector3(x1, y1, z1),
                    new Vector3(x1, y0, z1)
                ]
            );
        }
        if (face == BlockFace.NegativeY)
        {
            mesh.AddQuad(
                [
                    new Vector3(x0, y0, z0),
                    new Vector3(x1, y0, z0),
                    new Vector3(x1, y0, z1),
                    new Vector3(x0, y0, z1)
                ]
            );
        }
        if (face == BlockFace.PositiveY)
        {
            mesh.AddQuad(
                [            
                    new Vector3(x1, y1, z0),
                    new Vector3(x0, y1, z0),
                    new Vector3(x0, y1, z1),
                    new Vector3(x1, y1, z1)
                ]
            );
        }
        if (face == BlockFace.NegativeZ)
        {
            mesh.AddQuad(
                [
                    new Vector3(x0, y1, z0),
                    new Vector3(x1, y1, z0),
                    new Vector3(x1, y0, z0),
                    new Vector3(x0, y0, z0)
                ]
            );
        }
        if (face == BlockFace.PositiveZ)
        {
            mesh.AddQuad(
                [
                    new Vector3(x0, y0, z1),
                    new Vector3(x1, y0, z1),
                    new Vector3(x1, y1, z1),
                    new Vector3(x0, y1, z1)
                ]
            );
        }
    }
}
