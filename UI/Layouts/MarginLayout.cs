using System;
namespace Crystal.Framework.UI.Layouts
{
    public class MarginLayout : Layout
    {
        public float Left, Right, Top, Bottom;

        public MarginLayout(float left, float right, float top, float bottom)
        {
            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Bottom = bottom;
        }

        public MarginLayout(float vertical, float horizontal) : this(horizontal, horizontal, vertical, vertical)
        { }

        public MarginLayout(float all) : this(all, all, all, all)
        { }

        protected override void OnAvailableSpaceChanged() => rebalanceSize();
        protected override void OnChildAdded(IUIElement element) => rebalanceSize();
        protected override void OnChildRemoved(IUIElement element) => rebalanceSize();

        private void rebalanceSize()
        {
            var rect = this.AvailableSpace;

            rect.Position.X += this.Left;
            rect.Width -= this.Left;

            rect.Position.Y += this.Top;
            rect.Height -= this.Top;

            rect.Width -= this.Right;
            rect.Height -= this.Bottom;

            foreach (var child in this.Children)
            {
                child.AvailableSpace = rect;
            }
        }
    }
}