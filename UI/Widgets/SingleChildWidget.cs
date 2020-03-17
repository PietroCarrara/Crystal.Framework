using System;
namespace Crystal.Framework.UI.Widgets
{
    /// <summary>
    /// A widget that has a single child
    /// </summary>
    public abstract class SingleChildWidget : Widget
    {
        private Widget child;

        public Widget Child
        {
            get => child;
            set
            {
                if (child != null)
                {
                    throw new Exception("Can't change the child of this widget!");
                }

                value.BecomeChildOf(this);
                this.ChangeState();
                this.child = value;
            }
        }
    }
}