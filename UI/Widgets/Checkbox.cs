using System;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class Checkbox : Widget
    {
        private bool value = false;

        private DrawableWidget box,
                               check;

        public Action<Checkbox> OnChangeValue;

        public bool Value
        {
            get => value;
            set
            {
                this.value = value;
                this.OnChangeValue?.Invoke(this);
                this.ChangeState();
            }
        }

        public override void OnMouseClick(UIEvent e)
        {
            this.Value = !value;
            e.PreventPropagation();
        }

        public Checkbox()
        {
            box = new DrawableWidget();
            check = new DrawableWidget
            {
                Alignment = Alignment.Center,
            };

            box.BecomeChildOf(this);
            check.BecomeChildOf(this);
        }

        protected override IUILayout Build()
        {
            box.Drawable = Theme.CheckboxTheme.Box;
            check.Drawable = Theme.CheckboxTheme.Check;

            box.AvailableArea = this.AvailableArea;
            check.AvailableArea = Margins.XY(
                (int)(box.Layout.Area.Width * 0.2f),
                (int)(box.Layout.Area.Height * 0.2f)
            ).Apply(box.Layout.Area);

            if (value)
            {
                return new OrderedWidgetsLayout
                {
                    Children = new Widget[]
                    {
                        box,
                        check,
                    }
                };
            }

            return box.Layout;
        }
    }
}