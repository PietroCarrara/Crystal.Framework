using System;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.UI.UILayouts
{
    public struct AnimatableUILayout : IUILayout
    {
        TextureSlice IUILayout.Area => this.Area;

        public TextureSlice Area;
        public Color Tint;
        public IAnimatable Animatable;

        public void Match(Action<OrderedWidgetsLayout> a,
                          Action<DrawableUILayout> b,
                          Action<TextUILayout> c,
                          Action<AnimatableUILayout> d,
                          Action<UnorderedWidgetsLayout> e)
        {
            d(this);
        }
    }
}