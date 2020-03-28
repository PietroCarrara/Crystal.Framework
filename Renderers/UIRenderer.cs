using System.Collections.Generic;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;
using Crystal.Framework.UI.Widgets;

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
            scene.Widgets.Root.AvailableArea = new TextureSlice(Point.Zero, canvas.Size);
            canvas.SizeChanged += (c, size) =>
            {
                scene.Widgets.Root.AvailableArea = new TextureSlice(Point.Zero, size);
            };
        }

        public void Render(Scene scene, float delta)
        {
            canvas.Clear();
            
            scene.Drawer.BeginDraw(
                canvas,
                samplerState: SamplerState.PointClamp
            );
            drawWidget(scene.Widgets.Root, delta, scene.Drawer);
            scene.Drawer.EndDraw();
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