using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI
{
    public class SliderTheme
    {
        public readonly IDrawable Knob, Bar;

        public SliderTheme(IDrawable knob, IDrawable bar)
        {
            this.Knob = knob;
            this.Bar = bar;
        }
    }
}