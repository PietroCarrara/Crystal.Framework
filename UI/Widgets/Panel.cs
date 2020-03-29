using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class Panel : SingleChildWidget
    {
        private NinePatchImage background;

        private NinePatchImageWidget bgWidget;

        public Panel()
        {
            bgWidget = new NinePatchImageWidget();
            bgWidget.BecomeChildOf(this);
        }

        public NinePatchImage Background
        {
            get => background;
            set
            {
                background = value;
                this.ChangeState();
            }
        }

        protected override IUILayout Build()
        {
            bgWidget.AvailableArea = this.AvailableArea;
            bgWidget.Image = background != null ? background : Theme.PanelBackground;

            Child.AvailableArea = bgWidget.Margins.Apply(this.AvailableArea);

            return new OrderedWidgetsLayout
            {
                Children = new Widget[]
                {
                    bgWidget,
                    Child
                }
            };
        }
    }
}