using System.Numerics;

namespace Minecraft.Physics;

public class AABB
{
    public Vector3 Min;
    public Vector3 Max;

    public AABB(Vector3 min, Vector3 max)
    {
        Min = min;
        Max = max;
    }

    public bool Intersects(AABB other)
    {
        if (Min.X < other.Max.X && Max.X > other.Min.X &&
            Min.Y < other.Max.Y && Max.Y > other.Min.Y &&
            Min.Z < other.Max.Z && Max.Z > other.Min.Z)
        {
            return true;
        }

        return false;
    }
}
