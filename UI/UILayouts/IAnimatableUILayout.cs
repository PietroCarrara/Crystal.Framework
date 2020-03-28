using System;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.UI.UILayouts
{
    public struct IAnimatableUILayout : IUILayout
    {
        TextureSlice IUILayout.Area => this.Area;

        public TextureSlice Area;
        public IAnimatable Animatable;

        public void Match(Action<OrderedWidgetsLayout> a,
                          Action<IDrawableUILayout> b,
                          Action<TextUILayout> c,
                          Action<IAnimatableUILayout> d,
                          Action<UnorderedWidgetsLayout> e)
        {
            d(this);
        }
    }
}