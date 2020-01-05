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
            return new Matrix4(new float[,]
            {
                { x, 0, 0, 0 },
                { 0, y, 0, 0 },
                { 0, 0, z, 0 },
                { 0, 0, 0, 1 },
            });
        }
        
        private Matrix4x4 matrix;

        public float[,] Data
        {
            get => new float[,] 
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
                data[2, 1],
                data[3, 2],
                data[3, 3]
            );
        }

        private Matrix4(Matrix4x4 data)
        {
            this.matrix = data;
        }
    }
}