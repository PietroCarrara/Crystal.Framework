using System.Collections.Generic;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.Renderers
{
    public class UIRenderer : IRenderer
    {
        public void Initialize(Scene scene)
        {
            // TODO: Remove
            scene.UI.Root.AvailableArea = new TextureSlice(Point.Zero, scene.WindowCanvas.Size);
            scene.WindowCanvas.SizeChanged += (c, size) =>
            {
                scene.UI.Root.AvailableArea = new TextureSlice(Point.Zero, size);
            };
        }

        public void Render(Scene scene, IDrawer drawer, float delta)
        {
            drawer.BeginDraw(
                scene.WindowCanvas,
                samplerState: SamplerState.PointClamp
            );
            drawWidget(scene.UI.Root, delta, drawer);
            drawer.EndDraw();
        }

        private void drawWidget(Widget ui, float delta, IDrawer drawer)
        {
            ui.Layout.Match(
                (ui) => drawWidgets(ui.Children, delta, drawer),
                (ui) => drawIDrawableUILayout(ui, delta, drawer),
                (ui) => drawTextUILayout(ui, drawer),
                (ui) => drawIAnimatableUILayout(ui, delta, drawer),
                (ui) => drawWidgets(ui.Children, delta, drawer)
            );
        }

        private void drawWidgets(IEnumerable<Widget> widgets, float delta, IDrawer drawer)
        {
            foreach (var widget in widgets)
            {
                drawWidget(widget, delta, drawer);
            }
        }

        private void drawIDrawableUILayout(DrawableUILayout drawable, float delta, IDrawer drawer)
        {
            drawer.Draw(
                drawable.Drawable,
                drawable.Area,
                drawable.Tint,
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
                drawable.Tint,
                drawable.Text,
                drawable.SourceRectangle
            );
        }

        private void drawIAnimatableUILayout(AnimatableUILayout animatable, float delta, IDrawer drawer)
        {
            animatable.Animatable.PreDraw(delta);

            drawer.Draw(
                animatable.Animatable,
                animatable.Area,
                animatable.Tint,
                delta,
                null,
                0,
                animatable.Animatable.GetSourceRectangle()
            );
        }
    }
}