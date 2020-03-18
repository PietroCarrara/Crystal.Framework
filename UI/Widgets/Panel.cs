using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class Panel : SingleChildWidget
    {
        private NinePatchImage background;

        private NinePatchImageWidget bgWidget;
        private Margins margins;

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
            bgWidget.Area = this.Area;
            bgWidget.Image = background != null ? background : Theme.PanelBackground;

            this.margins = Margins.XY(bgWidget.Image.BorderThickness.X, bgWidget.Image.BorderThickness.Y);
            Child.Area = this.margins.Apply(this.Area);

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