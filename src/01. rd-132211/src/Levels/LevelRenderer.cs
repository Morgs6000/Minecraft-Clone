using RubyDung.Mathematics;
using RubyDung.Rendering;

namespace RubyDung.Levels;

public class LevelRenderer : LevelListener
{
    public static readonly int CHUNK_SIZE = 16;

    private Level _level;

    private Chunk[] _chunks;

    private int _xChunks;
    private int _yChunks;
    private int _zChunks;

    public LevelRenderer(Level level)
    {
        _level = level;
        _level.AddListener(this);

        _xChunks = level.Width  / CHUNK_SIZE;
        _yChunks = level.Depth  / CHUNK_SIZE;
        _zChunks = level.Height / CHUNK_SIZE;

        _chunks = new Chunk[_xChunks * _yChunks * _zChunks];

        for (int x = 0; x < _xChunks; x++)
        {
            for (int y = 0; y < _yChunks; y++)
            {
                for (int z = 0; z < _zChunks; z++)
                {
                    Vector3Int positon = new(
                        x * CHUNK_SIZE,
                        y * CHUNK_SIZE,
                        z * CHUNK_SIZE
                    );

                    int i = (x + y * _xChunks) * _zChunks + z;
                    _chunks[i] = new Chunk(level, positon);
                }
            }
        }
    }

    public void Init()
    {
        foreach (Chunk chunk in _chunks)
        {
            chunk.Init();
        }
    }

    public void Render(Shader shader)
    {
        foreach (Chunk chunk in _chunks)
        {
            chunk.Render(shader);
        }
    }

    private void SetChunk(Vector3Int min, Vector3Int max)
    {
        // Converte para índices de chunk: inferior com Floor, superior com Ceil
        int x0 = Mathf.FloorToInt((float)min.X / CHUNK_SIZE);
        int y0 = Mathf.FloorToInt((float)min.Y / CHUNK_SIZE);
        int z0 = Mathf.FloorToInt((float)min.Z / CHUNK_SIZE);

        int x1 = Mathf.FloorToInt((float)max.X / CHUNK_SIZE);
        int y1 = Mathf.FloorToInt((float)max.Y / CHUNK_SIZE);
        int z1 = Mathf.FloorToInt((float)max.Z / CHUNK_SIZE);

        // Limita os índices ao intervalo válido
        x0 = Mathf.Max(x0, 0);
        y0 = Mathf.Max(y0, 0);
        z0 = Mathf.Max(z0, 0);

        x1 = Mathf.Min(x1 + 1, _xChunks);
        y1 = Mathf.Min(y1 + 1, _yChunks);
        z1 = Mathf.Min(z1 + 1, _zChunks);

        // Reconstroi os chunks no intervalo [cx0, cx1) etc.
        for (int x = x0; x < x1; x++)
        {
            for (int y = y0; y < y1; y++)
            {
                for (int z = z0; z < z1; z++)
                {
                    int i = (x + y * _xChunks) * _zChunks + z;
                    _chunks[i].Init();
                }
            }
        }
    }

    public void BlockChanged(Vector3Int position)
    {
        SetChunk(
            new Vector3Int(position.X - 1, position.Y - 1, position.Z - 1),
            new Vector3Int(position.X + 1, position.Y + 1, position.Z + 1)
        );
    }

    public void LightColumnChanged(int x, int y, int z0, int z1)
    {
        SetChunk(
            new Vector3Int(x - 1, y - 1, z0 - 1),
            new Vector3Int(x - 1, y - 1, z1 - 1)
        );
    }

    public void AllChanged()
    {
        SetChunk(
            new Vector3Int(0, 0, 0),
            new Vector3Int(_level.Width, _level.Depth, _level.Height)
        );
    }
}
