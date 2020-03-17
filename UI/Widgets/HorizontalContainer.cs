using System;
using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class HorizontalContainer : Widget
    {
        private List<Widget> widgets = new List<Widget>();

        public Widget[] Widgets
        {
            get => widgets.ToArray();
            set
            {
                if (this.widgets.Count != 0)
                {
                    throw new Exception("Can't set the list of widgets when it's not empty!");
                }

                this.widgets = value.ToList();

                foreach (var widget in this.widgets)
                {
                    this.addChild(widget);
                }
            }
        }

        public void Add(Widget widget)
        {
            this.widgets.Add(widget);
            this.addChild(widget);
        }

        private void addChild(Widget widget)
        {
            widget.BecomeChildOf(this);
            this.ChangeState();
        }

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