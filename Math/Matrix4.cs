using System.Linq;
using System;
using System.Numerics;

namespace Crystal.Framework.Math
{
    public class Matrix4
    {
        public static Matrix4 Identity
        {
            get => new Matrix4(Matrix4x4.Identity);
        }

        public static Matrix4 CreateScale(float x, float y, float z)
        {
            return new Matrix4(Matrix4x4.CreateScale(x, y, z));
        }

        public static Matrix4 CreateTranslation(float x, float y, float z)
        {
            return new Matrix4(Matrix4x4.CreateTranslation(x, y, z));
        }

        public static Matrix4 CreateTranslation(Vector3 translation)
        {
            return CreateTranslation(translation.X, translation.Y, translation.Z);
        }

        public static Matrix4 CreateTranslation(Vector2 translation, float z = 0)
        {
            return CreateTranslation(translation.X, translation.Y, z);
        }

        public static Matrix4 CreateRotationX(float radians)
        {
            return new Matrix4(Matrix4x4.CreateRotationX(radians));
        }

        public static Matrix4 CreateRotationY(float radians)
        {
            return new Matrix4(Matrix4x4.CreateRotationY(radians));
        }

        public static Matrix4 CreateRotationZ(float radians)
        {
            return new Matrix4(Matrix4x4.CreateRotationZ(radians));
        }

        private Matrix4x4 matrix;

        public float[,] ToFloatArray()
        {
            return new float[,]
            {
                {
                    this.matrix.M11,
                    this.matrix.M12,
                    this.matrix.M13,
                    this.matrix.M14,
                },
                {
                    this.matrix.M21,
                    this.matrix.M22,
                    this.matrix.M23,
                    this.matrix.M24,
                },
                {
                    this.matrix.M31,
                    this.matrix.M32,
                    this.matrix.M33,
                    this.matrix.M34,
                },
                {
                    this.matrix.M41,
                    this.matrix.M42,
                    this.matrix.M43,
                    this.matrix.M44,
                }
            };
        }

        public Matrix4()
        {
            this.matrix = Matrix4x4.Identity;
        }

        public Matrix4(float[,] data)
        {
            this.matrix = new Matrix4x4(
                data[0, 0],
                data[0, 1],
                data[0, 2],
                data[0, 3],
                data[1, 0],
                data[1, 1],
                data[1, 2],
                data[1, 3],
                data[2, 0],
                data[2, 1],
                data[2, 2],
                data[2, 3],
                data[3, 0],
                data[3, 1],
                data[3, 2],
                data[3, 3]
            );
        }

        private Matrix4(Matrix4x4 data)
        {
            this.matrix = data;
        }

        public override string ToString()
        {
            var m = this.matrix;

            var len = 0;
            foreach (var n in this.ToFloatArray())
            {
                if (n.ToString().Length > len)
                {
                    len = n.ToString().Length;
                }
            }

            Func<float, string> p = (n) => n.ToString().PadLeft(len);
            
            return $"({p(m.M11)}, {p(m.M12)}, {p(m.M13)}, {p(m.M14)})\n" +
                   $"({p(m.M21)}, {p(m.M22)}, {p(m.M23)}, {p(m.M24)})\n" +
                   $"({p(m.M31)}, {p(m.M32)}, {p(m.M33)}, {p(m.M34)})\n" +
                   $"({p(m.M41)}, {p(m.M42)}, {p(m.M43)}, {p(m.M44)})";
        }

        public Vector3 Scale
        {
            get => new Vector3(this.matrix.M11, this.matrix.M22, this.matrix.M33);
            set
            {
                this.matrix.M11 = value.X;
                this.matrix.M22 = value.Y;
                this.matrix.M33 = value.Z;
            }
        }

        public static Matrix4 operator *(Matrix4 left, Matrix4 right)
        {
            return new Matrix4(left.matrix * right.matrix);
        }
    }
}