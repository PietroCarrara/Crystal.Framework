using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class Button : SingleChildWidget
    {
        public delegate void ButtonClickedHandler(Button button);

        public event ButtonClickedHandler Pressed;

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

        protected override IUILayout Build()
        {
            this.background.Image = this.Theme.ButtonTheme.Default;
            if (this.borderThickness.HasValue)
            {
                this.background.BorderThickness = this.borderThickness.Value;
            }

            var margins = Margins.Horizontal(this.BorderThickness);
            Child.AvailableArea = margins.Apply(this.AvailableArea);

            var area = Child.Area;
            area.TopLeft.X -= margins.Left;
            area.Width += margins.Right;

            this.background.AvailableArea = area;

            return new OrderedUILayouts
            {
                Children = new IUILayout[]
                {
                    background.Layout,
                    Child.Layout
                }
            };
        }
    }
}