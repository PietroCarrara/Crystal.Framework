using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class StackingContainer : Widget
    {
        private List<Widget> widgets = new List<Widget>();

        public Widget Add(Widget widget)
        {
            this.widgets.Add(widget);
            widget.BecomeChildOf(this);
            this.ChangeState();

            return widget;
        }

        protected override IUILayout Build()
        {
            foreach (var child in this.widgets)
            {
                child.Area = this.Area;
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