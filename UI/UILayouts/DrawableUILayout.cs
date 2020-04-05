using System;
using System.Numerics;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.UI.UILayouts
{
    public struct DrawableUILayout : IUILayout
    {
        TextureSlice IUILayout.Area => this.Area;

        public Vector2? Origin;
        public TextureSlice Area;
        public IDrawable Drawable;
        public Color Tint;
        public TextureSlice? SourceRectangle;

        public void Match(Action<OrderedWidgetsLayout> a,
                          Action<DrawableUILayout> b,
                          Action<TextUILayout> c,
                          Action<AnimatableUILayout> d,
                          Action<UnorderedWidgetsLayout> e)
        {
            b(this);
        }
    }
}