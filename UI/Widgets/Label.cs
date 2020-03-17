using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class Label : Widget
    {
        private string text = "", font = "";

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

            // Scale the font so it fits in the area but is not distorted
            var size = font.MeasureString(this.text);

            var scaleX = size.X / this.Area.Width;
            var scaleY = size.Y / this.Area.Height;
            var scale = System.Math.Max(scaleY, scaleX);

            var width = (int)(size.X / scale);
            var height = (int)(size.Y / scale);

            var area = new TextureSlice(
                this.Area.TopLeft,
                width,
                height
            );

            return new TextUILayout
            {
                Text = this.text,
                Font = font,
                Area = this.Alignment.Apply(this.Area, area),
            };
        }
    }
}