using System;
using System.Linq;
using System.Collections.Generic;

namespace Crystal.Framework.UI.Widgets
{
    public abstract class Container : Widget
    {
        protected List<Widget> widgets = new List<Widget>();

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

    }
}