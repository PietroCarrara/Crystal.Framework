using System;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.UI.UILayouts
{
    public struct TextUILayout : IUILayout
    {
        TextureSlice IUILayout.Area => SourceRectangle.HasValue ? SourceRectangle.Value : this.Area;

        public TextureSlice Area;
        /// <summary>
        /// Slice of the area to be drawn. Crops text
        /// </summary>
        public TextureSlice? SourceRectangle;
        public IFont Font;
        public Color Tint;
        public string Text;

        public void Match(Action<OrderedWidgetsLayout> a,
                          Action<DrawableUILayout> b,
                          Action<TextUILayout> c,
                          Action<AnimatableUILayout> d,
                          Action<UnorderedWidgetsLayout> e)
        {
            c(this);
        }
    }
}