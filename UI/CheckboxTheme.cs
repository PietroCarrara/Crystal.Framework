using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI
{
    public class CheckboxTheme
    {
        public readonly IDrawable Check;
        public readonly IDrawable Box;

        public CheckboxTheme(IDrawable check, IDrawable box)
        {
            this.Check = check;
            this.Box = box;
        }
    }
}