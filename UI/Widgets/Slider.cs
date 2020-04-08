using System;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class Slider : Widget
    {
        private float value;

        private DrawableWidget knob, slider;

        public Action<Slider> OnChangeValue;

        public float Value
        {
            get => value;
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException("value", value, "Values should be in range [0, 1]");
                }

                this.value = value;
                this.OnChangeValue?.Invoke(this);
                this.ChangeState();
            }
        }

        public Slider()
        {
            knob = new DrawableWidget
            {
                Alignment = Alignment.Center,
                Fit = ImageFit.Scale,
            };
            slider = new DrawableWidget
            {
                Alignment = Alignment.Center,
                Fit = ImageFit.Scale,
            };

            knob.BecomeChildOf(this);
            slider.BecomeChildOf(this);
        }

        public override void OnMouseClick(UIEvent e, Input input)
        {
            var mousePos = input.GetMousePositionRaw();

            var area = TextureSlice.Union(knob.Layout.Area, slider.Layout.Area);

            if (area.Contains(mousePos))
            {
                e.PreventPropagation();
            }
        }

        public override void OnMouseHold(UIEvent e, Input input)
        {
            var mousePos = input.GetMousePositionRaw();

            var area = TextureSlice.Union(knob.Layout.Area, slider.Layout.Area);

            if (area.Contains(mousePos))
            {
                var pos = mousePos.X - slider.Layout.Area.TopLeft.X;
                Value = Math.Clamp(pos / slider.Layout.Area.Width, 0, 1);

                e.PreventPropagation();
            }
        }

        protected override IUILayout Build()
        {
            knob.Drawable = Theme.SliderTheme.Knob;
            slider.Drawable = Theme.SliderTheme.Bar;

            slider.AvailableArea = this.AvailableArea;

            // Calculate position based on the value of the slider
            var pos = (slider.Layout.Area.Width * value) - (slider.Layout.Area.Width * knob.Alignment.X);
            var height = Math.Min(this.AvailableArea.Height, slider.Layout.Area.Height * 3);

            knob.AvailableArea = new TextureSlice(
                slider.Layout.Area.TopLeft + new Point((int)pos, -(height - slider.Layout.Area.Height) / 2),
                slider.Layout.Area.Width,
                height
            );

            return new OrderedWidgetsLayout
            {
                Children = new Widget[]
                {
                    slider,
                    knob,
                },
            };
        }
    }
}