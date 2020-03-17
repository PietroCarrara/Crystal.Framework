using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class Panel : SingleChildWidget
    {
        private NinePatchImage background;
        
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
            var bg = Background;

            if (bg == null)
            {
                bg = Theme.PanelBackground;    
            }

            Child.Area = this.Area;

            var ninePatch = new NinePatchImageWidget
            {
                Area = this.Area,
                Image = bg,
            };
            ninePatch.BecomeChildOf(this);

            return new OrderedUILayouts
            {
                Children = new IUILayout[]
                {
                    ninePatch.Layout,
                    Child.Layout
                }
            };
        }
    }
}