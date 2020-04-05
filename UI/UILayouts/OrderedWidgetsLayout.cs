using System.Linq;
using System;
using Crystal.Framework.Graphics;
using System.Collections.Generic;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.UI.UILayouts
{
    public struct OrderedWidgetsLayout : IUILayout
    {
        public TextureSlice Area
        {
            get => calculateArea();
        }

        public IEnumerable<Widget> Children;

        public void Match(Action<OrderedWidgetsLayout> a,
                          Action<DrawableUILayout> b,
                          Action<TextUILayout> c,
                          Action<AnimatableUILayout> d,
                          Action<UnorderedWidgetsLayout> e)
        {
            a(this);
        }

        private TextureSlice calculateArea()
        {
            return TextureSlice.Union(
                this.Children.Select(c => c.Layout.Area)
            );
        }
    }
}