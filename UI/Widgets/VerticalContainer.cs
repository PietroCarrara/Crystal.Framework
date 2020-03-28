using System;
using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class VerticalContainer : Container
    {
        protected override IUILayout Build()
        {
            var len = this.widgets.Count;

            if (len <= 0)
            {
                return IUILayout.Empty;
            }

            var height = this.AvailableArea.Height / len;

            var i = 0;
            foreach (var child in this.widgets)
            {
                child.AvailableArea = new TextureSlice(
                    this.AvailableArea.TopLeft.X,
                    this.AvailableArea.TopLeft.Y + height * i,
                    this.AvailableArea.Width,
                    height
                );

                i++;
            }

            return new UnorderedWidgetsLayout
            {
                Children = this.widgets,
            };
        }
    }
}