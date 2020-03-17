using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class VerticalContainer : Widget
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
            var len = this.widgets.Count;

            var height = this.Area.Height / len;
            
            var i = 0;
            foreach (var child in this.widgets)
            {
                child.Area = new TextureSlice(
                    this.Area.TopLeft.X,
                    this.Area.TopLeft.Y + height * i,
                    this.Area.Width,
                    height
                );

                i++;
            }

            return new OrderedUILayouts
            {
                Children = this.widgets.Select(w => w.Layout),
            };
        }
    }
}