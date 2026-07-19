using Minecraft.Mathematics;

namespace Minecraft.Levels;

public interface LevelListener
{
    void BlockChanged(Vector3Int position);

    void LightColumnChanged(int x, int y, int z0, int z1);

    void AllChanged();
}
