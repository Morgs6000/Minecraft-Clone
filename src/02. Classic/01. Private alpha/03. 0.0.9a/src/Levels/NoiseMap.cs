namespace Minecraft.Levels;

public class NoiseMap
{
    Random random = new Random();
    int seed;
    int levels;
    int fuzz;

    public NoiseMap(int levels) 
	{
        this.seed = this.random.Next();
        this.levels = 0;
        this.fuzz = 16;
        this.levels = levels;
    }

    public int[] read(int width, int height) 
	{
        Random random = new Random();
        int[] tmp = new int[width * height];
        int level = this.levels;
        int step = width >> level;

        for(int y = 0; y < height; y += step) 
		{
            for(int x = 0; x < width; x += step) 
			{
                tmp[x + y * width] = (random.Next(256) - 128) * this.fuzz;
            }
        }

        for(step = width >> level; step > 1; step /= 2) {
            int val = 256 * (step << level);
            int ss = step / 2;

            for(int y = 0; y < height; y += step) 
			{
                for(int x = 0; x < width; x += step) 
				{
                    int ul = tmp[(x + 0) % width + (y + 0) % height * width];
                    int ur = tmp[(x + step) % width + (y + 0) % height * width];
                    int dl = tmp[(x + 0) % width + (y + step) % height * width];
                    int dr = tmp[(x + step) % width + (y + step) % height * width];
                    int m = (ul + dl + ur + dr) / 4 + random.Next(val * 2) - val;
                    tmp[x + ss + (y + ss) * width] = m;
                }
            }

            for(int y = 0; y < height; y += step) 
			{
                for(int x = 0; x < width; x += step) 
				{
                    int c = tmp[x + y * width];
                    int r = tmp[(x + step) % width + y * width];
                    int d = tmp[x + (y + step) % width * width];
                    int mu = tmp[(x + ss & width - 1) + (y + ss - step & height - 1) * width];
                    int ml = tmp[(x + ss - step & width - 1) + (y + ss & height - 1) * width];
                    int m = tmp[(x + ss) % width + (y + ss) % height * width];
                    int u = (c + r + m + mu) / 4 + random.Next(val * 2) - val;
                    int l = (c + d + m + ml) / 4 + random.Next(val * 2) - val;
                    tmp[x + ss + y * width] = u;
                    tmp[x + (y + ss) * width] = l;
                }
            }
        }

        int[] result = new int[width * height];

        for(int y = 0; y < height; ++y) 
		{
            for(int x = 0; x < width; ++x) 
			{
                result[x + y * width] = tmp[x % width + y % height * width] / 512 + 128;
            }
        }

        return result;
    }
}
