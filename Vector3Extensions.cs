using System.Numerics;

namespace Crystal.Framework
{
    public static class Vector3Extensions
    {
        public static Vector2 Vec2(this Vector3 self)
        {
            return new Vector2(self.X, self.Y);
        }
    }
}