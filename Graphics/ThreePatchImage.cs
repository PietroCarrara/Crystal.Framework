using System.Collections.Generic;

namespace Crystal.Framework.Graphics
{
    public class ThreePatchImage
    {
        public readonly IDrawable Texture;
        private readonly int left, right;

        public int? BorderThickness;

        /// <summary>
        /// Creates a ThreePatchImage
        /// </summary>
        /// <param name="texture">The texture of the image</param>
        /// <param name="left">The X coordinates where the stretching part of the image starts</param>
        /// <param name="right">The X coordinates where the stretching part of the image ends</param>
        public ThreePatchImage(IDrawable texture, int left, int right, int? borderThickness = null)
        {
            this.Texture = texture;
            this.left = left;
            this.right = right;
            this.BorderThickness = borderThickness;
        }

        public int CalculateBorder(Point area)
        {
            if (!BorderThickness.HasValue)
            {
                return (int)(left * area.Y / (float)Texture.Height);
            }

            return BorderThickness.Value;
        }

        /// <summary>
        /// Gets the information needed to draw this image onto a area.
        /// </summary>
        /// <returns>The steps necessary to draw this image with origin (0, 0)</returns>
        public IEnumerable<(TextureSlice source, TextureSlice destination)> DrawingPrimitives(TextureSlice area)
        {
            var borderThickness = this.CalculateBorder(area.Size);

            // Left border
            yield return (
                TextureSlice.FromTwoPoints(
                    (0, 0),
                    (left, Texture.Height)
                ),
                new TextureSlice(
                    0,
                    0,
                    borderThickness,
                    area.Height
                ) + area.TopLeft
            );

            // Center (stretching part)
            yield return (
                TextureSlice.FromTwoPoints(
                    (left, 0),
                    (right, Texture.Height)
                ),
                new TextureSlice(
                    borderThickness,
                    0,
                    area.Width - 2 * borderThickness,
                    area.Height
                ) + area.TopLeft
            );

            // Right border
            yield return (
                TextureSlice.FromTwoPoints(
                    (right, 0),
                    (Texture.Width, Texture.Height)
                ),
                new TextureSlice(
                    area.Width - borderThickness,
                    0,
                    borderThickness,
                    area.Height
                ) + area.TopLeft
            );
        }

    }
}