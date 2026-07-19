using RubyDung.Levels.Blocks;
using RubyDung.Mathematics;
using RubyDung.Meshing;
using RubyDung.Physics;
using RubyDung.Rendering;

namespace RubyDung.Levels;

public class Chunk
{
    private readonly Level _level;

    private readonly Vector3Int _position;

    private readonly int _size = LevelRenderer.CHUNK_SIZE;

    private MeshQuad _mesh = new MeshQuad();

    private MeshRender _meshRender;

    public Chunk(Level level, Vector3Int postions)
    {
        _level = level;

        _position = postions;

        _meshRender = new MeshRender();
    }

    public void Init()
    {
        _mesh = new MeshQuad();
        _mesh.Clear();

        for (int x = 0; x < _size; x++)
        {
            for (int y = 0; y < _size; y++)
            {
                for (int z = 0; z < _size; z++)
                {
                    Vector3Int position = new(
                        x + _position.X, 
                        y + _position.Y, 
                        z + _position.Z
                    );

                    int blockID = _level.GetBlock(position);

                    if (blockID > 0)
                    {
                        Block.Blocks[blockID].Init(_mesh, _level, position);
                    }
                }
            }
        }

        _meshRender.Mesh = _mesh;
    }

    public void Render(Shader shader)
    {
        _meshRender.Draw(shader);
    }
}
