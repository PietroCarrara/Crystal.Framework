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

        private Widget[] children;
        public IEnumerable<Widget> Children
        {
            get
            {
                if (needsResort)
                {
                    resort(children);
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

                children = value.ToArray();
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
                          Action<DrawableUILayout> b,
                          Action<TextUILayout> c,
                          Action<AnimatableUILayout> d,
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

        private void resort(Widget[] array)
        {
            var focusedIndex = -1;

            for (var i = 0; i < array.Length; i++)
            {
                if (focusedIndex < 0 && array[i].HasFocus)
                {
                    focusedIndex = i;
                }

                // If we have seen someone focused and the current
                // index has no focus, swap them out
                if (focusedIndex >= 0 && !array[i].HasFocus)
                {
                    swap(array, i, focusedIndex);
                    focusedIndex++;
                }
            }
        }

        private void swap<T>(T[] array, int a, int b)
        {
            var tmp = array[a];
            array[a] = array[b];
            array[b] = tmp;
        }
    }
}