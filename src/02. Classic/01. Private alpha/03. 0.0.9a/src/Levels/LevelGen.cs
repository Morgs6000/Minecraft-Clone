using Minecraft.Levels.Blocks;
using Minecraft.Mathematics;

namespace Minecraft.Levels;

public class LevelGen
{
    private int _widht;
    private int _depth;
    private int _height;

    private Random _random = new Random();

    public LevelGen(int width, int depth, int height)
    {
        _widht = width;
        _depth = depth;
        _height = height;
    }

    public byte[] GenerateMap()
    {
        int w = _widht;
        int d = _depth;
        int h = _height;

        int[] heightmap1 = new NoiseMap(0).read(w, d);
        int[] heightmap2 = new NoiseMap(0).read(w, d);
        int[] cf = new NoiseMap(1).read(w, d);
        int[] rockMap = new NoiseMap(1).read(w, d);

        byte[] blocks = new byte[_widht * _depth * _height];

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < d; y++)
            {
                for (int z = 0; z < h; z++)
                {
                    int dh1 = heightmap1[x + y * _widht];
                    int dh2 = heightmap2[x + y * _widht];
                    int cfh = cf[x + y * _widht];
                    if (cfh < 128)
                    {
                        dh2 = dh1;
                    }

                    int dh = dh1;
                    if (dh2 > dh1)
                    {
                        dh = dh2;
                    }

                    dh = dh / 8 + h / 3;
                    int rh = rockMap[x + y * _widht] / 8 + h / 3;
                    if (rh > dh - 2)
                    {
                        rh = dh - 2;
                    }

                    int id = 0;

                    if (z == dh)
                    {
                        id = Block.Grass.ID;
                    }
                    if (z < dh)
                    {
                        id = Block.Dirt.ID;
                    }
                    if (z <= rh)
                    {
                        id = Block.Rock.ID;
                    }

                    int i = (y * _height + z) * _widht + x;
                    blocks[i] = (byte)id;
                }
            }
        }

        int count = w * d * h  / 256 / 64;

        for (int i = 0; i < count; i++)
        {
            float x = (float)_random.NextDouble() * (float)w;
            float y = (float)_random.NextDouble() * (float)d;
            float z = (float)_random.NextDouble() * (float)h;

            int length = (int)(_random.NextDouble() + _random.NextDouble() * 150.0f);

            float dir1 = (float)(_random.NextDouble() * Mathf.PI * 2.0f);
            float dira1 = 0.0f;

            float dir2 = (float)(_random.NextDouble() * Mathf.PI * 2.0f);
            float dira2 = 0.0f;

            for (int l = 0; l < length; l++)
            {
                x = (float)(x + Mathf.Sin(dir1) * Mathf.Cos(dir2));
                y = (float)(y + Mathf.Cos(dir1) * Mathf.Cos(dir2));
                z = (float)(z + Mathf.Sin(dir2));

                dir1 += dira1 * 0.2f;
                dira1 *= 0.9f;
                dira1 += (float)_random.NextDouble() - (float)_random.NextDouble();

                dir2 += dira2 * 0.5f;
                dira2 *= 0.5f;
                dira2 += (float)_random.NextDouble() - (float)_random.NextDouble();

                float size = (float)(Mathf.Sin(l * Mathf.PI / length) * 2.5f + 1.0f);

                for (int xx = (int)(x - size); xx <= (int)(x + size); xx++)
                {
                    for (int yy = (int)(y - size); yy <= (int)(y + size); yy++)
                    {
                        for (int zz = (int)(z - size); zz <= (int)(z + size); zz++)
                        {
                            float xd = (float)xx - x;
                            float yd = (float)yy - y;
                            float zd = (float)zz - z;

                            float dd = xd * xd + yd * yd * 2.0f + zd * zd;

                            if (dd < size * size &&
                                xx >= 1 && xx < _widht - 1 &&
                                yy >= 1 && yy < _depth - 1 &&
                                zz >= 1 && zz < _height - 1)
                            {
                                int ii = (yy * _height + zz) * _widht + xx;

                                if (blocks[ii] == Block.Rock.ID)
                                {
                                    blocks[ii] = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        return blocks;
    }
}
