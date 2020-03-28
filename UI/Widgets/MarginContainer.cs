

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
            Child.AvailableArea = this.margins.Apply(this.AvailableArea);

            return Child.Layout;
        }
    }
}