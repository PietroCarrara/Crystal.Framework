using System;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.UI.UILayouts
{
    public interface IUILayout
    {
        TextureSlice Area { get; }

        /// <summary>
        /// Calls the appropriate action based on the layout type
        /// </summary>
        /// <param name="a">Called in case this is a OrderedUILayouts</param>
        /// <param name="b">Called in case this is a IDrawableUILayout</param>
        /// <param name="c">Called in case this is a TextUILayout</param>
        /// <param name="d">Called in case this is a IAnimatableUILayout</param>
        void Match(Action<OrderedWidgetsLayout> a,
                   Action<DrawableUILayout> b,
                   Action<TextUILayout> c,
                   Action<AnimatableUILayout> d,
                   Action<UnorderedWidgetsLayout> e);

        public static IUILayout Empty => new OrderedWidgetsLayout { Children = new Widget[0] };
    }
}