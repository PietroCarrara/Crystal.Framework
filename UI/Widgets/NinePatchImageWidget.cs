using System.Numerics;
using System.Linq;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class NinePatchImageWidget : Widget
    {
        private IDrawableWidget[] widgets;
        
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

        private Point? borderThickness;
        public Point BorderThickness
        {
            get => borderThickness.HasValue ? borderThickness.Value : image.BorderThickness;
            set
            {
                this.borderThickness = value;
                image.BorderThickness = value;
                this.ChangeState();
            }
        }
        
        public NinePatchImageWidget()
        {
            this.widgets = new IDrawableWidget[9];

            for (int i = 0; i < 9; i++)
            {
                this.widgets[i] = new IDrawableWidget
                {
                    Fit = ImageFit.Distort,
                };
                this.widgets[i].BecomeChildOf(this);
            }
        }
        
        protected override IUILayout Build()
        {
            if (borderThickness.HasValue)
            {
                image.BorderThickness = borderThickness.Value;
            }

            var i = 0;
            foreach (var primitive in image.DrawingPrimitives(this.AvailableArea))
            {
                this.widgets[i].Drawable = image.Texture;
                this.widgets[i].SourceRectangle = primitive.source;
                this.widgets[i].AvailableArea = primitive.destination;
                
                i++;
            }

            return new OrderedUILayouts
            {
                Children = this.widgets,
            };
        }
    }
}