using System;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI.UILayouts
{
    public struct IAnimatableUILayout : IUILayout
    {
        TextureSlice IUILayout.Area => this.Area;

        public TextureSlice Area;
        public IAnimatable Animatable;

        public void Match(Action<OrderedUILayouts> a,
                          Action<IDrawableUILayout> b,
                          Action<TextUILayout> c,
                          Action<IAnimatableUILayout> d)
        {
            d(this);
        }
    }
}