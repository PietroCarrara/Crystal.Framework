using System;
using System.Linq;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class NinePatchImageWidget : Widget
    {
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

        private float? borderThickness;
        public float BorderThickness
        {
            get => borderThickness.HasValue ? borderThickness.Value : image.BorderThickness;
            set
            {
                this.borderThickness = value;
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