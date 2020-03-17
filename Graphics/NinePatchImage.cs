using System.Collections.Generic;
namespace Crystal.Framework.Graphics
{
    public class NinePatchImage : IDrawable
    {
        public readonly IDrawable Texture;
        private readonly Point topLeft, topRight, bottomLeft, bottomRight;

        public int Width => this.Texture.Width;

        public int Height => this.Texture.Height;

        /// <summary>
        /// Value to multiply the border sizes
        /// </summary>
        public float BorderThickness;

        public NinePatchImage(
            IDrawable texture,
            Point topLeft,
            Point topRight,
            Point bottomLeft,
            Point bottomRight,
            float borderThickness = 1)
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
                new TextureSlice(0, 0, topLeft.X, topLeft.Y),
                new TextureSlice(
                    0,
                    0,
                    thick(topLeft.X),
                    thick(topLeft.Y)
                ) + area.TopLeft
            );

            // Top Center
            yield return (
                TextureSlice.FromTwoPoints(
                    (topLeft.X, 0),
                    (topRight.X, topRight.Y)
                ),
                new TextureSlice(
                    thick(topLeft.X),
                    0,
                    squeezeCenterWidth(area.Width),
                    thick(topLeft.Y)
                ) + area.TopLeft
            );

            // Top Right
            yield return (
                new TextureSlice(
                    topRight.X,
                    0,
                    Texture.Width - topRight.X,
                    topRight.Y
                ),
                new TextureSlice(
                    area.Width - thick(Texture.Width - topRight.X),
                    0,
                    thick(Texture.Width - topRight.X),
                    thick(topRight.Y)
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
                    thick(topLeft.Y),
                    thick(topLeft.X),
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
                    thick(topLeft.X),
                    thick(topLeft.Y),
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
                    area.Width - thick(Texture.Width - topRight.X),
                    thick(topRight.Y),
                    thick(Texture.Width - topRight.X),
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
                    area.Height - thick(Texture.Height - bottomLeft.Y),
                    thick(bottomLeft.X),
                    thick(Texture.Height - bottomLeft.Y)
                ) + area.TopLeft
            );

            // Bottom Center
            yield return (
                TextureSlice.FromTwoPoints(
                    (bottomLeft.X, bottomLeft.Y),
                    (bottomRight.X, Texture.Height)
                ),
                new TextureSlice(
                    thick(bottomLeft.X),
                    area.Height - thick(Texture.Height - bottomLeft.Y),
                    squeezeCenterWidth(area.Width),
                    thick(Texture.Height - bottomLeft.Y)
                ) + area.TopLeft
            );

            // Bottom Center
            yield return (
                TextureSlice.FromTwoPoints(
                    bottomRight,
                    (Texture.Width, Texture.Height)
                ),
                new TextureSlice(
                    area.Width - thick(Texture.Width - bottomRight.X),
                    area.Height - thick(Texture.Height - bottomRight.Y),
                    thick(Texture.Width - bottomRight.X),
                    thick(Texture.Height - bottomRight.Y)
                ) + area.TopLeft
            );
        }

        /// <summary>
        /// Applies the border thickness to some value
        /// </summary>
        private int thick(int value)
        {
            return (int)(value * this.BorderThickness);
        }

        /// <summary>
        /// Applies the border thickness to some point
        /// </summary>
        private Point thick(Point p)
        {
            return new Point(thick(p.X), thick(p.Y));
        }

        private int squeezeCenterWidth(int totalWidth)
        {
            // total Width of the image - total width of the corners
            return totalWidth - thick(topLeft.X) - thick(Texture.Width - topRight.X);
        }

        private int squeezeCenterHeight(int totalHeight)
        {
            // total height of the image - total height of the corners
            return totalHeight - thick(topLeft.Y) - thick(Texture.Height - bottomLeft.Y);
        }

        public void Dispose()
        {
            this.Texture.Dispose();
        }
    }
}