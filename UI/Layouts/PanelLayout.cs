using System;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI.Layouts
{
    public class PanelLayout : Layout
    {
        protected override void OnChildAdded(IUIElement element) => element.AvailableSpace = this.AvailableSpace;

        protected override void OnAvailableSpaceChanged()
        {
            foreach (var child in this.Children)
            {
                child.AvailableSpace = this.AvailableSpace;
            }
        }

        public override void Draw(IDrawer drawer, float delta)
        {
            var background = this.Skin.PanelBackground;

            drawer.Draw(background, this.AvailableSpace, delta, Vector2.Zero);
        }
    }
}