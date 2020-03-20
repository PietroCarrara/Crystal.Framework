using Crystal.Framework.Graphics;
using Crystal.Framework.UI.Widgets;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.Collections.Specialized
{
    public class WidgetStorage
    {
        public StackingContainer Root;

        public WidgetStorage()
        {
            Root = new StackingContainer();
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