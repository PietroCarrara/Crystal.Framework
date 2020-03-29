using System;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class Button : SingleChildWidget
    {
        public Action<Button> Pressed;

        private ThreePatchImageWidget background;

        public Button()
        {
            this.background = new ThreePatchImageWidget();
            this.background.BecomeChildOf(this);
        }

        public override void OnMouseReleased()
        {
            this.Pressed?.Invoke(this);
        }

        protected override IUILayout Build()
        {
            this.background.Image = this.Theme.ButtonTheme.Default;
            this.background.AvailableArea = this.AvailableArea;

            Child.AvailableArea = this.background.Margins.Apply(this.AvailableArea);

            return new OrderedWidgetsLayout
            {
                Children = new Widget[]
                {
                    background,
                    Child
                }
            };
        }
    }
}