using System;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI.UILayouts
{
    public struct IDrawableUILayout : IUILayout
    {
        TextureSlice IUILayout.Area => this.Area;

        public TextureSlice Area;
        public IDrawable Drawable;

        public void Match(Action<OrderedUILayouts> a,
                          Action<IDrawableUILayout> b,
                          Action<TextUILayout> c,
                          Action<IAnimatableUILayout> d)
        {
            b(this);
        }
    }
}