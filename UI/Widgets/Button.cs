using System;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class Button : SingleChildWidget
    {
        public Action<Button> Pressed;

        private ThreePatchImageWidget background;

        private bool click = false;

        public Button()
        {
            this.background = new ThreePatchImageWidget();
            this.background.BecomeChildOf(this);
        }

        public override void OnMouseReleased(UIEvent e, Input input)
        {
            this.Pressed?.Invoke(this);
            click = false;
            e.PreventPropagation();
            this.ChangeState();
        }

        public override void OnMouseClick(UIEvent e, Input input)
        {
            click = true;
            e.PreventPropagation();
            this.ChangeState();
        }

        public override void OnMouseLeave(Input input)
        {
            click = false;
            this.ChangeState();
        }

        protected override IUILayout Build()
        {
            var area = this.AvailableArea;

            if (click)
            {
                var scale = this.AvailableArea.Height / (float)Theme.ButtonTheme.Primary.Texture.Height;
                var height = (int)(Theme.ButtonTheme.Secondary.Texture.Height * scale);

                area.TopLeft.Y += area.Height - height;
                area.Height = height;
            }

            this.background.Image = click ? Theme.ButtonTheme.Secondary : Theme.ButtonTheme.Primary;
            this.background.AvailableArea = area;

            Child.AvailableArea = this.background.Margins.Apply(area);

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