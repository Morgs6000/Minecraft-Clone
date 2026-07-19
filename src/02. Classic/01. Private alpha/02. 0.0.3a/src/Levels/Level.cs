using System.Drawing;
using System.IO.Compression;
using Minecraft.Levels.Blocks;
using Minecraft.Mathematics;
using Minecraft.Utilities;

namespace Minecraft.Levels;

public class Level
{
    public readonly int Width;

    public readonly int Depth;

    public readonly int Height;

    private byte[] _blocks;

    private int[] _lightDepths;

    private List<LevelListener> _levelListeners = [];

    private int _unprocessed = 0;

    private Random _ramdom = new Random();

    public Level(int w, int d, int h)
    {
        Width = w;
        Depth = d;
        Height = h;

        _blocks = new byte[w * d * h];
        _lightDepths = new int[w * d];

        bool mapLoaded = Load();
        if (!mapLoaded)
        {
            _blocks = new LevelGen(w, d, h).GenerateMap();
        }

        CalcLightDepths(0, 0, w, d);
    }

    public bool Load()
    {
        string path = SavePath();

        if (!File.Exists(path))
        {
            return false;
        }

        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (GZipStream gs = new GZipStream(fs, CompressionMode.Decompress))
            using (BinaryReader br = new BinaryReader(gs))
            {
                // O br.Read(array, offset, count) não existe diretamente na classe.
                // Para ler um array de bytes, use:
                byte[] buffer = br.ReadBytes(_blocks.Length);
                buffer.CopyTo(_blocks, 0);

                CalcLightDepths(0, 0, Width, Depth);

                for (int i = 0; i < _levelListeners.Count; i++)
                {
                    _levelListeners[i].AllChanged();
                }

                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.StackTrace);

            return false;
        }
    }

    public void Save()
    {
        string path = SavePath();

        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            using (GZipStream gs = new GZipStream(fs, CompressionMode.Compress))
            using (BinaryWriter bw = new BinaryWriter(gs))
            {
                bw.Write(_blocks);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.StackTrace);
        }
    }

    private string SavePath()
    {
        string path = "saves";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return Path.Combine(path, "level.dat");
    }

    public void CalcLightDepths(int x0, int y0, int x1, int y1)
    {
        for (int x = x0; x < x0 + x1; x++)
        {
            for (int y = y0; y < y0 + x1; y++)
            {
                int oldDepth = _lightDepths[x + y * Width];

                int z;

                for (z = Height - 1; z > 0 && !IsLightBlocker(new Vector3Int(x, y, z)); z--)
                {
                    
                }

                _lightDepths[x + y * Width] = z;

                if (oldDepth != z)
                {
                    int zl0 = oldDepth < z ? oldDepth : z;
                    int zl1 = oldDepth > z ? oldDepth : z;

                    for (int i = 0; i < _levelListeners.Count; i++)
                    {
                        _levelListeners[i].LightColumnChanged(x, y, zl0, zl1);
                    }
                }                
            }
        }
    }

    public bool IsLightBlocker(Vector3Int position)
    {
        return IsSolidBlock(position);
    }

    public void AddListener(LevelListener levelListener)
    {
        _levelListeners.Add(levelListener);
    }

    public void RemoveListener(LevelListener levelListener)
    {
        _levelListeners.Remove(levelListener);
    }

    public bool IsLit(Vector3Int position)
    {
        if (
            position.X >= 0 && position.X < Width &&
            position.Y >= 0 && position.Y < Depth &&
            position.Z >= 0 && position.Z < Height
        )
        {
            return position.Z >= _lightDepths[position.X + position.Y * Width];
        }

        return true;
    }

    public int GetBlock(Vector3Int position)
    {
        if (
            position.X >= 0 && position.X < Width &&
            position.Y >= 0 && position.Y < Depth &&
            position.Z >= 0 && position.Z < Height
        )
        {
            int i = (position.Y * Height + position.Z) * Width + position.X;
            return _blocks[i];
        }

        return 0;
    }

    public bool IsSolidBlock(Vector3Int position)
    {
        Block block = Block.Blocks[GetBlock(position)];

        if (block != null)
        {
            return block.IsSolide();
        }

        return false;
    }

    public float GetBrightness(Vector3Int position)
    {
        float dark = 0.4f;
        float light = 1.0f;

        if (
            position.X >= 0 && position.X < Width &&
            position.Y >= 0 && position.Y < Depth &&
            position.Z >= 0 && position.Z < Height
        )
        {
            if (position.Z < _lightDepths[position.X + position.Y * Width])
            {
                return dark;
            }
        }

        return light;
    }

    public void SetBlock(Vector3Int position, int type)
    {
        if (
            position.X >= 0 && position.X < Width &&
            position.Y >= 0 && position.Y < Depth &&
            position.Z >= 0 && position.Z < Height
        )
        {
            int i = (position.Y * Height + position.Z) * Width + position.X;
            _blocks[i] = (byte)type;

            CalcLightDepths(position.X, position.Y, 1, 1);

            for (int j = 0; j < _levelListeners.Count; j++)
            {
                _levelListeners[j].BlockChanged(position);
            }
        }
    }

    public void Tick()
    {
        _unprocessed += Width * Depth * Height;        
        int ticks = _unprocessed / 400;
        _unprocessed -= ticks * 400;

        for (int i = 0; i < ticks; i++)
        {
            int x = _ramdom.Next(Width);
            int y = _ramdom.Next(Depth);
            int z = _ramdom.Next(Height);

            Block block = Block.Blocks[GetBlock(new Vector3Int(x, y, z))];

            if (block != null)
            {
                block.Tick(this, new Vector3Int(x, y, z), _ramdom);
            }
        }        
    }
}
