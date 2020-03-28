using System.Linq;
using System.Collections.Generic;
using System;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.UI.UILayouts
{
    public struct UnorderedWidgetsLayout : IUILayout
    {
        private bool needsResort;

        public TextureSlice Area
        {
            get => calculateArea();
        }

        public Widget Builder { get; set; }

        private IEnumerable<Widget> children;
        public IEnumerable<Widget> Children
        {
            get
            {
                if (needsResort)
                {
                    children = children.OrderBy(w => w.HasFocus ? 1 : 0);
                    needsResort = false;
                }

                return children;
            }
            set
            {
                if (children != null)
                {
                    foreach (var child in children)
                    {
                        child.StateChanged -= requestResort;
                    }
                }

                children = value;
                needsResort = true;

                foreach (var child in children)
                {
                    child.StateChanged += requestResort;
                }
            }
        }

        private void requestResort(Widget w)
        {
            this.needsResort = true;
        }

        public void Match(Action<OrderedWidgetsLayout> a,
                          Action<IDrawableUILayout> b,
                          Action<TextUILayout> c,
                          Action<IAnimatableUILayout> d,
                          Action<UnorderedWidgetsLayout> e)
        {
            e(this);
        }

        private TextureSlice calculateArea()
        {
            return TextureSlice.Union(
                this.Children.Select(c => c.Layout.Area)
            );
        }
    }
}