namespace Crystal.Framework.UI
{
    public class UIEvent
    {
        public bool ShouldPreventPropagation { get; private set; }

        public void PreventPropagation()
        {
            this.ShouldPreventPropagation = true;
        }
    }
}