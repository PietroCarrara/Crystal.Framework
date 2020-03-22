using System.Numerics;

namespace Crystal.Framework
{
    public static class Matrix4x4Extensions
    {
        public static Vector2 Transform(this Matrix4x4 self, Vector2 v)
        {
            return Vector2.Transform(v, self);
        }

        public static Matrix4x4 Invert(this Matrix4x4 self)
        {
            Matrix4x4.Invert(self, out var res);
            return res;
        }
    }
}