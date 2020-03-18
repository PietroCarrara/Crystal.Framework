using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class StackingContainer : Container
    {
        protected override IUILayout Build()
        {
            foreach (var child in this.widgets)
            {
                child.AvailableArea = this.Area;
            }

            return new OrderedUILayouts
            {
                Children = this.widgets.Select(w =>
                {
                    return w.Layout;
                }),
            };
        }
    }
}