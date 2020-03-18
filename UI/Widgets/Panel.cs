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
            Child.Area = this.Area;
            bgWidget.Area = this.Area;

            bgWidget.Image = background != null ? background : Theme.PanelBackground;

            return new OrderedUILayouts
            {
                Children = new IUILayout[]
                {
                    bgWidget.Layout,
                    Child.Layout
                }
            };
        }
    }
}