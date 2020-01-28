using System;

namespace Crystal.Framework
{
    public struct Vector3
    {
        public float X, Y, Z;

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3(float xyz)
        {
            this.X = xyz;
            this.Y = xyz;
            this.Z = xyz;
        }

        public static Vector3 Zero
        {
            get => new Vector3(0);
        }
        
        public static Vector3 One
        {
            get => new Vector3(1);
        }

        public static Vector3 operator -(Vector3 left, Vector2 right)
        {
            return new Vector3(
                left.X - right.X,
                left.Y - right.Y,
                left.Z
            );
        }

        public static implicit operator Vector3(Vector2 v)
        {
            return new Vector3(v.X, v.Y, 0);
        }
    }
}