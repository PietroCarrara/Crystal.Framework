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

        private int? borderThickness;
        public int BorderThickness
        {
            get => borderThickness.HasValue ? borderThickness.Value : background.BorderThickness;
            set
            {
                this.borderThickness = value;
                background.BorderThickness = value;
                this.ChangeState();
            }
        }

        public override void OnMouseReleased()
        {
            this.Pressed?.Invoke(this);
        }

        protected override IUILayout Build()
        {
            this.background.Image = this.Theme.ButtonTheme.Default;
            if (this.borderThickness.HasValue)
            {
                this.background.BorderThickness = this.borderThickness.Value;
            }

            var margins = Margins.Horizontal(this.BorderThickness);
            Child.AvailableArea = margins.Apply(this.AvailableArea);

            var area = Child.Layout.Area;
            area.TopLeft.X -= margins.Left;
            area.Width += margins.Right;

            this.background.AvailableArea = area;

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