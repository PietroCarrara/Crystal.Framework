using System;
using System.Numerics;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI.UILayouts
{
    public struct IDrawableUILayout : IUILayout
    {
        TextureSlice IUILayout.Area => this.Area;

        public Vector2? Origin;
        public TextureSlice Area;
        public IDrawable Drawable;
        public TextureSlice? SourceRectangle;

        public void Match(Action<OrderedUILayouts> a,
                          Action<IDrawableUILayout> b,
                          Action<TextUILayout> c,
                          Action<IAnimatableUILayout> d)
        {
            b(this);
        }
    }
}