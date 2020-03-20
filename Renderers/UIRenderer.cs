using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.Renderers
{
    /// <summary>
    /// Naive UI rendering
    /// Does not implement damage control
    /// </summary>
    public class UIRenderer : IRenderer
    {
        private Canvas canvas;
        
        public void Initialize(Scene scene)
        {
            this.canvas = scene.Canvases.Create();
        }

        public void Render(Scene scene, float delta)
        {
            var root = scene.Widgets.Root;
            root.AvailableArea = new TextureSlice(Point.Zero, canvas.Size);
            
            scene.Drawer.BeginDraw(
                canvas,
                samplerState: SamplerState.PointClamp
            );
            drawIUILayout(root.Layout, delta, scene.Drawer);
            scene.Drawer.EndDraw();
        }

        private void drawIUILayout(IUILayout ui, float delta, IDrawer drawer)
        {
            ui.Match(
                (ui) => drawOrderedUILayout(ui, delta, drawer),
                (ui) => drawIDrawableUILayout(ui, delta, drawer),
                (ui) => drawTextUILayout(ui, drawer),
                (ui) => drawIAnimatableUILayout(ui, delta, drawer)
            );
        }

        private void drawOrderedUILayout(OrderedUILayouts ordered, float delta, IDrawer drawer)
        {
            foreach (var child in ordered.Children)
            {
                drawIUILayout(child, delta, drawer);
            }
        }

        private void drawIDrawableUILayout(IDrawableUILayout drawable, float delta, IDrawer drawer)
        {
            drawer.Draw(
                drawable.Drawable,
                drawable.Area,
                delta,
                drawable.Origin,
                0,
                drawable.SourceRectangle
            );
        }

        private void drawTextUILayout(TextUILayout drawable, IDrawer drawer)
        {
            drawer.DrawString(
                drawable.Font,
                drawable.Area,
                drawable.Text
            );
        }

        private void drawIAnimatableUILayout(IAnimatableUILayout animatable, float delta, IDrawer drawer)
        {
            animatable.Animatable.PreDraw(delta);

            drawer.Draw(
                animatable.Animatable,
                animatable.Area,
                delta,
                null,
                0,
                animatable.Animatable.GetSourceRectangle()
            );
        }
    }
}