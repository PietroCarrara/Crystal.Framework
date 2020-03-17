using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class Label : Widget
    {
        private string text, font;

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
            var font = this.Theme.Fonts[this.font];
            
            return new TextUILayout
            {
                Text = this.text,
                Font = font,
                Area = this.Area,
            };
        }
    }
}