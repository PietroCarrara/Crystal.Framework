using System.Linq;
using System.Numerics;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class ThreePatchImageWidget : Widget
    {
        private readonly IDrawableWidget[] widgets;

        private ThreePatchImage image;
        public ThreePatchImage Image
        {
            get => image;
            set
            {
                this.image = value;
                this.ChangeState();
            }
        }

        public int? BorderThickness
        {
            get => image.BorderThickness;
            set
            {
                image.BorderThickness = value;
                this.ChangeState();
            }
        }

        public Margins Margins => Margins.Horizontal(image.CalculateBorder(this.AvailableArea.Size));

        public ThreePatchImageWidget()
        {
            this.widgets = new IDrawableWidget[3];

            for (int i = 0; i < 3; i++)
            {
                widgets[i] = new IDrawableWidget
                {
                    Fit = ImageFit.Distort,
                };
                widgets[i].BecomeChildOf(this);
            }
        }

        protected override IUILayout Build()
        {
            var i = 0;
            foreach (var primitive in image.DrawingPrimitives(this.AvailableArea))
            {
                widgets[i].Drawable = image.Texture;
                widgets[i].SourceRectangle = primitive.source;
                widgets[i].AvailableArea = primitive.destination;

                i++;
            }

            return new OrderedWidgetsLayout
            {
                Children = widgets,
            };
        }
    }
}