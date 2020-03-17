using Crystal.Framework.Graphics;
using Crystal.Framework.UI.Widgets;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.Collections.Specialized
{
    public class WidgetStorage
    {
        private Scene scene;
        private StackingContainer root;

        public WidgetStorage(Scene s)
        {
            this.scene = s;
            root = new StackingContainer();
        }

        /// <summary>
        /// Adds a widget to the pool
        /// </summary>
        /// <param name="widget">The widget to add</param>
        public void Add(Widget widget)
        {
            this.root.Add(widget);
        }

        public IUILayout Layout
        {
            get 
            {
                if (root.NeedsRebuild)
                {
                    root.Theme = scene.Theme;
                    root.Area = new TextureSlice(Point.Zero, scene.Viewport.Size);
                }
                
                return root.Layout;
            }
        }
    }
}