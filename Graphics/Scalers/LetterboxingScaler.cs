using System;
using System.Numerics;

namespace Crystal.Framework.Graphics.Scalers
{
    public class LetterboxingScaler : IScaler
    {
        public TextureSlice Scale(TextureSlice container, TextureSlice fitting)
        {
            var screenSize = container.Size;

            var scaleX = fitting.Size.X / (float)screenSize.X;
            var scaleY = fitting.Size.Y / (float)screenSize.Y;
            var scale = Math.Max(scaleY, scaleX);

            var width = (int)(fitting.Size.X / scale);
            var height = (int)(fitting.Size.Y / scale);

            return new TextureSlice(
                (screenSize.X - width) / 2,
                (screenSize.Y - height) / 2,
                width,
                height
            );
        }

        public Matrix4x4 ScaleMatrix(TextureSlice container, TextureSlice fitting)
        {
            var screenSize = container.Size;

            var scaleX = fitting.Size.X / (float)screenSize.X;
            var scaleY = fitting.Size.Y / (float)screenSize.Y;
            var scale = Math.Max(scaleY, scaleX);

            var width = (int)(fitting.Size.X / scale);
            var height = (int)(fitting.Size.Y / scale);

            var matrix = Matrix4x4.CreateScale(
                1 / scale,
                1 / scale,
                1
            );

            matrix *= Matrix4x4.CreateTranslation(
                (screenSize.X - width) / 2,
                (screenSize.Y - height) / 2,
                0
            );

            return matrix;
        }
    }
}