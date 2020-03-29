using System;
using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class HorizontalContainer : Container
    {
        protected override IUILayout Build()
        {
            var len = this.Children.Count;

            if (len <= 0)
            {
                return IUILayout.Empty;
            }

            var width = this.AvailableArea.Width / len;

            var i = 0;
            foreach (var child in this.Children)
            {
                child.AvailableArea = new TextureSlice(
                    this.AvailableArea.TopLeft.X + width * i,
                    this.AvailableArea.TopLeft.Y,
                    width,
                    this.AvailableArea.Height
                );

                i++;
            }

            return new UnorderedWidgetsLayout
            {
                Children = this.Children,
            };
        }
    }
}