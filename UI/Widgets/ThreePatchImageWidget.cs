using System.Linq;
using System.Numerics;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class ThreePatchImageWidget : Widget
    {
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

        private int? borderThickness = new int?();
        public int BorderThickness
        {
            get => borderThickness.HasValue ? borderThickness.Value : image.BorderThickness;
            set
            {
                this.borderThickness = value;
                image.BorderThickness = value;
                this.ChangeState();
            }
        }

        protected override IUILayout Build()
        {
            if (borderThickness.HasValue)
            {
                image.BorderThickness = borderThickness.Value;
            }
            
            return new OrderedUILayouts
            {
                Children = image
                    .DrawingPrimitives(this.Area)
                    .Select(primitive => (IUILayout)new IDrawableUILayout
                    {
                        Area = primitive.destination,
                        SourceRectangle = primitive.source,
                        Drawable = image.Texture,
                        Origin = Vector2.Zero
                    })
            };
        }
    }
}