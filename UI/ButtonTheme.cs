using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI
{
    public struct ButtonTheme
    {
        public readonly ThreePatchImage Primary;
        public readonly ThreePatchImage Secondary;

        public ButtonTheme(ThreePatchImage primary, ThreePatchImage secondary)
        {
            this.Primary = primary;
            this.Secondary = secondary;
        }
    }
}