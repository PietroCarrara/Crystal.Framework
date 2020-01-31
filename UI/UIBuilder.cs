using System;
using System.Linq;
using System.Collections.Generic;

namespace Crystal.Framework.UI
{
    public class UIBuilder
    {
        private readonly Scene scene;
        private readonly ISkin skin;

        private Stack<Layout> layouts = new Stack<Layout>();

        public static UIBuilder Begin<T>(Scene scene, ISkin skin)
        where T : Layout, new()
        {
            var builder = new UIBuilder(scene, skin);
            return builder.Inside<T>();
        }

        private UIBuilder(Scene scene, ISkin skin)
        {
            this.scene = scene;
            this.skin = skin;
        }

        public UIBuilder Inside<T>()
        where T : Layout, new()
        {
            return this.Inside(new T());
        }

        public UIBuilder Inside(Layout layout)
        {
            layout.Skin = this.skin;

            if (this.layouts.Any())
            {
                this.layouts.Peek().AddChild(layout);
            }
            else
            {
                layout.Owner = this.scene;
                layout.AvailableSpace = new Rectangle(new Vector2(0, 0), scene.Viewport.Size);
            }

            this.layouts.Push(layout);

            return this;
        }

        public UIBuilder Add<T>()
        where T : IUIElement, new()
        {
            return this.Add(new T());
        }

        public UIBuilder Add(IUIElement element)
        {
            element.Skin = this.skin;
            
            this.layouts.Peek().AddChild(element);
            return this;
        }

        public UIBuilder Outside()
        {
            this.layouts.Pop();

            if (!this.layouts.Any())
            {
                throw new Exception("Closed last layout available!");
            }

            return this;
        }

        public Layout End()
        {
            var layout = this.layouts.Last();
            this.scene.Add(layout);
            return layout;
        }
    }
}