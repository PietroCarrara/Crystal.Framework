using System;
using System.Linq;
using System.Collections.Generic;

namespace Crystal.Framework.UI.Widgets
{
    public abstract class Container : Widget
    {
        public Widget[] Widgets
        {
            get => this.Children.ToArray();
            set
            {
                if (this.Children.Any())
                {
                    throw new Exception("Can't set the list of widgets when it's not empty!");
                }

                foreach (var child in value)
                {
                    this.addChild(child);
                }

                this.OnSetChildren(value);
            }
        }

        public void Add(Widget widget)
        {
            this.addChild(widget);
        }

        private void addChild(Widget widget)
        {
            widget.BecomeChildOf(this);
            this.ChangeState();
        }

        protected virtual void OnSetChildren(Widget[] children)
        {

        }
    }
}