using System.Linq;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI.Layouts
{
    public class HorizontalLayout : Layout
    {
        protected override void OnChildAdded(IUIElement element) => this.rebalanceSizes();
        protected override void OnChildRemoved(IUIElement element) => this.rebalanceSizes();
        protected override void OnAvailableSpaceChanged() => this.rebalanceSizes();

        private void rebalanceSizes()
        {
            var size = this.AvailableSpace;

            int i = 0;
            size.Width /= this.Children.Count();

            foreach (var item in this.Children)
            {
                size.Position.X = i * size.Width + this.AvailableSpace.Position.X;
                item.AvailableSpace = size;
                i++;
            }
        }
    }
}