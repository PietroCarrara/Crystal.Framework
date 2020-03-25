using System;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.UI.UILayouts
{
    public struct TextUILayout : IUILayout
    {
        TextureSlice IUILayout.Area => this.Area;

        internal Widget Builder;
        Widget IUILayout.Builder { get => Builder; }

        public TextureSlice Area;
        public IFont Font;
        public string Text;

        public void Match(Action<OrderedUILayouts> a,
                          Action<IDrawableUILayout> b,
                          Action<TextUILayout> c,
                          Action<IAnimatableUILayout> d)
        {
            c(this);
        }
    }
}