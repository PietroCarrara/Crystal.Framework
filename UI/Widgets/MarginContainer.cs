

using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class MarginContainer : SingleChildWidget
    {
        private Margins margins;
        public Margins Margins
        {
            get => margins;
            set
            {
                this.margins = value;
                this.ChangeState();
            }
        }
        
        protected override IUILayout Build()
        {
            Child.Area = this.margins.Apply(this.Area);

            return Child.Layout;
        }
    }
}