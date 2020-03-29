using System;
using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.UI;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.Collections.Specialized
{
    public class WidgetStorage
    {
        public StackingContainer Root { get; private set; } = new StackingContainer();

        private UIUpdater updater = new UIUpdater();

        /// <summary>
        /// Update focus and fire input events
        /// </summary>
        /// <param name="input">The object to query for input information</param>
        public void UpdateInput(Input input)
        {
            // HACK: Force layout to be built
            var _ = Root.Layout;

            updater.Update(Root.Children, input);
        }

        /// <summary>
        /// Adds a widget to the pool
        /// </summary>
        /// <param name="widget">The widget to add</param>
        public void Add(Widget widget)
        {
            this.Root.Add(widget);
        }
    }
}