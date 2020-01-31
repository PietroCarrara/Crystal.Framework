using System;
using System.Linq;

namespace Crystal.Framework.UI.Layouts
{
    public class VerticalLayout : Layout
    {
        protected override void OnChildAdded(IUIElement element) => this.rebalanceSizes();
        protected override void OnChildRemoved(IUIElement element) => this.rebalanceSizes();
        protected override void OnAvailableSpaceChanged() => this.rebalanceSizes();

        private void rebalanceSizes()
        {
            var size = this.AvailableSpace;

            int i = 0;
            size.Height /= this.Children.Count();

            foreach (var item in this.Children)
            {
                size.Position.Y = i * size.Height + this.AvailableSpace.Position.Y;
                item.AvailableSpace = size;
                i++;
            }
        }
    }
}