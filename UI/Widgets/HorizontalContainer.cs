using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class HorizontalContainer : Widget
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

            var width = this.Area.Width / len;
            
            var i = 0;
            foreach (var child in this.widgets)
            {
                child.Area = new TextureSlice(
                    this.Area.TopLeft.X + width * i,
                    this.Area.TopLeft.Y,
                    width,
                    this.Area.Height
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