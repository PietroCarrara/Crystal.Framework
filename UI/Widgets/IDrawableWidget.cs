using System.Numerics;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class IDrawableWidget : Widget
    {
        private IDrawable drawable;
        private TextureSlice? sourceRectangle;
        private ImageFit fit = ImageFit.Scale;

        public IDrawable Drawable
        {
            get => drawable;
            set
            {
                this.drawable = value;
                this.ChangeState();
            }
        }

        public TextureSlice? SourceRectangle
        {
            get => sourceRectangle;
            set
            {
                this.sourceRectangle = value;
                this.ChangeState();
            }
        }

        public ImageFit Fit
        {
            get => fit;
            set
            {
                this.fit = value;
                this.ChangeState();
            }
        }

        protected override IUILayout Build()
        {
            var area = this.AvailableArea;
            var sourceRectangle = this.SourceRectangle;

            switch (this.Fit)
            {
                case ImageFit.Distort:
                    // Do nothing, default values already distort
                    break;
                case ImageFit.Scale:
                    // Area occupied by the unmodified image
                    var imgArea = new TextureSlice(
                        this.AvailableArea.TopLeft,
                        drawable.Width,
                        drawable.Height
                    );

                    // Scale to fit the image into the available area
                    var scale = imgArea.Fit(this.AvailableArea.Size);

                    imgArea.Width = (int)(imgArea.Width * scale);
                    imgArea.Height = (int)(imgArea.Height * scale);

                    // Position the image in the area according to our alignment
                    area = this.Alignment.Apply(this.AvailableArea, imgArea);

                    break;
                case ImageFit.Crop:
                    // Our source rectangle
                    TextureSlice srcRectArea;
                    if (sourceRectangle.HasValue)
                    {
                        srcRectArea = this.SourceRectangle.Value;
                    }
                    else
                    {
                        srcRectArea = new TextureSlice(
                            Point.Zero,
                            drawable.Width,
                            drawable.Height
                        );
                    }

                    // If we need to crop for the image to fit...
                    if (srcRectArea.Width > this.AvailableArea.Width ||
                        srcRectArea.Height > this.AvailableArea.Height)
                    {
                        // Crop the image
                        var srcRectScale = srcRectArea.Crop(this.AvailableArea.Size);

                        srcRectArea.Width = (int)(srcRectArea.Width * srcRectScale);
                        srcRectArea.Height = (int)(srcRectArea.Height * srcRectScale);
                    }
                    else
                    {
                        // Position the image according to aout alignment
                        area = this.Alignment.Apply(this.AvailableArea, srcRectArea);
                    }

                    // Trim the leaking parts of the image
                    srcRectArea.Width -= srcRectArea.Width - this.AvailableArea.Width;
                    srcRectArea.Height -= srcRectArea.Height - this.AvailableArea.Height;

                    // Position the source rectangle on the image according to our alignment
                    sourceRectangle = this.Alignment.Apply(
                        new TextureSlice(Point.Zero, drawable.Width, drawable.Height),
                        srcRectArea
                    );

                    break;
            }

            return new IDrawableUILayout
            {
                Area = area,
                Drawable = drawable,
                Origin = Vector2.Zero,
                SourceRectangle = sourceRectangle,
            };
        }
    }

    public enum ImageFit
    {
        Distort,
        Scale,
        Crop,
    }
}