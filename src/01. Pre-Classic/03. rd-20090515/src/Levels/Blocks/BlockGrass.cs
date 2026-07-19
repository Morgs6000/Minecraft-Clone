using RubyDung.Mathematics;

namespace RubyDung.Levels.Blocks;

public class BlockGrass : Block
{
    public BlockGrass(int id) : base(id)
    {
        _tex = 3;
    }

    protected override int GetTexture(BlockFace face)
    {
        if (face == BlockFace.PositiveZ)
        {
            return 0;
        }
        if (face == BlockFace.NegativeZ)
        {
            return 2;
        }

        return 3;
    }

    public override void Tick(Level level, Vector3Int position, Random random)
    {
        if (!level.IsLit(position))
        {
            level.SetBlock(position, Block.Dirt.ID);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                int xt = position.X + random.Next(3) - 1;
                int yt = position.Y + random.Next(3) - 1;
                int zt = position.Z + random.Next(5) - 3;

                if (level.GetBlock(new Vector3Int(xt, yt, zt)) == Block.Dirt.ID && 
                    level.IsLit(new Vector3Int(xt, yt, zt)))
                {
                    level.SetBlock(new Vector3Int(xt, yt, zt), Block.Grass.ID);
                }
            }
        }
    }
}
