using System;
using Crystal.Framework.Graphics;

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
        void Match(Action<OrderedUILayouts> a,
                   Action<IDrawableUILayout> b,
                   Action<TextUILayout> c,
                   Action<IAnimatableUILayout> d);

        public static IUILayout Empty => new OrderedUILayouts { Children = new IUILayout[0] };
    }
}