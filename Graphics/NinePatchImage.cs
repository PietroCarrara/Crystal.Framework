using System;
using System.Collections.Generic;
using Crystal.Framework.UI;

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
        public int? BorderThickness;

        public NinePatchImage(
            IDrawable texture,
            Point topLeft,
            Point topRight,
            Point bottomLeft,
            Point bottomRight,
            int? borderThickness = null)
        {
            this.Texture = texture;

            this.topLeft = topLeft;
            this.topRight = topRight;
            this.bottomLeft = bottomLeft;
            this.bottomRight = bottomRight;

            this.BorderThickness = borderThickness;
        }

        /// <summary>
        /// Calculates the border thickness for a given area this image would be drawn
        /// If BorderThickness has a value, just returns that. Else, scales the borders
        /// </summary>
        public int CalculateBorder(Point areaSize)
        {
            // Scale border noramlly
            if (!BorderThickness.HasValue)
            {
                return Math.Max(
                    (topLeft.X * areaSize.X / Texture.Width),
                    (topLeft.Y * areaSize.Y / Texture.Height)
                );
            }

            return BorderThickness.Value;
        }

        /// <summary>
        /// Gets the information needed to draw this image onto a area.
        /// </summary>
        /// <returns>The steps necessary to draw this image with origin (0, 0)</returns>
        public IEnumerable<(TextureSlice source, TextureSlice destination)> DrawingPrimitives(TextureSlice area)
        {
            var borderThickness = CalculateBorder(area.Size);

            // Top Left
            yield return (
                TextureSlice.FromTwoPoints(
                    (0, 0),
                    topLeft
                ),
                new TextureSlice(
                    0,
                    0,
                    borderThickness,
                    borderThickness
                ) + area.TopLeft
            );

            // Top Center
            yield return (
                TextureSlice.FromTwoPoints(
                    (topLeft.X, 0),
                    (topRight.X, topRight.Y)
                ),
                new TextureSlice(
                    borderThickness,
                    0,
                    squeezeCenterWidth(area.Width, borderThickness),
                    borderThickness
                ) + area.TopLeft
            );

            // Top Right
            yield return (
                TextureSlice.FromTwoPoints(
                    (topRight.X, 0),
                    (Texture.Width, topRight.Y)
                ),
                new TextureSlice(
                    area.Width - borderThickness,
                    0,
                    borderThickness,
                    borderThickness
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
                    borderThickness,
                    borderThickness,
                    squeezeCenterHeight(area.Height, borderThickness)
                ) + area.TopLeft
            );

            // Center
            yield return (
                TextureSlice.FromTwoPoints(
                    topLeft,
                    bottomRight
                ),
                new TextureSlice(
                    borderThickness,
                    borderThickness,
                    squeezeCenterWidth(area.Width, borderThickness),
                    squeezeCenterHeight(area.Height, borderThickness)
                ) + area.TopLeft
            );

            // Center Right
            yield return (
                TextureSlice.FromTwoPoints(
                    topRight,
                    (Texture.Width, bottomRight.Y)
                ),
                new TextureSlice(
                    area.Width - borderThickness,
                    borderThickness,
                    borderThickness,
                    squeezeCenterHeight(area.Height, borderThickness)
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
                    area.Height - borderThickness,
                    borderThickness,
                    borderThickness
                ) + area.TopLeft
            );

            // Bottom Center
            yield return (
                TextureSlice.FromTwoPoints(
                    (bottomLeft.X, bottomLeft.Y),
                    (bottomRight.X, Texture.Height)
                ),
                new TextureSlice(
                    borderThickness,
                    area.Height - borderThickness,
                    squeezeCenterWidth(area.Width, borderThickness),
                    borderThickness
                ) + area.TopLeft
            );

            // Bottom Center
            yield return (
                TextureSlice.FromTwoPoints(
                    bottomRight,
                    (Texture.Width, Texture.Height)
                ),
                new TextureSlice(
                    area.Width - borderThickness,
                    area.Height - borderThickness,
                    borderThickness,
                    borderThickness
                ) + area.TopLeft
            );
        }

        private int squeezeCenterWidth(int totalWidth, int borderThickness)
        {
            // total Width of the image - total width of the corners
            return totalWidth - 2 * borderThickness;
        }

        private int squeezeCenterHeight(int totalHeight, int borderThickness)
        {
            // total height of the image - total height of the corners
            return totalHeight - 2 * borderThickness;
        }

        public void Dispose()
        {
            this.Texture.Dispose();
        }
    }
}