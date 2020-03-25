using System;
using System.Collections.Generic;
namespace Crystal.Framework.Graphics
{
    public class NinePatchImage : IDisposable
    {
        public readonly IDrawable Texture;
        private readonly Point topLeft, topRight, bottomLeft, bottomRight;

        /// <summary>
        /// Value of the border sizes
        /// X is for the left and right borders
        /// Y is for the top and bottom borders
        /// </summary>
        public Point BorderThickness;

        public NinePatchImage(
            IDrawable texture,
            Point topLeft,
            Point topRight,
            Point bottomLeft,
            Point bottomRight,
            Point borderThickness)
        {
            this.Texture = texture;

            this.topLeft = topLeft;
            this.topRight = topRight;
            this.bottomLeft = bottomLeft;
            this.bottomRight = bottomRight;

            this.BorderThickness = borderThickness;
        }

        /// <summary>
        /// Gets the information needed to draw this image onto a area.
        /// </summary>
        /// <returns>The steps necessary to draw this image with origin (0, 0)</returns>
        public IEnumerable<(TextureSlice source, TextureSlice destination)> DrawingPrimitives(TextureSlice area)
        {
            // Top Left
            yield return (
                TextureSlice.FromTwoPoints(
                    (0, 0),
                    topLeft
                ),
                new TextureSlice(
                    0,
                    0,
                    BorderThickness.X,
                    BorderThickness.Y
                ) + area.TopLeft
            );

            // Top Center
            yield return (
                TextureSlice.FromTwoPoints(
                    (topLeft.X, 0),
                    (topRight.X, topRight.Y)
                ),
                new TextureSlice(
                    BorderThickness.X,
                    0,
                    squeezeCenterWidth(area.Width),
                    BorderThickness.Y
                ) + area.TopLeft
            );

            // Top Right
            yield return (
                TextureSlice.FromTwoPoints(
                    (topRight.X, 0),
                    (Texture.Width, topRight.Y)
                ),
                new TextureSlice(
                    area.Width - BorderThickness.X,
                    0,
                    BorderThickness.X,
                    BorderThickness.Y
                ) + area.TopLeft
            );

            // Center Left
            yield return (
                TextureSlice.FromTwoPoints(
                    (0, topLeft.Y),
                    bottomLeft
                ),
                new TextureSlice(
                    0,
                    BorderThickness.Y,
                    BorderThickness.X,
                    squeezeCenterHeight(area.Height)
                ) + area.TopLeft
            );

            // Center
            yield return (
                TextureSlice.FromTwoPoints(
                    topLeft,
                    bottomRight
                ),
                new TextureSlice(
                    BorderThickness.X,
                    BorderThickness.Y,
                    squeezeCenterWidth(area.Width),
                    squeezeCenterHeight(area.Height)
                ) + area.TopLeft
            );

            // Center Right
            yield return (
                TextureSlice.FromTwoPoints(
                    topRight,
                    (Texture.Width, bottomRight.Y)
                ),
                new TextureSlice(
                    area.Width - BorderThickness.X,
                    BorderThickness.Y,
                    BorderThickness.X,
                    squeezeCenterHeight(area.Height)
                ) + area.TopLeft
            );

            // Bottom Left
            yield return (
                TextureSlice.FromTwoPoints(
                    (0, bottomLeft.Y),
                    (bottomLeft.X, Texture.Height)
                ),
                new TextureSlice(
                    0,
                    area.Height - BorderThickness.Y,
                    BorderThickness.X,
                    BorderThickness.Y
                ) + area.TopLeft
            );

            // Bottom Center
            yield return (
                TextureSlice.FromTwoPoints(
                    (bottomLeft.X, bottomLeft.Y),
                    (bottomRight.X, Texture.Height)
                ),
                new TextureSlice(
                    BorderThickness.X,
                    area.Height - BorderThickness.Y,
                    squeezeCenterWidth(area.Width),
                    BorderThickness.Y
                ) + area.TopLeft
            );

            // Bottom Center
            yield return (
                TextureSlice.FromTwoPoints(
                    bottomRight,
                    (Texture.Width, Texture.Height)
                ),
                new TextureSlice(
                    area.Width - BorderThickness.X,
                    area.Height - BorderThickness.Y,
                    BorderThickness.X,
                    BorderThickness.Y
                ) + area.TopLeft
            );
        }

        private int squeezeCenterWidth(int totalWidth)
        {
            // total Width of the image - total width of the corners
            return totalWidth - 2 * BorderThickness.X;
        }

        private int squeezeCenterHeight(int totalHeight)
        {
            // total height of the image - total height of the corners
            return totalHeight - 2 * BorderThickness.Y;
        }

        public void Dispose()
        {
            this.Texture.Dispose();
        }
    }
}