using System.Numerics;

namespace Crystal.Framework
{
    public static class Vector2Etensions
    {
        public static Vector3 Vec3(this Vector2 self)
        {
            return new Vector3(
                self.X,
                self.Y,
                0
            );
        }
    }
}