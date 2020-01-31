using System;
using System.Collections.Generic;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI
{
    public abstract class Layout : IUIElement
    {
        public Scene Owner { get; set; }
        public Layout Parent { get; set; }
        public Vector2 Position => Parent == null ?
                                       AvailableSpace.Position :
                                       Parent.Position + AvailableSpace.Position;
        private Rectangle availableSpace;
        public Rectangle AvailableSpace
        {
            get => this.availableSpace;
            set
            {
                this.availableSpace = value;
                this.OnAvailableSpaceChanged();
            }
        }
        public AnchorPoint Anchor { get; set; }

        public List<IUIElement> Children { get; private set; } = new List<IUIElement>();
        public ISkin Skin { get; set; }

        protected virtual void OnAvailableSpaceChanged()
        { }
        protected virtual void OnChildAdded(IUIElement element)
        { }
        protected virtual void OnChildRemoved(IUIElement element)
        { }

        public void AddChild(IUIElement element)
        {
            element.Parent = this;
            this.Children.Add(element);
            this.OnChildAdded(element);
        }

        public void RemoveChild(IUIElement element)
        {
            element.Parent = null;
            this.Children.Remove(element);
            this.OnChildRemoved(element);
        }

        public virtual void Update(float delta)
        {
            foreach (var child in this.Children)
            {
                child.Update(delta);
            }
        }

        public virtual void Draw(IDrawer drawer, float delta)
        {
            foreach (var child in this.Children)
            {
                child.Draw(drawer, delta);
            }
        }
    }
}