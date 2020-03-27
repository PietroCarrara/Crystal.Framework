using System.Linq;
using System;
using Crystal.Framework.Graphics;
using System.Collections.Generic;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.UI.UILayouts
{
    public struct OrderedUILayouts : IUILayout
    {
        private TextureSlice? area;
        
        public TextureSlice Area
        {
            get
            {
                if (this.area.HasValue) return this.area.Value;

                this.area = this.calculateArea();
                return this.area.Value;
            }
        }

        internal Widget Builder;
        Widget IUILayout.Builder { get => Builder; }

        public IEnumerable<Widget> Children;

        public void Match(Action<OrderedUILayouts> a,
                          Action<IDrawableUILayout> b,
                          Action<TextUILayout> c,
                          Action<IAnimatableUILayout> d)
        {
            a(this);
        }

        private TextureSlice calculateArea()
        {
            return TextureSlice.Union(
                this.Children.Select(c => c.Area)
            );
        }
    }
}