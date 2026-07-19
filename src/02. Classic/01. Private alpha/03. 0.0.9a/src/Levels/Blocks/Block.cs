using System.Runtime.InteropServices.Marshalling;
using Minecraft.Mathematics;
using Minecraft.Meshing;
using Minecraft.Physics;

namespace Minecraft.Levels.Blocks;

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
    public static readonly Block[] Blocks = new Block[256];

    public static Block Empty = null!;
    public static Block Rock = new Block(id: 1, tex: 1);
    public static Block Grass = new BlockGrass(id: 2);
    public static Block Dirt = new Block(id: 3, tex: 2);
    public static Block StoneBrick = new Block(id: 4, tex: 16);
    public static Block Wood = new Block(id: 5, tex: 4);
    public static Block Bush = new BlockBush(id: 6);

    public readonly int ID;

    protected int _tex = 0;

    public Block(int id)
    {
        Blocks[id] = this;
        ID = id;
    }

    public Block(int id, int tex) : this(id)
    {
        _tex = tex;
    }

    public virtual bool IsSolide()
    {
        return true;
    }

    public virtual void Tick(Level level, Vector3Int position, Random random)
    {
        
    }

    public virtual AABB GetBounds(Vector3Int position)
    {
        return new AABB(
            new Vector3(position.X, position.Y, position.Z),
            new Vector3(position.X + 1, position.Y + 1, position.Z + 1)
        );
    }

    public virtual void Init(MeshQuad mesh, Level level, Vector3Int position)
    {
        int x = position.X;
        int y = position.Y;
        int z = position.Z;

        float c1 = 0.6f;
        float c2 = 0.8f;
        float c3 = 1.0f;

        float br;

        if (ShoulAddFace(level, new Vector3Int(x - 1, y, z)))
        {
            br = level.GetBrightness(new Vector3Int(x - 1, y, z)) * c1;
            Color[] colors = new Color[4];

            for (int i = 0; i < 4; i++)
            {
                colors[i] = new Color(br, br, br);
            }

            AddFaceWithTexture(mesh, position, BlockFace.NegativeX, colors);
        }
        if (ShoulAddFace(level, new Vector3Int(x + 1, y, z)))
        {
            br = level.GetBrightness(new Vector3Int(x + 1, y, z)) * c1;
            Color[] colors = new Color[4];

            for (int i = 0; i < 4; i++)
            {
                colors[i] = new Color(br, br, br);
            }

            AddFaceWithTexture(mesh, position, BlockFace.PositiveX, colors);
        }
        if (ShoulAddFace(level, new Vector3Int(x, y - 1, z)))
        {
            br = level.GetBrightness(new Vector3Int(x, y - 1, z)) * c2;
            Color[] colors = new Color[4];

            for (int i = 0; i < 4; i++)
            {
                colors[i] = new Color(br, br, br);
            }

            AddFaceWithTexture(mesh, position, BlockFace.NegativeY, colors);
        }
        if (ShoulAddFace(level, new Vector3Int(x, y + 1, z)))
        {
            br = level.GetBrightness(new Vector3Int(x, y + 1, z)) * c2;
            Color[] colors = new Color[4];

            for (int i = 0; i < 4; i++)
            {
                colors[i] = new Color(br, br, br);
            }

            AddFaceWithTexture(mesh, position, BlockFace.PositiveY, colors);
        }
        if (ShoulAddFace(level, new Vector3Int(x, y, z - 1)))
        {
            br = level.GetBrightness(new Vector3Int(x, y, z - 1)) * c3;
            Color[] colors = new Color[4];

            for (int i = 0; i < 4; i++)
            {
                colors[i] = new Color(br, br, br);
            }

            AddFaceWithTexture(mesh, position, BlockFace.NegativeZ, colors);
        }
        if (ShoulAddFace(level, new Vector3Int(x, y, z + 1)))
        {
            br = level.GetBrightness(new Vector3Int(x, y, z + 1)) * c3;
            Color[] colors = new Color[4];

            for (int i = 0; i < 4; i++)
            {
                colors[i] = new Color(br, br, br);
            }

            AddFaceWithTexture(mesh, position, BlockFace.PositiveZ, colors);
        }
    }

    private bool ShoulAddFace(Level level, Vector3Int position)
    {
        return !level.IsSolidBlock(position);
    }

    protected virtual int GetTexture(BlockFace face)
    {
        return _tex;
    }

    public void AddFaceWithTexture(MeshQuad mesh, Vector3Int position, BlockFace face, Color[]? colors = null)
    {
        float x0 = (float)position.X + 0.0f;
        float y0 = (float)position.Y + 0.0f;
        float z0 = (float)position.Z + 0.0f;

        float x1 = (float)position.X + 1.0f;
        float y1 = (float)position.Y + 1.0f;
        float z1 = (float)position.Z + 1.0f;

        Color[] colors1 = colors ?? [];

        int tex = GetTexture(face);

        float u0 = (float)(tex % 16) / 16.0f;
        float v0 = (float)(tex / 16) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        TexCoord[] texCoords = [
            new TexCoord(u0, v1),
            new TexCoord(u1, v1),
            new TexCoord(u1, v0),
            new TexCoord(u0, v0)
        ];

        if (face == BlockFace.NegativeX)
        {
            mesh.AddQuad(
                [
                    new Vector3(x0, y1, z0),
                    new Vector3(x0, y0, z0),
                    new Vector3(x0, y0, z1),
                    new Vector3(x0, y1, z1)
                ],
                colors1,
                texCoords
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
                ],
                colors1,
                texCoords
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
                ],
                colors1,
                texCoords
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
                ],
                colors1,
                texCoords
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
                ],
                colors1,
                texCoords
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
                ],
                colors1,
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
