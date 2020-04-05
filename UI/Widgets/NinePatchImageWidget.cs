using System.Numerics;
using System.Linq;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class NinePatchImageWidget : Widget
    {
        private DrawableWidget[] widgets;

        private NinePatchImage image;
        public NinePatchImage Image
        {
            get => image;
            set
            {
                image = value;
                this.ChangeState();
            }
        }

        public Color Tint
        {
            get => widgets[0].Tint;
            set
            {
                foreach (var w in widgets)
                {
                    w.Tint = value;
                }
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

        public Margins Margins => Margins.All(image.CalculateBorder(this.AvailableArea.Size));

        public NinePatchImageWidget()
        {
            this.widgets = new DrawableWidget[9];

            for (int i = 0; i < 9; i++)
            {
                this.widgets[i] = new DrawableWidget
                {
                    Fit = ImageFit.Distort,
                };
                this.widgets[i].BecomeChildOf(this);
            }
        }

        protected override IUILayout Build()
        {
            var i = 0;
            foreach (var primitive in image.DrawingPrimitives(this.AvailableArea))
            {
                this.widgets[i].Drawable = image.Texture;
                this.widgets[i].SourceRectangle = primitive.source;
                this.widgets[i].AvailableArea = primitive.destination;

                i++;
            }

            return new OrderedWidgetsLayout
            {
                Children = this.widgets,
            };
        }
    }
}