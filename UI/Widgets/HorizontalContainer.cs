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
            var len = this.widgets.Count;

            if (len <= 0)
            {
                return IUILayout.Empty;
            }

            var width = this.Area.Width / len;

            var i = 0;
            foreach (var child in this.widgets)
            {
                child.AvailableArea = new TextureSlice(
                    this.Area.TopLeft.X + width * i,
                    this.Area.TopLeft.Y,
                    width,
                    this.Area.Height
                );

                i++;
            }

            return new OrderedUILayouts
            {
                Children = this.widgets,
            };
        }
    }
}