using System;
using System.Linq;
using System.Collections.Generic;

namespace Crystal.Framework.UI.Widgets
{
    public abstract class Container : Widget
    {
        public void Add(Widget widget)
        {
            this.addChild(widget);
        }

        private void addChild(Widget widget)
        {
            widget.BecomeChildOf(this);
            this.ChangeState();
        }
    }
}