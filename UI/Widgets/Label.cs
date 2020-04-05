using System;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class Label : Widget
    {
        private string text = "", font = "";
        private bool expand = true, shrink = true;
        private Color tint = Color.Black;

        public string Text
        {
            get => text;
            set
            {
                text = value;
                this.ChangeState();
            }
        }

        public string Font
        {
            get => font;
            set
            {
                this.font = value;
                this.ChangeState();
            }
        }

        public bool Expand
        {
            get => expand;
            set
            {
                this.expand = value;
                this.ChangeState();
            }
        }

        public bool Shrink
        {
            get => shrink;
            set
            {
                this.shrink = value;
                this.ChangeState();
            }
        }

        public Color Tint
        {
            get => tint;
            set
            {
                tint = value;
                this.ChangeState();
            }
        }

        protected override IUILayout Build()
        {
            IFont font;
            if (this.font != "")
            {
                font = this.Theme.Fonts[this.font];
            }
            else
            {
                font = this.Theme.MediumFont;
            }

            var size = font.MeasureString(this.text);

            var area = new TextureSlice(
                this.AvailableArea.TopLeft,
                (Point)size
            );

            // Scale the font so it fits in the area but is not distorted
            var scaleX = this.AvailableArea.Width / size.X;
            var scaleY = this.AvailableArea.Height / size.Y;
            var scale = System.Math.Min(scaleY, scaleX);

            var source = this.AvailableArea;

            // If we are shrinking or expanding
            if ((scale > 1 && expand) || (scale < 1 && shrink))
            {
                var width = (int)(size.X * scale);
                var height = (int)(size.Y * scale);

                area = new TextureSlice(
                    this.AvailableArea.TopLeft,
                    width,
                    height
                );
            }

            area = this.Alignment.Apply(this.AvailableArea, area);

            return new TextUILayout
            {
                Text = this.text,
                Font = font,
                Area = area,
                Tint = tint,
                SourceRectangle = source,
            };
        }
    }
}